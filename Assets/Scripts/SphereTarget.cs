using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTarget : MonoBehaviour {
    [SerializeField]
    private float minInterval;
    [SerializeField]
    private float maxInterval;
    [SerializeField]
    private AudioSource beepSource;
    [SerializeField]
    private AudioSource wooshSource;
    private float remainingTime;
    private List<SphereComponent> spheres = new List<SphereComponent>();

    private GameControllerBehaviour gameController;

    public GameObject submitVFXObject;
    public GameObject warnVFXObject;
    // First blinks slow, then fast. Total time is slowBlinkTime + fastBlinkTime in seconds.
    public float slowBlinkTime = 3f;
    public float fastBlinkTime = 1f;
    private Coroutine blink;

    void OnTriggerEnter(Collider other) {
        if (other == null) return;
        SphereComponent sphere = other.GetComponent<SphereComponent>();
        if (sphere != null) {
            spheres.Add(sphere);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other == null) return;
        SphereComponent sphere = other.GetComponent<SphereComponent>();
        if (spheres.Contains(sphere))
            spheres.Remove(sphere);
    }

    void Start() {
        gameController = FindObjectOfType<GameControllerBehaviour>();
        StartCoroutine(DeliverTimer());
    }

    private IEnumerator DeliverTimer() {
        float duration = Random.Range(minInterval, maxInterval);
        yield return new WaitForSeconds(duration - slowBlinkTime - fastBlinkTime);

        blink = StartCoroutine(Blink(0.3f));
        yield return new WaitForSeconds(slowBlinkTime);
        StopCoroutine(blink);
        blink = StartCoroutine(Blink(0.15f));
        yield return new WaitForSeconds(fastBlinkTime);

        StartCoroutine(Deliver());
        StartCoroutine(DeliverTimer());
    }

    private IEnumerator Deliver() {
        submitVFXObject.GetComponent<Animator>().SetTrigger("Submit");
        yield return new WaitForSeconds(0.7f);
        foreach (SphereComponent sphere in spheres){
            gameController.RateSphere(sphere);
            Destroy(sphere.gameObject);
        }
        spheres.Clear();

        wooshSource.Play();
        StopCoroutine(blink);
        warnVFXObject.SetActive(false);
    }

    void OnDisable() {
        StopAllCoroutines();
    }

    private IEnumerator Blink(float interval) {
        while (true) {
            warnVFXObject.SetActive(!warnVFXObject.activeSelf);
            if (warnVFXObject.activeSelf) beepSource.Play();
            yield return new WaitForSeconds(interval);
        }
    }
}
