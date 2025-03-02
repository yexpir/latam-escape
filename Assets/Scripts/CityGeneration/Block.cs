using UnityEngine;

namespace CityGeneration
{
    [RequireComponent(typeof(MeshCollider), typeof(MeshRenderer), typeof(MeshFilter))]
    public class Block : MonoBehaviour
    {
        MeshFilter _meshFilter;
        MeshCollider _collider;
        MeshRenderer _renderer;
        float _height;
        Color _color;
        Texture2D _texture;

        public const int layerIndex = 8;
        public static LayerMask layerMask = 1<<layerIndex;

        void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _collider = GetComponent<MeshCollider>();
            _renderer = GetComponent<MeshRenderer>();
            gameObject.layer = layerIndex;
        }

        public bool ShowID;

        void Update()
        {
            if (ShowID)
            {
                print(_meshFilter.sharedMesh.GetInstanceID());
                ShowID = false;
            }
        }

        public void Init(Mesh mesh, Color color, float height, string newName)
        {
            _meshFilter.sharedMesh = mesh;
            _collider.sharedMesh = mesh;
            _color = color;
            _renderer.material.color = color;
            _height = height;
            var scale = transform.localScale;
            scale.y = height;
            transform.localScale = scale; 
            name = newName;
            gameObject.layer = layerIndex;
        }

        public void Kill()
        {
            // for (var i = 0; i < transform.childCount; i++)
            // {
            //     Destroy(gameObject.transform.GetChild(i));
            // }
            gameObject.SetActive(false);
        }

        public void Rise()
        {
            gameObject.SetActive(true);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
        

    }
}