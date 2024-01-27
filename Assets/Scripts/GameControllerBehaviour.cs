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

    public delegate void GameEvent();
    public GameEvent onGameStart;
    public GameEvent onLevelLost;
    public GameEvent onLevelWon;
    public GameEvent onGameWon;
    public GameEvent onLikedSphere;
    public GameEvent onDislikedSphere;

    public GameObject MainMenu;
    public GameObject LostScreen;
    public GameObject WonScreen;

    public GameObject player;

    private Vector3 playerStartPosition;

    public void Start()
    {
        DontDestroyOnLoad(this);
        playerStartPosition = player.transform.position;
        player.GetComponentInChildren<Rigidbody>().isKinematic = true;
        onGameWon += ShowWinScreen;
        onLevelLost += ShowLooseScreen;
    }

    public void RateSphere(SphereComponent sphere)
    {
        foreach (SphereTypeEntry likedType in levels[currentLevel].likes)
        {
            if(likedType == sphere.type)
            {
                progress.value += progressModifier;
                onLikedSphere?.Invoke();
                if(progress.value >= 1f)
                    if(currentLevel == levels.Length - 1)
                    {
                        onGameWon?.Invoke();
                    } else
                    {
                        onLevelWon?.Invoke();
                    }
                break;
            }
        }

        foreach (SphereTypeEntry hateType in levels[currentLevel].hates)
        {
            if(hateType == sphere.type)
            {
                progress.value -= progressModifier;
                onDislikedSphere?.Invoke();
                if(progress.value <= 0f)
                    onLevelLost?.Invoke();
                break;
            }
        }
    }


    public void StartGame()
    {
        player.transform.position = playerStartPosition;
        player.GetComponentInChildren<Rigidbody>().isKinematic = false;
        onGameStart?.Invoke();
        currentLevel = 0;
        progress.value = levels[0].startingEntertainment;
        MainMenu.SetActive(false);
    }


    private void ShowWinScreen()
    {
        WonScreen.SetActive(true);
    }

    private void ShowLooseScreen()
    {
        LostScreen.SetActive(true);
    }
}
