using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CityBuilder : MonoBehaviour
{
    public List<Block> blockPrefabs = new();
    List<Block> validBlocks = new();

    [SerializeField] LayerMask layerMask;

    [SerializeField] bool randomSeed;
    [SerializeField] int manualSeed;

    [SerializeField] float setCityWidth;
    [SerializeField] float setCityLength;
    [SerializeField] float setBlockUnit;
    [SerializeField] float setStreetWidth;
    [SerializeField] float setCellSize;

    public static float cityWidth;
    public static float cityLength;
    public static float blockUnit;
    public static float streetWidth;
    public static float cellSize;

    float offset;
    Vector3 startPosition = Vector3.zero;
    Vector3 spawnPosition;


    readonly Vector3[] checkers = new Vector3[4];

    void Awake()
    {
        cityWidth = setCityWidth;
        cityLength = setCityLength;
        blockUnit = setBlockUnit;
        streetWidth = setStreetWidth;
        cellSize = setStreetWidth/3;
    }

    void Start()
    {
        if (randomSeed) manualSeed = Random.Range(100000, 1000000);
        Random.InitState(manualSeed);

        offset = blockUnit + streetWidth;
        setCityWidth *= offset;
        setCityLength *= offset;

        var longUnit = streetWidth + blockUnit * 2;
        var pivotOffset = (longUnit - blockUnit) / 2;

        startPosition.x = pivotOffset;
        startPosition.z = pivotOffset;
        spawnPosition = startPosition;

        foreach (var block in (Blocks[]) Enum.GetValues(typeof(Blocks)))
        {
            var b = blockPrefabs[(int)block];
            b.Init(blockUnit, longUnit, pivotOffset);
            switch (block)
            {
                case Blocks.Reg :
                    b.Regular();
                    break;
                case Blocks.Big :
                    b.Big();
                    break;
                case Blocks.Hor :
                    b.Horizontal();
                    break;
                case Blocks.Ver :
                    b.Vertical();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        while (spawnPosition.z < setCityLength)
        {
            if (CanSpawnBlock(spawnPosition))
            {
                var block = validBlocks[0];

                var max = validBlocks.Sum(b => b.probability);

                var rndNum = Random.Range(0, max);
                var prob = 0;
                foreach (var b in validBlocks)
                {
                    prob += b.probability;
                    if (rndNum >= prob) continue;
                    block = b;
                    break;
                }

                var newBlock = Instantiate(block, spawnPosition, Quaternion.identity, transform);
                //newBlock.SetScale(Random.Range(0.5f, 1.0f));
                newBlock.SetScale(1);
                newBlock.SetRandomSolor();
            }
            
            spawnPosition.x += offset;

            if (spawnPosition.x >= setCityWidth)
            {
                spawnPosition.x = startPosition.x;
                spawnPosition.z += offset;
            }
        }
    }

    
    bool CanSpawnBlock(Vector3 pos)
    {
        validBlocks.Clear();

        SetAvailabilityCheckers(pos);

        for (var i = 0; i < 4; i++)
        {
            if (checkers[3].x >= setCityWidth || checkers[3].z >= setCityLength) break;
            if (Physics.CheckSphere(checkers[i], 5.0f, layerMask)) break;
            validBlocks.Add(blockPrefabs[i]);
        }
        return validBlocks.Any();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var shift = Vector3.up * 25;

        foreach (var c in checkers)
            Gizmos.DrawWireSphere(c + shift, 5);
    }
    void SetAvailabilityCheckers(Vector3 pos)
    {
        checkers[0] = pos;
        checkers[1] = pos + Vector3.right * offset;
        checkers[2] = pos + Vector3.forward * offset;
        checkers[3] = pos + (Vector3.right + Vector3.forward) * offset;
    }
}

public enum Blocks
{
    Reg,
    Big,
    Hor,
    Ver
}
