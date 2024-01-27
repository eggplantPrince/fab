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

    void Start() {
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer() {
        float duration = Random.Range(minInterval, maxInterval);
        yield return new WaitForSeconds(duration);
        float x = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        Instantiate(sphere, new Vector3(x, 19f, z), Quaternion.identity);
        StartCoroutine(SpawnTimer());
    }

    void OnDisable() {
        StopAllCoroutines();
    }
}
