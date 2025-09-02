using Cinemachine;
using Extensions;
using Gameplay;
using UnityEngine;
using Utils;

public class CameraController : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] Transform _follow;
    [SerializeField] float _smoothTime;
    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    Vector3 _trackOffset;
    Vector3 _shoulderOffset;
    
    Vector3 _velocity = Vector3.zero;

    float _playerHeight;

    Cinemachine3rdPersonFollow _body;
    CinemachineComposer _composer;

    void Awake()
    {
        _body = _virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        _composer = _virtualCamera.GetCinemachineComponent<CinemachineComposer>();
    }

    void Start()
    {
        _playerHeight = _player.transform.position.y;
        _trackOffset = _composer.m_TrackedObjectOffset;
        _shoulderOffset = _body.ShoulderOffset;
    }

    void LateUpdate()
    {
        var position = _player.transform.position;
        if (_player.state.isGrounded)
            _playerHeight = _player.state.groundHit.y;
        else if (_player.transform.position.y < _playerHeight)
            _playerHeight = _player.transform.position.y;
        position.y = _playerHeight;
        
        _follow.transform.position = position;
        _follow.forward = _player.transform.forward;

        var laneIndex = _player.state.currentLaneIndex;
        
        var direction = (int)_player.state.currentForward.Max();
        var forwardAxis = _player.state.currentForward.Abs();

        if (forwardAxis == Vector3.forward && direction == -1 || forwardAxis == Vector3.right && direction == 1)
            laneIndex = City.map.laneCount - (laneIndex + 1);
        
        var shoulder = 1 - 2 * laneIndex / (City.map.laneCount-1);
        
        var trackOffset = _trackOffset;
        trackOffset.x = shoulder;
        _composer.m_TrackedObjectOffset = Vector3.SmoothDamp(_composer.m_TrackedObjectOffset, trackOffset, ref _velocity, _smoothTime);

        var shoulderOffset = _shoulderOffset;
        shoulderOffset.x = shoulder;
        _body.ShoulderOffset = Vector3.SmoothDamp(_body.ShoulderOffset, shoulderOffset, ref _velocity, _smoothTime);
    }
}