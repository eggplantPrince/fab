using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sphereTypes.asset", menuName = "Scriptable Objects/Sphere Type Map")]
public class SphereTypesMap : ScriptableObject
{
   [System.Serializable]
   public class SphereTypeEntry
   {
       public string typeName;
       public Material material;
   }

   public SphereTypeEntry[] typeList;

}
