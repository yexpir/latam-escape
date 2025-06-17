using CityStuff.GenerationStuff;
using CityStuff.GenerationStuff.BlockGenerationStuff.Mesh.Data;
using CityStuff.PoolStuff.PrefabStuff.BaseObjectStuff;
using CityStuff.WorldDataStuff;
using Extensions;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace CityStuff.PoolStuff.PrefabStuff
{
    [RequireComponent(typeof(MeshCollider), typeof(MeshRenderer), typeof(MeshFilter))]
    public class Block : WorldObject
    {
        MeshFilter _meshFilter;
        MeshCollider _collider;
        MeshRenderer _renderer;
        Material _material;
        float _height;
        Color _color;
        Texture2D _texture;

        public const int layerIndex = 8;
        public static LayerMask layerMask = 1<<layerIndex;

        public bool ShowID;
        static readonly int _emissiveColorID = Shader.PropertyToID("_EmissionColor");
        const string _emissionKeyword = "_EMISSION";
        
        void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _collider = GetComponent<MeshCollider>();
            _renderer = GetComponent<MeshRenderer>();
            _collider.convex = true;
            _collider.isTrigger = true;
            _material = _renderer.material;
            gameObject.layer = layerIndex;
        }

        void Update()
        {
            if (ShowID)
            {
                print(_meshFilter.sharedMesh.GetInstanceID());
                ShowID = false;
            }
        }

        public override void Init(ObjectData newData, Transform parent)
        {
            base.Init(newData, parent);

            var bitmask = MapCalculator.GetCellBitmask(data.coordinates);
            var mesh = MeshData.GetMesh(bitmask);
            var color = Palette.colors[MapCalculator.GetHeight(data.coordinates)];
            var height = Mathf.RoundToInt(Mathf.Pow(MapCalculator.GetHeight(data.coordinates) + 1, City.map.heightPow) * City.map.heightMult);
            
            Init(mesh, color, height, bitmask.BitToString());
        }

        public void Init(Mesh mesh, Color color, float height, string newName)
        {
            _meshFilter.sharedMesh = mesh;
            _collider.sharedMesh = mesh;
            
            _color = color;
            _material.color = color;
            
            _height = height;
            name = newName;
            
            _material.EnableKeyword(_emissionKeyword);
            _material.SetColor(_emissiveColorID, _color);
            var scale = transform.localScale;
            scale.y = _height;
            transform.localScale = scale;
            gameObject.layer = layerIndex;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}