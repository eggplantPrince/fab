using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTarget : MonoBehaviour {
    [SerializeField]
    private float minInterval;
    [SerializeField]
    private float maxInterval;
    private List<SphereComponent> spheres = new List<SphereComponent>();

    private GameControllerBehaviour gameController;

    void OnTriggerEnter(Collider other) {
        SphereComponent sphere = other.GetComponent<SphereComponent>();
        if (sphere != null) {
            spheres.Add(sphere);
        }
    }

    void OnTriggerExit(Collider other) {
        SphereComponent sphere = other.GetComponent<SphereComponent>();
        if (sphere != null) {
            spheres.Add(sphere);
        }

        if (spheres.Contains(sphere))
            spheres.Remove(sphere);
    }


    void Start() {
        gameController = FindObjectOfType<GameControllerBehaviour>();
        StartCoroutine(DeliverTimer());
    }

    private IEnumerator DeliverTimer() {
        float duration = Random.Range(minInterval, maxInterval);
        yield return new WaitForSeconds(duration);
        Deliver();
        StartCoroutine(DeliverTimer());
    }

    void Deliver() {
        Debug.Log(spheres);
        foreach (SphereComponent sphere in spheres){
            gameController.RateSphere(sphere);
            Destroy(sphere.gameObject);
        }
        spheres.Clear();
    }

    void OnDisable() {
        StopAllCoroutines();
    }
}
