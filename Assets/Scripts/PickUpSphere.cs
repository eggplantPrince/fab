using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSphere : MonoBehaviour {
    List<SphereComponent> spheres = new List<SphereComponent>();
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
        for (int i = spheres.Count - 1; i >= 0; i--) {
            if (spheres[i] == null) {
                spheres.RemoveAt(i);
            }
        }
    }

    private bool EvaluateSpheres() {
        bool anyWithinReach = false;
        foreach (SphereComponent sphere in spheres) {
            float d = (sphere.transform.position - transform.position).sqrMagnitude;
            if (d <= distance) {
                anyWithinReach = true;
                if (d < currentMinDistance) {
                    grabbableSphere = sphere;
                    currentMinDistance = d;
                }
            }
        }
        if (!anyWithinReach) currentMinDistance = Mathf.Infinity;
        return anyWithinReach;
    }

    public SphereComponent Grab() {
        return grabbableSphere;
    }
}
