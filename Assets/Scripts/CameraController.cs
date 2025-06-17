using System;
using System.Collections;
using System.Collections.Generic;
using Extensions;
using Gameplay;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Transform _follow;

    float _playerHeight;

    void Start()
    {
        _playerHeight = _player.transform.position.y;
    }

    void LateUpdate()
    {
        var position = _player.transform.position;

        position = _player.IsTurning ? _player.transform.position : position.ProjectWithSelector(_player.state.currentStreet.streetPosition, _player.state.currentRight);
        position.y = _playerHeight;
        
        _follow.transform.position = position;
        _follow.forward = _player.transform.forward;
    }
}
