using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject sphere;
    [SerializeField]
    private float minInterval;
    [SerializeField]
    private float maxInterval;

    public float spawnForce = 5f;

    public float minRandomForce = -1f;

    public float maxRandomForce = 3f;

    void Start() {
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer() {
        float duration = Random.Range(minInterval, maxInterval);
        yield return new WaitForSeconds(duration);
        GameObject spawnedSphere = Instantiate(sphere, transform.position, Quaternion.identity);
        float randomForce = Random.Range(minRandomForce,maxRandomForce);
        spawnedSphere.GetComponent<Rigidbody>().AddForce(transform.right * (randomForce + spawnForce), ForceMode.Impulse);
        StartCoroutine(SpawnTimer());
    }

    void OnDisable() {
        StopAllCoroutines();
    }
}
