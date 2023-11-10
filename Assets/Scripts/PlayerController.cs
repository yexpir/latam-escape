using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _forwardSpeed;
    [SerializeField] float _sideStepSpeed;
    [SerializeField] float _sidestepSize;
    [SerializeField] float _turnSpeed;
    [SerializeField] float _acceleration;
    bool _isSideSteping;
    bool _isTurning;

    RaycastHit _hitR;
    RaycastHit _hitL;

    [SerializeField] float _turnOriginForwardOffset;
    [SerializeField] float _turnTargetForwardOffset;
    [SerializeField] float _sideStepOriginForwardOffset;
    [SerializeField] float _sidestepTargetForwardOffset;

    Vector3 TurnRayOrigin => transform.position + transform.forward * _turnOriginForwardOffset;
    Vector3 TurnRayTarget;
    Vector3 SidestepRayOrigin => transform.position + transform.forward * _sideStepOriginForwardOffset;
    Vector3 SidestepRayTarget;
    
    void Update()
    {
        transform.position += transform.forward * (_forwardSpeed * Time.deltaTime);
        
        if(_isSideSteping) return;
        if (_isTurning) return;
        
        if (In.RightPressed || In.LeftPressed)
        {
            StartCoroutine(TurnCorner(In.XInt));
            StartCoroutine(SideStep(In.XInt));
        }

        _forwardSpeed += Time.deltaTime * _acceleration;
    }

    Vector3 targetSidePos;
    IEnumerator SideStep(int direction)
    {
        if (_isTurning) yield break;
        if (!CanSidestep(direction)) yield break;
        _isSideSteping = true;
        
        float currDistance;
        var prevDistance = _sidestepSize;
        
        if (IsMovingAlongZ)
        {
            targetSidePos = transform.position + transform.right * (_sidestepSize * direction);
            while (true)
            {
                transform.position += transform.right * (direction * _sideStepSpeed * Time.deltaTime);
                targetSidePos.z = transform.position.z;
                currDistance = Vector3.Distance(targetSidePos, transform.position);
                if (currDistance > prevDistance)
                {
                    break;
                }
                prevDistance = currDistance;
                yield return null;
            }
            transform.position = targetSidePos;
        }
        else
        {
            targetSidePos = transform.position + transform.right * (_sidestepSize * direction);
            while (true)
            {
                transform.position += transform.right * (direction * _sideStepSpeed * Time.deltaTime);
                targetSidePos.x = transform.position.x;
                currDistance = Vector3.Distance(targetSidePos, transform.position);
                if (currDistance > prevDistance)
                {
                    break;
                }
                prevDistance = currDistance;
                yield return null;
            }
            transform.position = targetSidePos;
        }
        
        
        _isSideSteping = false;
    }

    IEnumerator TurnCorner(int direction)
    {
        if(_isSideSteping) yield break;
        if (!CanTurn(direction)) yield break;
        _isTurning = true;

        var startRotation = transform.rotation;
        var targetDirection = transform.right * direction;
        var lookRotation = Quaternion.LookRotation(targetDirection);
        var time = 0.0f;

        var isInZ = IsMovingAlongZ;
        var forwardValue = isInZ ? transform.position.z : transform.position.x;
        float girdForwardValue;
        if (isInZ && transform.forward.z > 0 || !isInZ && transform.forward.x > 0)
            girdForwardValue = Mathf.Ceil(forwardValue / _sidestepSize) * _sidestepSize;
        else
            girdForwardValue = Mathf.Floor(forwardValue / _sidestepSize) * _sidestepSize;

        do
        {
            forwardValue = isInZ ? transform.position.z : transform.position.x;
            yield return null;
        } while (forwardValue < girdForwardValue);
        
        var startPosition = isInZ ? 
            new Vector3(transform.position.x, transform.position.y, girdForwardValue) : 
            new Vector3(girdForwardValue, transform.position.y, transform.position.z);
        transform.position = startPosition;
        
        var turnTargetPos = transform.position + transform.forward * _sidestepSize;
        var targetDistance = isInZ ? turnTargetPos.z : turnTargetPos.x;
        while (time < 1f)
        {
            transform.rotation = Quaternion.Lerp(startRotation, lookRotation, time);

            transform.position = isInZ ? 
                new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(startPosition.z, targetDistance, time)) : 
                new Vector3(Mathf.Lerp(startPosition.x, targetDistance, time), transform.position.y, transform.position.z);
            
            time += Time.deltaTime * _turnSpeed;
            
            yield return null;
        }
        transform.rotation = Quaternion.Lerp(startRotation, lookRotation, 1);
        transform.position = isInZ ? 
            new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(startPosition.z, targetDistance, 1)) : 
            new Vector3(Mathf.Lerp(startPosition.x, targetDistance, 1), transform.position.y, transform.position.z);
        
        _isTurning = false;
    }

    bool IsMovingAlongZ => Mathf.Abs(transform.right.x) > Mathf.Abs(transform.right.z);

    bool CanTurn(int dir)
    {
        TurnRayTarget = GetTurnRayTarget(dir);
        var direction = (TurnRayTarget - TurnRayOrigin).normalized;
        return !Physics.Raycast(TurnRayOrigin, direction, CityBuilder.streetWidth, Block.layerMask);
    }

    bool CanSidestep(int dir)
    {
        SidestepRayTarget = GetSidestepRayTarget(dir);
        var direction = (SidestepRayTarget - SidestepRayOrigin).normalized;
        return !CanTurn(dir) && !Physics.Raycast(SidestepRayOrigin, direction, _sidestepSize + 1, Block.layerMask);
    }

    Vector3 GetTurnRayTarget(int dir) => TurnRayOrigin + transform.forward * _turnTargetForwardOffset + transform.right * (dir * CityBuilder.streetWidth);

    Vector3 GetSidestepRayTarget(int dir) => SidestepRayOrigin + transform.forward * _sidestepTargetForwardOffset + transform.right * (dir * _sidestepSize);

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Block")) GameManager.ResetCurrentScene();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(TurnRayOrigin, GetTurnRayTarget(1));
        Gizmos.DrawLine(TurnRayOrigin, GetTurnRayTarget(-1));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(SidestepRayOrigin, GetSidestepRayTarget(1));
        Gizmos.DrawLine(SidestepRayOrigin, GetSidestepRayTarget(-1));
    }
}
