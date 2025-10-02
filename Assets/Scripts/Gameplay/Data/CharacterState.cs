using System;
using CityStuff.GenerationStuff;
using Extensions;
using UnityEngine;
using Utils;
namespace Gameplay.Data
{
    [Serializable]
    public class CharacterState
    {
        public Character character { get; }
        public Vector2Int currentChunkCoordinates;
        public Vector2Int prevChunkCoordinates;
        public Vector3 currentChunkPosition;
        public Vector3 currentPosition => character.transform.position;
        public Vector3 nextPosition => character.transform.position + character.vector3.vector;
        public Vector3 currentCenter => currentPosition + character.hitbox.center;
        public Vector3 nextCenter => nextPosition + character.hitbox.center;
        public Vector3 currentForward;
        public Vector3 prevForward;
        public Vector3 currentRight;
        public Vector3 prevRight;
        public Vector3 intersection;
        public Vector3 groundHit;
        public float fallSpeed;
        public float fallCurve;

        public readonly Street currentStreet;
        public readonly Street nextStreet;
        public int currentLaneIndex { get; private set; }
        
        Transform _actor;
        public bool isGrounded;
        public bool isLanding;
        public bool isFalling;
        
        public CharacterState(Character newCharacter, Transform newActor)
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
            
            _actor = newActor;

            character.OnIntersectionReached.action += SetStreets;
        }


        public void SetStreets()
        {
            SetOrientation();

            currentStreet.Set(currentRight, StreetService.GetClosestStreet(character.transform.position, currentRight));
            nextStreet.Set(currentForward, StreetService.GetClosestStreetInDirection(character.transform.position + character.state.currentForward * City.map.laneWidth, currentForward));

            intersection = currentStreet.streetPosition.Project(nextStreet.streetPosition);
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
            return $"currentChunkCoordinates: {currentChunkCoordinates}\n" +
                   $"currentChunkPosition: {currentChunkPosition}\n" +
                   $"currentForward: {currentForward}\n" +
                   $"intersection: {intersection}\n" +
                   $"groundHit: {groundHit}\n" +
                   $"isGrounded: {isGrounded}\n";
        }
        
        public bool HasEnteredNewChunk()
        {
            currentChunkCoordinates = MapCalculator.WorldToCell(character.transform.position);
            currentChunkPosition = MapCalculator.CellToWorld(currentChunkCoordinates);
            if (currentChunkCoordinates == prevChunkCoordinates) return false;
            prevChunkCoordinates = currentChunkCoordinates;
            return true;
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