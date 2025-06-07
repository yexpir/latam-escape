using UnityEngine;

namespace Gameplay.Abilities
{
    public class Grounding : Ability
    {
        [SerializeField] LayerMask _layerMask;
        [SerializeField] float _feetSize;
        Ray feet;
        void Update()
        {
            feet = new Ray(transform.position, Vector3.down);
            var maxFallSpeed = Mathf.Clamp(-_character.velocity.y, 0, _character.data.maxFallSpeed);
            var feetSize = _character.hitbox.radius + maxFallSpeed + _feetSize;
            var hasHit = Physics.Raycast(feet, out var hit, feetSize, _layerMask);
            print($"RAYCAST HIT: {hasHit} FEET SIZE: {feetSize}");
            if (hasHit)
            {
                if (!_character.isGrounded)
                {
                    transform.position = new Vector3(
                        transform.position.x,
                        hit.point.y + _character.hitbox.radius,
                        transform.position.z
                    );
                    _character.velocity.y = 0;
                    _character.isGrounded = true;
                    print($"GROUNDED {hit.collider.name}");
                }
            }
            else if (_character.isGrounded)
            {
                _character.isGrounded = false;
                print("AIRBORNE");
            }
        }
        
        void OnDrawGizmos()
        {
            var from = transform.position;
            var to = from + feet.direction * _feetSize;
            Gizmos.DrawLine(from, to);
        }
    }
}