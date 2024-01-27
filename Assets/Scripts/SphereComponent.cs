using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SphereTypesMap;

public class SphereComponent : MonoBehaviour {
    public Rigidbody rb;
    public Collider collider;
    public MeshRenderer meshRenderer;

    public SphereTypeEntry type;


    void Start() {
    }

    private void Update() {
        
    }

    public void SetSphereType (SphereTypeEntry type) {
        this.type = type;
        meshRenderer.material = type.material;
    }

    internal void GetGrabbed()
    {
        collider.enabled = false;
        rb.isKinematic = true;
    }

    internal void ReleaseGrab()
    {
        collider.enabled = true;
        rb.isKinematic= false;
    }
}
