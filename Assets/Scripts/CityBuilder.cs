using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CityBuilder : MonoBehaviour
{
    public List<Block> blocksPrefabs = new();
    List<Block> blocksValid = new();
    [SerializeField] bool randomSeed;
    [SerializeField] int seed;
    [SerializeField] int width;
    [SerializeField] int length;
    [SerializeField] int blockUnit;
    [SerializeField] int streetWidth;
    int offset;
    Vector3 startPosition = Vector3.zero;
    Vector3 spawnPosition;
    [SerializeField] LayerMask layerMask;


    Vector3[] spheres = new Vector3[4];
    
    void Start()
    {
        offset = blockUnit + streetWidth;
        width *= offset;
        length *= offset;
        startPosition.x = - (width + 15);
        spawnPosition = startPosition;
        if (randomSeed) seed = Random.Range(100000, 1000000);
        Random.InitState(seed);
    }

    void Update()
    {
        if (spawnPosition.z > length) return;
        
        if (CanSpawnBlock(spawnPosition))
        {
            var block = blocksValid[0];

            var max = 0;
            foreach (var b in blocksValid)
            {
                max += b.probability;
            }

            var rndNum = Random.Range(0, max);
            var prob = 0;
            foreach (var b in blocksValid)
            {
                prob += b.probability;
                if (rndNum >= prob) continue;
                block = b;
                break;
            }

            var newBlock = Instantiate(block, spawnPosition, Quaternion.identity, transform);
            newBlock.SetScale(Random.Range(0.5f, 1.0f));
            newBlock.SetColor(Random.ColorHSV());
        }
        
        spawnPosition.x += offset;

        if (spawnPosition.x >= width)
        {
            spawnPosition.x = startPosition.x;
            spawnPosition.z += offset;
        }
    }
    
    
    bool CanSpawnBlock(Vector3 pos)
    {
        blocksValid.Clear();

        spheres[0] = pos;
        spheres[1] = pos + Vector3.right * offset;
        spheres[2] = pos + Vector3.forward * offset;
        spheres[3] = pos + (Vector3.right + Vector3.forward) * offset;

        for (var i = 0; i < 4; i++)
        {
            if (Physics.CheckSphere(spheres[i], 5.0f, layerMask)) break;
            blocksValid.Add(blocksPrefabs[i]);
        }
        return blocksValid.Any();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var shift = Vector3.up * 25;
        Gizmos.DrawWireSphere(spheres[0] + shift, 5);
        Gizmos.DrawWireSphere(spheres[1] + shift, 5);
        Gizmos.DrawWireSphere(spheres[2] + shift, 5);
        Gizmos.DrawWireSphere(spheres[3] + shift, 5);
    }
}
