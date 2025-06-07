using UnityEngine;

namespace Gameplay.Data
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Character")]
    public class SO_Player : ScriptableObject
    {
        public float forwardSpeed;
        public float sideStepSpeed;
        public float turnSpeed;
        public float acceleration;
        public float jumpForce;
        public float jumpGravity;
        public float fallGravity;
        public float maxFallSpeed;
        public float fallingThreshold;
    }
}