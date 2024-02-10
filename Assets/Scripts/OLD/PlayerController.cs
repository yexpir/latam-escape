using System.Collections;
using System;
using UnityEngine;
using System.Linq;
using UnityEditor;
using UnityEditor.Rendering;
using WIP.Utils;
using Grid = WIP.Utils.Grid;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] float _forwardSpeed;
    [SerializeField] float _sideStepSpeed;
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

    public Vector3 Position => transform.position;

    Vector3 TurnRayOrigin => transform.position + transform.forward * _turnOriginForwardOffset;
    Vector3 TurnRayTarget;
    Vector3 SidestepRayOrigin => transform.position + transform.forward * _sideStepOriginForwardOffset;
    Vector3 SidestepRayTarget;

    public float Angle
    {
        get => _angle;
        set => _angle = M.Mod(value, 360f);
    }[SerializeField]float _angle;
    
    [SerializeField]float radius;

    public float speed
    {
        get => _speedBackField;
        set => _speedBackField = value * 100;
    } float _speedBackField;
    [SerializeField]float _speed;
    
    void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        speed = _speed;
    }
    
    void OnValidate()
    {
        _angle = M.Mod(_angle, 360);
        _speed = Mathf.Abs(_speed);
        speed = _speed;
    }

    void Update()
    {
        // Angle += Time.deltaTime * speed;
        //     
        // var radians = Angle * Mathf.Deg2Rad;
        //
        //
        // var pos = transform.position;
        // pos.x = Mathf.Cos(radians) * radius;
        // pos.z = Mathf.Sin(radians) * radius;
        //
        // transform.position = pivot + pos;
        // transform.rotation = Quaternion.Euler(0,-Angle,0);
        //
        //
        // var startAngle = M.Mod(-Vector3.SignedAngle(Vector3.right, (transform.position - pivot).Flat().normalized, Vector3.up), 360f);
        // var rotationVector = (transform.position - pivot).normalized;
        // var startingAngle = M.Mod(Mathf.Atan2(rotationVector.z, rotationVector.x) * Mathf.Rad2Deg, 360);
        // print(startAngle);
        
        
        transform.position += transform.forward * (_forwardSpeed * Time.deltaTime);
        print(speed);
        if(_isSideSteping) return;
        if (_isTurning) return;
        
        if (In.RightPressed || In.LeftPressed)
        {
            StartCoroutine(Turn(In.XInt));
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
        var prevDistance = CityBuilder.cellSize;
        
        if (IsMovingAlongZ)
        {
            targetSidePos = transform.position + transform.right * (CityBuilder.cellSize * direction);
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
            targetSidePos = transform.position + transform.right * (CityBuilder.cellSize * direction);
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

    IEnumerator Turn(int dir)
    {
        if(_isSideSteping) yield break;
        if (!CanTurn(dir)) yield break;
        
        _isTurning = true;

        
        var startingPosition = transform.NextPosition();
        var pivot = Grid.GetNextPosition(startingPosition, transform.right * dir);
        var startingRotation = GetStartAngle(pivot, startingPosition);

        var targetPosition = GetTurnTarget(dir);
        var targetRotation = startingRotation + 90f * -dir;
        
        while (!Grid.HasPassedPosition(transform, startingPosition))
        {
            print("WAITING");
            yield return null;
        }

        transform.position = startingPosition;
        transform.rotation = Quaternion.Euler(transform.rotation.x, startingRotation, transform.rotation.z);

        Angle = startingRotation;
        
        while (M.IsInRange(Angle, startingRotation, targetRotation))
        {
            Angle += Time.deltaTime * speed * -dir;
            
            var radians = Angle * Mathf.Deg2Rad;

            var circlePos = Vector3.zero;
            circlePos.x = Mathf.Cos(radians) * radius;
            circlePos.z = Mathf.Sin(radians) * radius;
        
            Debug.Log($"SPEED: {speed} START: {startingRotation} --- ANGLE: {Angle} --- TARGET: {targetRotation}", this);
        
            transform.position = pivot + circlePos;
            transform.rotation = Quaternion.Euler(0,-Angle,0);
            transform.forward *= -dir;
            
            yield return null;
        }

        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(transform.rotation.x, targetRotation, transform.rotation.z);
        
        _isTurning = false;
    }

    static float GetStartAngle(Vector3 from, Vector3 to) => M.Mod(-Vector3.SignedAngle(Vector3.right, (to - from).Flat().normalized, Vector3.up),360f);

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
            girdForwardValue = Mathf.Ceil(forwardValue / CityBuilder.cellSize) * CityBuilder.cellSize;
        else
            girdForwardValue = Mathf.Floor(forwardValue / CityBuilder.cellSize) * CityBuilder.cellSize;
        
        do
        {
            forwardValue = isInZ ? transform.position.z : transform.position.x;
            yield return null;
        } while (forwardValue < girdForwardValue);
        
        var startPosition = isInZ ?
            new Vector3(transform.position.x, transform.position.y, girdForwardValue) :
            new Vector3(girdForwardValue, transform.position.y, transform.position.z);
        transform.position = startPosition;
        
        var turnTargetPos = transform.position + transform.forward * CityBuilder.cellSize;
        var targetDistance = isInZ ? turnTargetPos.z : turnTargetPos.x;
        while (time < 1f)
        {
            transform.rotation = Quaternion.Lerp(startRotation, lookRotation, time);

            transform.position = isInZ ? 
                new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(startPosition.z, targetDistance, time)) : 
                new Vector3(Mathf.Lerp(startPosition.x, targetDistance, time), transform.position.y, transform.position.z);
            
            time += Time.deltaTime * _turnSpeed;
            _turnSpeed += Time.deltaTime * _acceleration;
            
            yield return null;
        }
        transform.rotation = Quaternion.Lerp(startRotation, lookRotation, 1);
        transform.position = isInZ ? 
            new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(startPosition.z, targetDistance, 1)) : 
            new Vector3(Mathf.Lerp(startPosition.x, targetDistance, 1), transform.position.y, transform.position.z);

        _isTurning = false;
    }

    bool IsMovingAlongZ => Mathf.Abs(transform.right.x) > Mathf.Abs(transform.right.z);


    bool CanSidestep(int dir)
    {
        var any = Physics.OverlapSphereNonAlloc(GetTurnTarget(dir), 0f, results, Block.layerMask);
        return any == 0;
    }static Collider[] results = new Collider[1];
    bool CanTurn(int dir)
    {
        return !Physics.Raycast(transform.NthPosition(2), transform.right * dir, CityBuilder.streetWidth, Block.layerMask);
    }

    Vector3 GetTurnTarget(int dir)
    {
        var startPosition = Grid.GetNextPosition(transform.position, transform.forward);
        var targetPosition = Grid.GetNextPosition(startPosition, transform.forward);
        targetPosition = Grid.GetNextPosition(targetPosition, transform.right * dir);
        return targetPosition;
    }
    
    Vector3 GetSidestepTarget(int dir)
    {
        return Grid.GetNextPosition(transform.position, transform.forward) + transform.right * (CityBuilder.cellSize * dir);
    }

    Vector3 GetTurnRayTarget(int dir) => TurnRayOrigin + transform.forward * _turnTargetForwardOffset + transform.right * (dir * CityBuilder.streetWidth);

    Vector3 GetSidestepRayTarget(int dir) => SidestepRayOrigin + transform.forward * _sidestepTargetForwardOffset + transform.right * (dir * CityBuilder.cellSize);

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Block")) GameManager.ResetCurrentScene();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Grid.GetNextPosition(transform.position, transform.forward), 2);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetTurnTarget(1), 2);
        Gizmos.DrawWireSphere(GetTurnTarget(-1), 2);
        Gizmos.color = Color.yellow;
        var rayFrom = Grid.GetNextPosition(Grid.GetNextPosition(transform.position, transform.forward), transform.forward);
        var rayToVector = transform.right * CityBuilder.streetWidth;
        Gizmos.DrawLine(rayFrom, rayFrom + rayToVector * 1);
        Gizmos.DrawLine(rayFrom, rayFrom + rayToVector * -1);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Grid.GetNextPosition(transform.position, transform.right), 1.5f);
        Gizmos.DrawWireSphere(Grid.GetNextPosition(transform.position, transform.right * -1), 1.5f);
    }
}
