using CityStuff.ConfigurationStuff;
using CityStuff.GenerationStuff;
using CityStuff.GenerationStuff.BlockGenerationStuff.Mesh.Data;
using CityStuff.PoolStuff;
using CityStuff.PrefabStuff;
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

        [SerializeField] bool spawnWorld;
        [SerializeField] bool enableLogs = true;

        void OnValidate()
        {
            if(enableLogs)
                MyLogger.EnableLogger();
            else
                MyLogger.DisableLogger();
        }

        void OnEnable() => _player.OnChunkCrossed.action += UpdateWorld;

        void Awake() => Init();

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
            IDManager.Init();
            
            _city.segmentSet.Init();
            
            _worldData = new WorldData();
            _masker = new SpawnMasker(_player);
        }
        
        void UpdateWorld()
        {
            if (!spawnWorld) return;
            WorldSpawner.Spawn(_worldData, _masker, transform);
        }
    }
}