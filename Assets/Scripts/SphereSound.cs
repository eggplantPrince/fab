using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSound : MonoBehaviour {
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    MeshRenderer mrenderer;
    float initPitch;

    void Start() {
        initPitch = audioSource.pitch;
    }

    void OnCollisionEnter(Collision collision){
        if (collision.relativeVelocity.magnitude > 3 && mrenderer.isVisible) {
            audioSource.pitch = Random.Range(initPitch - 0.1f, initPitch + 0.1f);
            audioSource.Play();
        }
    }
}
