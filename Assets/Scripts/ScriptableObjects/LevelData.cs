using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "levelData.asset", menuName = "Scriptable Objects/Level Data")]
public class LevelData : ScriptableObject
{
    public float startingEntertainment;

    public SphereTypeEntry[] likes;

    public SphereTypeEntry[] hates;
}
