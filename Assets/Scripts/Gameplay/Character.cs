using CityGeneration.Data;
using Extensions;
using Gameplay.Data;
using Gameplay.Utils;
using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(SphereCollider))]
    public class Character : MonoBehaviour
    {
        public readonly Raiser OnStreetCrossed = new();
        
        public CharacterState state;
        public SO_Player data;
        public SO_Map map;
        public new SphereCollider collider { get; private set; }
        
        public Transform pointer;
        
        void Awake() => collider = GetComponent<SphereCollider>();

        void Start() => state = new CharacterState(this, pointer);

        void LateUpdate()
        {
            if(state.HasEnteredNewBlock() || state.HasChangedOrientation())
                OnStreetCrossed.Raise();
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