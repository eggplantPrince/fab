using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereDestroy : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
    }
}
