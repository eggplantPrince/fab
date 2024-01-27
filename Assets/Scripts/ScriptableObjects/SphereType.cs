using UnityEngine;

[CreateAssetMenu(fileName = "sphereType.asset", menuName = "Scriptable Objects/Sphere Type")]
public class SphereTypeEntry : ScriptableObject
{
    public string typeName;
    public Material material;
    public Texture texture;
    public Color tint;
}
