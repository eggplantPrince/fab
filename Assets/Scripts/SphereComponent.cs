using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SphereTypesMap;

public class SphereComponent : MonoBehaviour {
    public Rigidbody rb;
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

}
