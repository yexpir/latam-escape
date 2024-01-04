using System;
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
    [SerializeField] float setCityWidth;
    [SerializeField] float setCityLength;
    [SerializeField] float setBlockUnit;
    [SerializeField] float setStreetWidth;
    [SerializeField] float setSidesetSize;
    public static float cityWidth;
    public static float cityLength;
    public static float blockUnit;
    public static float streetWidth;
    public static float stepsideSize;
    float offset;
    Vector3 startPosition = Vector3.zero;
    Vector3 spawnPosition;
    [SerializeField] LayerMask layerMask;


    Vector3[] spheres = new Vector3[4];

    void Awake()
    {
        cityWidth = setCityWidth;
        cityLength = setCityLength;
        blockUnit = setBlockUnit;
        streetWidth = setStreetWidth;
        stepsideSize = setStreetWidth/3;
    }

    void Start()
    {
        offset = setBlockUnit + streetWidth;
        setCityWidth *= offset;
        setCityLength *= offset;
        if (randomSeed) seed = Random.Range(100000, 1000000);
        Random.InitState(seed);

        var longUnit = streetWidth + setBlockUnit * 2;
        var pivotOffset = (longUnit - setBlockUnit) / 2;
        startPosition.x = pivotOffset;
        startPosition.z = pivotOffset;
        spawnPosition = startPosition;
        foreach (var block in (Blocks[]) Enum.GetValues(typeof(Blocks)))
        {
            switch (block)
            {
                case Blocks.Reg :
                    blocksPrefabs[(int) block].cube.localScale = new Vector3(setBlockUnit, setBlockUnit, setBlockUnit);
                    blocksPrefabs[(int) block].cube.localPosition = new Vector3(0, setBlockUnit / 2, 0);
                    break;
                case Blocks.Big :
                    blocksPrefabs[(int) block].cube.localScale = new Vector3(longUnit, setBlockUnit, longUnit);
                    blocksPrefabs[(int) block].cube.localPosition = new Vector3(pivotOffset, setBlockUnit / 2, pivotOffset);
                    break;
                case Blocks.Hor :
                    blocksPrefabs[(int) block].cube.localScale = new Vector3(longUnit, setBlockUnit, setBlockUnit);
                    blocksPrefabs[(int) block].cube.localPosition = new Vector3(pivotOffset, setBlockUnit / 2, 0);
                    break;
                case Blocks.Ver :
                    blocksPrefabs[(int) block].cube.localScale = new Vector3(setBlockUnit, setBlockUnit, longUnit);
                    blocksPrefabs[(int) block].cube.localPosition = new Vector3(0, setBlockUnit / 2, pivotOffset);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    void Update()
    {
        if (spawnPosition.z > setCityLength) return;
        
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

        if (spawnPosition.x >= setCityWidth)
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

public enum Blocks
{
    Reg,
    Big,
    Hor,
    Ver
}
