using System.Collections.Generic;
using System.Linq;
using CityGeneration.Data;
using Extensions;
using Gameplay;
using Gameplay.Utils;
using UnityEngine;
using Grid = Gameplay.Utils.Grid;

namespace CityGeneration
{
    public class CityBuilder : MonoBehaviour
    {
        [SerializeField] SO_City _city;
        BlockBuilder _blockBuilder;

        Dictionary<Vector2, Block> aliveCells = new();
        
        Vector2[] _areaEndpoints = new Vector2[2];

        public GameObject mouse;
        
        [SerializeField]Transform _ground;


        void OnEnable()
        {
            _city.OnCityValidated += Init;
        }

        void Awake()
        {
            Init();
        }

        public void Init()
        {
            City.SetCity(_city);
            MapCalculator.Init();
            ShapeData.Init();
            QuadData.Init();
            MeshData.GenerateMeshes();
            StreetService.Init();
            _blockBuilder = new BlockBuilder();
            _blockBuilder.Init(transform);
            DestroyCells();
            SpawnCityAroundPlayer();
            _ground.position = new Vector3(ShapeData.B, 0, ShapeData.B);
            _ground.localScale = new Vector3(MapCalculator.ConvertValue * _city.area.x / 10, 1, MapCalculator.ConvertValue * _city.area.y / 10);
        }

        void Update()
        {
            PlayerPosition = mouse.transform.position;
            //print(QuadData.vertices);
            if(HasEnteredNewCell())
            {
                SpawnCityAroundPlayer();
            }
        }
        
        //TO DO
        /*
         * store map state
         * 
         * check if cell is inside area
         * check if cell is alive
         * if outside and alive -> kill
         * if inside and dead -> spawn
         * otherwise leave as is
         *
         * kill all alive cells outside area
         */


        void SpawnCityAroundPlayer()
        {
            //var correction = MapCalculator.ConvertValue % 2 == 0 ? Vector2.zero : Vector2.one / 2;
            var offset = _city.area/2;
            _areaEndpoints[0] = PlayerCell - offset;
            _areaEndpoints[1] = PlayerCell + _city.area - offset;
            
            foreach (var key in aliveCells.Keys.Where(ShouldKill))
                aliveCells[key].Kill();
            
            for (var y = (int)_areaEndpoints[0].y; y <= (int)_areaEndpoints[1].y; y++)
            {
                for (var x = (int)_areaEndpoints[0].x; x <= (int)_areaEndpoints[1].x; x++)
                {
                    var cell = new Vector2(x, y);
                    
                    if (!IsCellInsideArea(cell)) continue;
                    
                    if(!IsCellAlive(cell))
                        aliveCells.Add(cell, _blockBuilder.BuildBlock(cell));
                    else
                        aliveCells[cell].Rise();
                }
            }
        }

        void DestroyCells()
        {
            foreach (var cell in aliveCells.Values)
                cell.Destroy();
            aliveCells.Clear();
        }

        bool ShouldKill(Vector2 key)
        {
            return IsCellAlive(key) && !IsCellInsideArea(key);
        }
        
        bool IsCellInsideArea(Vector2 cell)
        {
            return cell.x >= _areaEndpoints[0].x &&
                   cell.x <= _areaEndpoints[1].x &&
                   cell.y >= _areaEndpoints[0].y &&
                   cell.y <= _areaEndpoints[1].y; 
        }

        bool IsCellAlive(Vector2 cell)
        {
            return aliveCells.ContainsKey(cell);
        }

        Vector2 _currCell;
        Vector2 _prevCell;

        bool HasEnteredNewCell()
        {
            if (PlayerCell != _prevCell)
            {
                _prevCell = PlayerCell;
                return true;
            }
            return false;
        }

        public static bool IsInsideBlock(Vector3 position)
        {
            return Physics.OverlapSphereNonAlloc(position, 1, _results, Block.layerMask) > 0;
        }static Collider[] _results = new Collider[1];

        public static Vector2 PlayerCell => MapCalculator.WorldToCell(PlayerPosition);
        static Vector3 PlayerPosition { get; set; }
    }
}
