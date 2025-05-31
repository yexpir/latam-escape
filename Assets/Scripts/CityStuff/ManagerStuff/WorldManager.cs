using System;
using CityStuff.ConfigurationStuff;
using CityStuff.GenerationStuff;
using CityStuff.GenerationStuff.BlockGenerationStuff.Mesh.Data;
using CityStuff.PoolStuff;
using CityStuff.SpawnerStuff;
using CityStuff.WorldDataStuff;
using Gameplay;
using UnityEngine;
using Utils;

namespace CityStuff.ManagerStuff
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] SO_City _city;
        [SerializeField] Player _player;
        
        WorldData _worldData;
        SpawnMasker _masker;

        void OnEnable()
        {
            _player.OnChunkCrossed.action += UpdateWorld;
        }

        void Awake()
        {
            Init();
            
            _worldData = new WorldData();
            _masker = new SpawnMasker(_player);
        }
        
        void UpdateWorld() => WorldSpawner.Spawn(_worldData, _masker, transform);

        void Init()
        {
            City.SetCity(_city);
            MapCalculator.Init();
            ShapeData.Init();
            QuadData.Init();
            MeshData.Init();
            StreetService.Init();
            PoolerMain.Init();
            Palette.Init();
        }
        
        /*void OnDrawGizmos()
        {
            if (_masker is null) return;
            
            Gizmos.color = Color.magenta;
            foreach (var chunk in _worldData.activeChunks)
                Gizmos.DrawSphere(_worldData.GetChunkData(chunk).position + Vector3.up*50, 15);
        }*/
    }
}