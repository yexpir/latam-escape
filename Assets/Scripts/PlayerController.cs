using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _forwardSpeed;
    [SerializeField] float _sideStepSpeed;
    [SerializeField] float _sideStepSize;
    [SerializeField] float _turnSpeed;
    bool isSideSteping;
    bool isTurning;

    RaycastHit _hitR;
    RaycastHit _hitL;
    void Update()
    {
        transform.position += transform.forward * (_forwardSpeed * Time.deltaTime);
        print("SIDESTEP " + isSideSteping + " --- TURNING " + isTurning);
        if(isSideSteping) return;
        if (isTurning) return;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(TurnCorner(1));
            StartCoroutine(SideStep(1));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(TurnCorner(-1));
            StartCoroutine(SideStep(-1));
        }
    }

    Vector3 targetSidePos;
    IEnumerator SideStep(int direction)
    {
        if (isTurning) yield break;
        if (!CanSideStep(direction)) yield break;
        isSideSteping = true;
        
        float currDistance;
        var prevDistance = _sideStepSize;
        
        if (IsMovingAlongZ)
        {
            targetSidePos = transform.position + transform.right * (_sideStepSize * direction);
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
            targetSidePos = transform.position + transform.right * (_sideStepSize * direction);
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
        
        
        isSideSteping = false;
    }



    IEnumerator TurnCorner(int direction)
    {
        //Physics.Raycast(transform.position, -transform.right + transform.forward*0.5f, 10)
        if(isSideSteping) yield break;
        if (!CanTurn(direction)) yield break;
        isTurning = true;

        var startRotation = transform.rotation;
        var targetDirection = transform.right * direction;
        var lookRotation = Quaternion.LookRotation(targetDirection);
        var time = 0.0f;

        var isInZ = IsMovingAlongZ;
        var forwardValue = isInZ ? transform.position.z : transform.position.x;
        float girdForwardValue;
        if ((isInZ && transform.forward.z > 0) || (!isInZ && transform.forward.x > 0))
        {
            girdForwardValue = Mathf.Ceil(forwardValue / 3) * 3;
        }
        else
        {
            girdForwardValue = Mathf.Floor(forwardValue / 3) * 3;
        }
        do
        {
            forwardValue = isInZ ? transform.position.z : transform.position.x;
            yield return null;
        } while (forwardValue < girdForwardValue);
        
        var startPosition = isInZ ? 
            new Vector3(transform.position.x, transform.position.y, girdForwardValue) : 
            new Vector3(girdForwardValue, transform.position.y, transform.position.z);
        transform.position = startPosition;
        
        var turnTargetPos = transform.position + transform.forward * 3;
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
        
        isTurning = false;
    }

    bool IsMovingAlongZ => Mathf.Abs(transform.right.x) > Mathf.Abs(transform.right.z);

    bool CanTurn(int dir)
    {
        var target = transform.position + transform.forward + transform.right * (dir * 10);
        var direction = target - transform.position;
        return Physics.Raycast(transform.position, direction, 10, Block.blockLayerMask);
    }

    bool CanSideStep(int dir)
    {
        return !CanTurn(dir) && Physics.Raycast(transform.position, transform.right * dir, 3, Block.blockLayerMask);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Block")) GameManager.ResetCurrentScene();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward + transform.right * 10);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward - transform.right * 10);
        Gizmos.DrawWireSphere(targetSidePos, 3);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 3);
        Gizmos.DrawLine(transform.position, transform.position - transform.right * 3);
    }
}
