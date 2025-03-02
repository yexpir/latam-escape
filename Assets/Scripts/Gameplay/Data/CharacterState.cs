using CityGeneration;
using Extensions;
using Gameplay.Utils;
using TMPro;
using UnityEngine;

namespace Gameplay.Data
{
    public class CharacterState
    {
        public Character character { get; }
        
        public Vector3 forward { get; private set; }
        public Vector3 right { get; private set; }
        public int currentLaneIndex { get; private set; }

        public readonly Street currentStreet;
        public readonly Street nextStreet;
        
        public Vector3 intersection;

        Vector3 block;
        Vector3 prevBlock;

        Vector3 prevForward;
        
        Transform pointer;
        public CharacterState(Character newCharacter, Transform pointer)
        {
            character = newCharacter;
            currentLaneIndex = City.map.laneCount / 2;
            
            forward = Vector3.forward;
            right = Vector3.right;
            
            currentStreet = new Street();
            nextStreet = new Street();
            
            intersection = Vector3.zero;
            
            block = Vector3.zero;
            prevBlock = Vector3.zero;
            
            prevForward = Vector3.zero;

            this.pointer = pointer;

            character.OnStreetCrossed.action += SetStreets;
        }

        public void SetStreets()
        {
            SetOrientation(character.transform);
            
            currentStreet.Set(right, StreetService.GetClosestStreet(character.transform.position, right));
            nextStreet.Set(forward, StreetService.GetClosestStreetInDirection(character.transform.position + character.state.forward * character.map.laneWidth, forward));

            intersection = currentStreet.streetPosition.Project(nextStreet.streetPosition);
            pointer.position = intersection;
        }
        public void SetOrientation(Transform transform)
        {
            forward = transform.forward.Round();
            right = transform.right.Round();
        }

        public void SetCurrentLaneIndex(int index)
        {
            currentLaneIndex = index;
        }

        public bool HasEnteredNewBlock()//this shouldn't be here probably XD
        {
            block = character.transform.position.Floor(City.map.blockSize);
            if (block == prevBlock) return false;
            prevBlock = block;
            return true;
        }

        public bool HasChangedOrientation()
        {
            if (forward == prevForward) return false;
            prevForward = forward;
            return true;
        }

        public override string ToString()
        {
            return $"FORWARD DIRECTION> {forward}\n" +
                   $"";
        }
    }
}