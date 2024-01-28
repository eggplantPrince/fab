using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public Animator animator;
    private GameControllerBehaviour gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameControllerBehaviour>();
        gc.onDislikedSphere += UpdateAnimation;
        gc.onLikedSphere += UpdateAnimation;
    }

    
    void UpdateAnimation(SphereTypeEntry type)
    {
        animator.SetFloat("Entertainment",gc.progress.value);
    }


    private void OnDestroy()
    {
        gc.onLikedSphere -= UpdateAnimation;
        gc.onDislikedSphere -= UpdateAnimation;
    }
}
