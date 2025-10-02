using System;
using CityStuff.PrefabStuff;
using Extensions;
using Gameplay.Data;
using UnityEngine;
using Utils;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        public readonly Raiser OnOrientationChanged = new();
        public readonly Raiser OnChunkCrossed = new();
        public readonly Raiser OnIntersectionReached = new();
        
        public SO_Player data;
        public CharacterState state;
        public CapsuleCollider hitbox { get; private set; }
        public MeshFilter meshFilter { get; private set; }
        public Rigidbody rigidBody;

        public Vector3Property vector3;

        

        public Transform actor;

        protected virtual void OnEnable()
        {
            OnChunkCrossed.action += OnIntersectionReached.Raise;
            OnOrientationChanged.action += OnIntersectionReached.Raise;
            vector3.OnVector3Change.action += UpdateCharacter;
        }

        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            hitbox = GetComponent<CapsuleCollider>();
            meshFilter = GetComponentInChildren<MeshFilter>();
        }

        void Start()
        {
            state = new CharacterState(this, transform);
        }

        public virtual void UpdateCharacter()
        {
            Physics.Raycast(state.nextCenter, Vector3.down, out var hit, Mathf.Infinity, 1<<9);
            state.groundHit = hit.point;
            state.isGrounded = Vector3.Distance(transform.position, state.groundHit) < data.feetSize;
        }

        bool _previousValue;

        protected virtual void LateUpdate()
        {
            if (state.HasEnteredNewChunk())
                OnChunkCrossed.Raise();
            if (state.HasChangedOrientation())
                OnOrientationChanged.Raise();
        }

        public void SetRadius(float radius)
        {
            if(radius <= 0.0f) return;
            hitbox.radius = radius;
        }

        public void SetState(CharacterState newState) => state = newState;

        public void Move(Vector3 movement)
        {
            transform.Move(movement);
        }
    }

    [Serializable]
    public class Vector3Property
    {
        [SerializeField] Vector3 _vector;
        public readonly Raiser OnVector3Change = new();

        public Vector3 vector
        {
            get => _vector;
            set
            {
                _vector = value;
                OnVector3Change.Raise();
            }
        }

        public float X
        {
            get => _vector.x;
            set
            {
                _vector.x = value;
                OnVector3Change.Raise();
            }
        }

        public float Y
        {
            get => _vector.y;
            set
            {
                _vector.y = value;
                OnVector3Change.Raise();
            }
        }

        public float Z
        {
            get => _vector.z;
            set
            {
                _vector.z = value;
                OnVector3Change.Raise();
            }
        }
    }
}