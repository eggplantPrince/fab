using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sphereTypesMap.asset", menuName = "Scriptable Objects/Sphere Type Map")]
public class SphereTypesMap : ScriptableObject
{
   public SphereTypeEntry[] typeList;
}