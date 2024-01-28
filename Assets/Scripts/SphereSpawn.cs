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
    [SerializeField]
    private float maxNumber;
    [SerializeField]
    private PickUpSphere pickUpSphere;
    private float counter = 0;

    public float spawnForce = 5f;

    public float minRandomForce = -1f;

    public float maxRandomForce = 3f;

    private Coroutine spawnCoroutine;

    private List<SphereTypeEntry> currentTypes;

    void Start() {
        GameControllerBehaviour gc = FindObjectOfType<GameControllerBehaviour>();
        gc.onGameStart+= OnGameStart;
        gc.onLevelLost+= OnGameStop;
    }

    void OnGameStart()
    {
        spawnCoroutine = StartCoroutine(SpawnTimer());
    }

    void OnGameStop()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnTimer() {
        float duration = Random.Range(minInterval, maxInterval);
        yield return new WaitForSeconds(duration);
        GameObject spawnedSphere = Instantiate(sphere, transform.position, Quaternion.identity);
        float randomForce = Random.Range(minRandomForce,maxRandomForce);

        SphereComponent sphereComponent = spawnedSphere.GetComponent<SphereComponent>();

        sphereComponent.SetSphereType(currentTypes[Random.Range(0, currentTypes.Count)]);
        sphereComponent.rb.AddForce(transform.right * (randomForce + spawnForce), ForceMode.Impulse);
        pickUpSphere.AddSphere(sphereComponent);
        counter++;
        StartCoroutine(SpawnTimer());
        //if (counter < maxNumber) 
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void SetCurrentTypes(List<SphereTypeEntry> types) {
        currentTypes = types;
        foreach (SphereTypeEntry entry in currentTypes) Debug.Log(entry);
        Debug.Log("--------");
    }
}
