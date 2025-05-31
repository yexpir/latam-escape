using System;
using CityStuff.GenerationStuff;
using Extensions;
using Gameplay.Data;
using UnityEngine;
using Utils;

namespace Gameplay
{
    [RequireComponent(typeof(SphereCollider))]
    public class Character : MonoBehaviour
    {
        public readonly Raiser OnOrientationChanged = new();
        public readonly Raiser OnChunkCrossed = new();
        public readonly Raiser OnIntersectionReached = new();
        
        public SO_Player data;
        public CharacterState state;
        public new SphereCollider collider { get; private set; }
        
        public Transform pointer;

        void OnEnable()
        {
            OnChunkCrossed.action += OnIntersectionReached.Raise;
            OnOrientationChanged.action += OnIntersectionReached.Raise;
        }

        void Start()
        {
            collider = GetComponent<SphereCollider>();
            state = new CharacterState(this, pointer);
        }

        void Update()
        {
        }

        void LateUpdate()
        {
            if (state.HasEnteredNewChunk())
                OnChunkCrossed.Raise();
            if (state.HasChangedOrientation())
                OnOrientationChanged.Raise();
            print(state.intersection);
        }

        public void SetRadius(float radius)
        {
            if(radius <= 0.0f) return;
            collider.radius = radius;
        }

        public void SetState(CharacterState newState) => state = newState;

        public void Move(Vector3 movement) => transform.Move(movement);
    }
}