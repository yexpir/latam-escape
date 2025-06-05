using System;
using Gameplay;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] Player _player;
    void OnEnable() => _player.OnIntersectionReached.action += UpdateGround;
    void OnDisable() => _player.OnIntersectionReached.action -= UpdateGround;
    void UpdateGround() => transform.position = _player.state.currentChunkPosition;
}
