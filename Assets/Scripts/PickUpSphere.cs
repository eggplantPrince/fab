using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSphere : MonoBehaviour {
    List<SphereComponent> spheres = new List<SphereComponent>();
    List<SphereComponent> colliderSpheres = new List<SphereComponent>();
    [SerializeField]
    private float distance;
    private float currentMinDistance = Mathf.Infinity;
    private SphereComponent grabbableSphere;

    public void AddSphere(SphereComponent sphere) {
        spheres.Add(sphere);
    }

    public void RemoveSphere(SphereComponent sphere) {
        spheres.Remove(sphere);
    }

    public bool HasGrabbable() {
        CleanList();
        return EvaluateSpheres();
    }

    private void CleanList() {
        // the spheres list is old and unused lol but still here idk why
        // just using extra cpu cycles for fun
        for (int i = spheres.Count - 1; i >= 0; i--) {
            if (spheres[i] == null) {
                spheres.RemoveAt(i);
            }
        }
        for (int i = colliderSpheres.Count - 1; i >= 0; i--) {
            if (colliderSpheres[i] == null) {
                colliderSpheres.RemoveAt(i);
            }
        }
    }

    private bool EvaluateSpheres() {
        if (colliderSpheres.Count == 0) {
            grabbableSphere = null;
            return false;
        }

        currentMinDistance = Mathf.Infinity;
        foreach (SphereComponent sphere in colliderSpheres) {
            float d = (sphere.transform.position - transform.position).sqrMagnitude;
            if (d < currentMinDistance) {
                grabbableSphere = sphere;
                currentMinDistance = d;
            }
        }

        return true;
    }

    public SphereComponent Grab() {
        colliderSpheres.Remove(grabbableSphere);
        return grabbableSphere;
    }

    void OnTriggerEnter(Collider other) {
        if (other.GetComponent<SphereComponent>() == null) return;
        colliderSpheres.Add(other.GetComponent<SphereComponent>());
    }

    void OnTriggerExit(Collider other) {
        if (other.GetComponent<SphereComponent>() == null) return;
        colliderSpheres.Remove(other.GetComponent<SphereComponent>());
    }

    void Update() {
        Debug.Log($"colliderSpheres: {colliderSpheres.Count}");
    }
}
