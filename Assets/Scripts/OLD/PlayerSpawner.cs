using System.Collections;
using Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] LayerMask _blockLayer;

    [SerializeField] CinemachineVirtualCamera _camera;
    Vector3 offset = Vector3.right * (CityBuilder.blockUnit + CityBuilder.streetWidth);
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
        spawnPosition.x += (CityBuilder.blockUnit + CityBuilder.streetWidth) * (int)(CityBuilder.cityWidth/2);
        for (var i = 0; i < 3; i++)
        {
            if (Physics.CheckSphere(spawnPosition + Vector3.forward * CityBuilder.streetWidth, 3, _blockLayer))
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
