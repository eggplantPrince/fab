using UnityEngine;

public class SphereComponent : MonoBehaviour {
    public Rigidbody rb;
    public Collider coll;
    public MeshRenderer contentMeshRenderer;
    public MeshRenderer bubbleTintRenderer;

    public SphereTypeEntry type;

    public bool currentlyGrabbed;

    public void SetSphereType(SphereTypeEntry type) {
        this.type = type;
        contentMeshRenderer.material.SetTexture("_BaseMap", type.texture);
        contentMeshRenderer.material.SetTexture("_EmissionMap", type.texture);
        Color.RGBToHSV(type.tint, out float h, out float s, out float v);
        v *= 0.05f;
        Color tintColor = Color.HSVToRGB(h, s, v);
        bubbleTintRenderer.material.SetColor("_EmissionColor", tintColor);
    }

    internal bool GetGrabbed()
    {
        if(coll != null)
        {
            coll.enabled = false;
        }
        if(rb != null)
        {
            rb.isKinematic = true;
        }

        currentlyGrabbed = true;

        return coll != null && rb != null;
    }

    internal bool ReleaseGrab()
    {
        if(coll != null)
        {
            coll.enabled = true;
        }
        if(rb != null)
        {
            rb.isKinematic= false;
        }

        currentlyGrabbed = false;

        return coll != null && rb != null;
    }
}
