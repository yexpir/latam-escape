using System;
using Actions_Stuff;
using UnityEngine;
using WIP;

[Serializable]
[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public float travelSpeed;
    public float sidestepSpeed;
    public float turnSpeed;
    public float acceleration;
    public float circumferencePoint = Mathf.PI * 2;
}