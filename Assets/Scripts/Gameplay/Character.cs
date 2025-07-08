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
        public Vector3 velocity;

        public Transform pointer;

        protected virtual void OnEnable()
        {
            OnChunkCrossed.action += OnIntersectionReached.Raise;
            OnOrientationChanged.action += OnIntersectionReached.Raise;
        }

        protected virtual void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            hitbox = GetComponent<CapsuleCollider>();
            meshFilter = GetComponentInChildren<MeshFilter>();
        }

        void Start()
        {
            state = new CharacterState(this, pointer);
        }

        protected virtual void Update()
        {
            if (Physics.Raycast(transform.position+Vector3.up, Vector3.down, out var hit, data.feetSize+1, 1<<9))
            {
                state.isGrounded = true;
                print($"IsGrounded: {state.isGrounded} {hit.transform.gameObject.name}");
            }
            else
            {
                state.isGrounded = false;
                print($"IsGrounded: {state.isGrounded}");
            }
            Move(velocity);
        }

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
}