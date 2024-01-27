using UnityEngine;

public class SphereComponent : MonoBehaviour {
    public Rigidbody rb;
    public Collider coll;
    public MeshRenderer contentMeshRenderer;
    public MeshRenderer bubbleTintRenderer;

    public SphereTypeEntry type;


    void Start() {
        if (type != null) {
            SetSphereType(type);
        }
    }

    public void SetSphereType(SphereTypeEntry type) {
        this.type = type;
        contentMeshRenderer.material.SetTexture("_BaseMap", type.texture);
        contentMeshRenderer.material.SetTexture("_EmissionMap", type.texture);
        Color.RGBToHSV(type.tint, out float h, out float s, out float v);
        v *= 0.05f;
        Color tintColor = Color.HSVToRGB(h, s, v);
        bubbleTintRenderer.material.SetColor("_EmissionColor", tintColor);
    }

    internal void GetGrabbed()
    {
        coll.enabled = false;
        rb.isKinematic = true;
    }

    internal void ReleaseGrab()
    {
        coll.enabled = true;
        rb.isKinematic= false;
    }
}
