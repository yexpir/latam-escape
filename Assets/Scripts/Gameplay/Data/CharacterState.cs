using CityStuff.GenerationStuff;
using Extensions;
using UnityEngine;
using Utils;

namespace Gameplay.Data
{
    public class CharacterState
    {
        public Character character { get; }
        public Vector2Int currentChunkCoordinates;
        public Vector2Int prevChunkCoordinates;
        public Vector3 currentChunkPosition;
        public Vector3 currentForward;
        public Vector3 prevForward;
        public Vector3 currentRight;
        public Vector3 prevRight;
        public Vector3 intersection;
        public float fallingSpeed;

        public readonly Street currentStreet;
        public readonly Street nextStreet;
        public int currentLaneIndex { get; private set; }
        
        Transform pointer;

        public CharacterState(Character newCharacter, Transform newPointer)
        {
            character = newCharacter;
            
            prevChunkCoordinates = Vector2Int.down;
            
            currentForward = Vector3.forward;
            prevForward = Vector3.zero;
            
            currentRight = Vector3.right;
            prevRight = Vector3.zero;
            
            intersection = Vector3.down;
            
            currentStreet = new Street();
            nextStreet = new Street();
            
            currentLaneIndex = City.map.laneCount / 2;
            
            pointer = newPointer;

            character.OnIntersectionReached.action += SetStreets;
        }

        public void SetStreets()
        {
            SetOrientation();

            currentStreet.Set(currentRight, StreetService.GetClosestStreet(character.transform.position, currentRight));
            nextStreet.Set(currentForward, StreetService.GetClosestStreetInDirection(character.transform.position + character.state.currentForward * City.map.laneWidth, currentForward));

            intersection = currentStreet.streetPosition.Project(nextStreet.streetPosition);
            pointer.position = intersection;
        }
        public void SetOrientation()
        {
            currentForward = character.transform.forward.Round();
            currentRight = character.transform.right.Round();
        }

        public void SetCurrentLaneIndex(int index)
        {
            currentLaneIndex = index;
        }

        public override string ToString()
        {
            return $"FORWARD DIRECTION> {currentForward}\n" +
                   $"";
        }
        
        public bool HasEnteredNewChunk()
        {
            currentChunkCoordinates = MapCalculator.WorldToCell(character.transform.position);
            currentChunkPosition = MapCalculator.CellToWorld(currentChunkCoordinates);
            if (currentChunkCoordinates != prevChunkCoordinates)
            {
                prevChunkCoordinates = currentChunkCoordinates;
                return true;
            }
            return false;
        }

        public bool HasChangedOrientation()
        {
            if (currentForward == prevForward) return false;
            prevForward = currentForward;
            prevRight = currentRight;
            return true;
        }
    }
}