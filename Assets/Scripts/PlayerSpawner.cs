using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] LayerMask _blockLayer;

    [SerializeField] CinemachineVirtualCamera _camera;
    Vector3 offset = Vector3.right * 30;
    void Start()
    {
        StartCoroutine(SpawnPlayerRoutine());
    }

    IEnumerator SpawnPlayerRoutine()
    {
        yield return new WaitForSeconds(3);
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        var spawnPosition = Vector3.zero;
        for (var i = 0; i < 3; i++)
        {
            if (Physics.CheckSphere(spawnPosition, 3, _blockLayer))
            {
                spawnPosition += offset;
            }
        }

        var player =  Instantiate(_player, spawnPosition+Vector3.up, Quaternion.identity);
        _camera.Follow = player.transform;
        _camera.LookAt = player.transform;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Vector3.zero, 3);
    }
}
