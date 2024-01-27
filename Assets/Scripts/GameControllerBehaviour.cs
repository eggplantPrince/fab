using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerBehaviour : MonoBehaviour
{
    public LevelData[] levels;
    public int currentLevel = 0;
    public Slider progress;
    public float progressModifier = .1f;

    public void RateSphere(SphereComponent sphere)
    {
        foreach (SphereTypeEntry likedType in levels[currentLevel].likes)
        {
            
        }
    }
}
