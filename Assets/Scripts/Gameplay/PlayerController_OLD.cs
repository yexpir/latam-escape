using System.Collections;
using CityGeneration;
using CityGeneration.Data;
using Extensions;
using Gameplay.Data;
using Gameplay.InputHandling;
using Gameplay.Utils;
using UnityEngine;
using Grid = Gameplay.Utils.Grid;

namespace Gameplay
{
    public class PlayerController_OLD : MonoBehaviour
    {
        public static PlayerController_OLD Instance { get; private set; }
        
        SO_City _city;
        SO_Player _playerData;
        
        bool _isSideSteping;
        bool _isTurning;

        RaycastHit _hitR;
        RaycastHit _hitL;

        public Vector3 Position => transform.position;

        bool IsMovingAlongZ => Mathf.Abs(transform.right.x) > Mathf.Abs(transform.right.z);

        public float Angle
        {
            get => _angle;
            set => _angle = M.Mod(value, 360f);
        } float _angle;

        float _forwardSpeedBase;
        float _turnSpeedBase;
    
        void Awake()
        {
            if(Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;

            _city = City.city;
            
            _playerData.turnSpeed *= 100;

            _forwardSpeedBase = _playerData.forwardSpeed;
            _turnSpeedBase = _playerData.turnSpeed;
        }

        void OnValidate()
        {
            _angle = M.Mod(_angle, 360);
        }

        void Update()
        {
            transform.position += transform.forward * (_playerData.forwardSpeed * Time.deltaTime);
            
            if(_isSideSteping) return;
            if (_isTurning) return;
        
            // if (In.RightPressed || In.LeftPressed)
            // {
            //     StartCoroutine(Turn(In.XInt));
            //     StartCoroutine(SideStep(In.XInt));
            // }

            var accel = /*CoinSpawner.CoinCount * 0.1f*/0;
            _playerData.forwardSpeed = _forwardSpeedBase + accel;
            _playerData.turnSpeed = _turnSpeedBase + accel;
        }
        
        IEnumerator SideStep(int direction)
        {
            if (_isTurning) yield break;
            if (!CanSidestep(direction)) yield break;
            _isSideSteping = true;
            float currDistance;
            var prevDistance = Grid.CellSize;
            Vector3 targetSidePos;
        
            if (IsMovingAlongZ)
            {
                targetSidePos = transform.position + transform.right * (Grid.CellSize * direction);
                while (true)
                {
                    transform.position += transform.right * (direction * _playerData.sideStepSpeed * Time.deltaTime);
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
                targetSidePos = transform.position + transform.right * (Grid.CellSize * direction);
                while (true)
                {
                    transform.position += transform.right * (direction * _playerData.sideStepSpeed * Time.deltaTime);
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

            var targetRotation = M.Mod(startingRotation + 90f * -dir, 360f);
            
            var targetPosition = GetTurnTarget(dir);
            var targetForward = transform.right * dir;
        
            while (!Grid.HasPassedPosition(transform, startingPosition)) yield return null;

            transform.position = startingPosition;
            transform.rotation = Quaternion.Euler(transform.rotation.x, startingRotation, transform.rotation.z);

            Angle = startingRotation;
        
            //Debug.Log($"START: {startingRotation} --- ANGLE: {Angle} --- TARGET: {targetRotation}", this);
            while (M.IsInRange(Angle, startingRotation, targetRotation))
            {
                Angle += Time.deltaTime * _playerData.turnSpeed * -dir;
            
                var radians = Angle * Mathf.Deg2Rad;

                var circlePos = Vector3.zero;
                circlePos.x = Mathf.Cos(radians) * Grid.CellSize;
                circlePos.z = Mathf.Sin(radians) * Grid.CellSize;
        
        
                transform.position = pivot + circlePos;
                transform.rotation = Quaternion.Euler(0,-Angle,0);
                transform.forward *= -dir;
            
                yield return null;
            }

            //transform.rotation = Quaternion.Euler(transform.rotation.x, targetRotation, transform.rotation.z);
            transform.position = targetPosition;
            transform.forward = targetForward;

            _isTurning = false;
        }

        static float GetStartAngle(Vector3 from, Vector3 to) => M.Mod(-Vector3.SignedAngle(Vector3.right, (to - from).Flatten().normalized, Vector3.up),360f);

        bool CanSidestep(int dir)
        {
            var any = Physics.OverlapSphereNonAlloc(GetTurnTarget(dir), 0f, results, Block.layerMask);
            return any == 0;
        }static Collider[] results = new Collider[1];
        bool CanTurn(int dir)
        {
            return !Physics.Raycast(transform.NthPosition(2), transform.right * dir, _city.mapData.totalStreetWidth, Block.layerMask);
        }

        Vector3 GetTurnTarget(int dir)
        {
            var startPosition = Grid.GetNextPosition(transform.position, transform.forward);
            var targetPosition = Grid.GetNextPosition(startPosition, transform.forward);
            targetPosition = Grid.GetNextPosition(targetPosition, transform.right * dir);
            return targetPosition;
        }
    
        void OnTriggerEnter(Collider other)
        {
            if(other.CompareLayer(Block.layerMask))
                GameManager.ResetCurrentScene();
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
            var rayToVector = transform.right * _city.mapData.totalStreetWidth;
            Gizmos.DrawLine(rayFrom, rayFrom + rayToVector * 1);
            Gizmos.DrawLine(rayFrom, rayFrom + rayToVector * -1);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Grid.GetNextPosition(transform.position, transform.right), 1.5f);
            Gizmos.DrawWireSphere(Grid.GetNextPosition(transform.position, transform.right * -1), 1.5f);
        }
    }
}
