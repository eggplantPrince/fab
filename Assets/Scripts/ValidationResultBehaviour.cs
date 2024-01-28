using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidationResultBehaviour : MonoBehaviour
{
    
    GameControllerBehaviour gc;
    public GameObject SpherePrefab;
    public Transform[] LikedSphereHolders;
    public Transform[] DislikedSphereHolders;
    int likedIndex = 0;
    int dislikedIndex = 0;

    List<SphereTypeEntry> revealedTypes;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameControllerBehaviour>();
        gc.onLikedSphere += ShowLikedSphereType;
        gc.onDislikedSphere+= ShowDislikedSphereType;
        gc.onGameStart += GameInit;
    }

    void GameInit()
    {
        likedIndex = 0;
        dislikedIndex = 0;
        revealedTypes = new List<SphereTypeEntry>();
    }
    

    void ShowLikedSphereType(SphereTypeEntry type)
    {
        if (!IsSphereKnown(type))
        {
            GameObject spawnedSphere = SpawnKnownSphere(type,LikedSphereHolders[likedIndex]);
            likedIndex += 1;
            if(likedIndex == DislikedSphereHolders.Length)
            {
                likedIndex = 0;
            }
        }
    }

    void ShowDislikedSphereType(SphereTypeEntry type)
    {
        if (!IsSphereKnown(type))
        {
            GameObject spawnedSphere = SpawnKnownSphere(type,DislikedSphereHolders[dislikedIndex]);
            dislikedIndex += 1;
            if(dislikedIndex == DislikedSphereHolders.Length)
            {
                dislikedIndex = 0;
            }
        }
    }


    private GameObject SpawnKnownSphere(SphereTypeEntry type, Transform location)
    {
        GameObject spawned = Instantiate(SpherePrefab,location,false);
        spawned.transform.localPosition = Vector3.zero;
        SphereComponent sphere = spawned.GetComponent<SphereComponent>();
        sphere.SetSphereType(type);
        sphere.rb.isKinematic = true;
        sphere.coll.enabled = false;
        return spawned;
    }

    private bool IsSphereKnown(SphereTypeEntry type)
    {
        foreach(SphereTypeEntry revealedType in revealedTypes)
        {
            if(revealedType == type)
            {
                return true;
            }
        }
        revealedTypes.Add(type);
        return false;
    }
}
