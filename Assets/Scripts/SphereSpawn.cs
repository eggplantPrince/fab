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

    public SphereTypesMap sphereTypes;

    void Start() {
        StartCoroutine(SpawnTimer());

    }

    private IEnumerator SpawnTimer() {
        float duration = Random.Range(minInterval, maxInterval);
        yield return new WaitForSeconds(duration);
        GameObject spawnedSphere = Instantiate(sphere, transform.position, Quaternion.identity);
        float randomForce = Random.Range(minRandomForce,maxRandomForce);

        SphereComponent sphereComponent = spawnedSphere.GetComponent<SphereComponent>();

        sphereComponent.SetSphereType(sphereTypes.typeList[Random.Range(0,sphereTypes.typeList.Length)]);
        sphereComponent.rb.AddForce(transform.right * (randomForce + spawnForce), ForceMode.Impulse);
        StartCoroutine(SpawnTimer());
    }

    void OnDisable() {
        StopAllCoroutines();
    }
}
