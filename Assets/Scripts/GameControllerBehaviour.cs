using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerBehaviour : MonoBehaviour
{
    public LevelData[] levels;
    public int currentLevel = 0;
    public Slider progress;
    public float progressModifier = .1f;

    public delegate void GameEvent();
    public delegate void SphereEvent(SphereTypeEntry type);
    public GameEvent onGameStart;
    public GameEvent onLevelLost;
    public GameEvent onLevelWon;
    public GameEvent onGameWon;
    public SphereEvent onLikedSphere;
    public SphereEvent onDislikedSphere;

    public GameObject MainMenu;
    public GameObject LostScreen;
    public GameObject WonScreen;

    public GameObject player;
    public SphereTypesMap sphereTypes;

    private Vector3 playerStartPosition;
    private List<SphereTypeEntry> currentTypes = new List<SphereTypeEntry>();

    public List<SphereSpawn> sphereSpawns;

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
                onLikedSphere?.Invoke(sphere.type);
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
                onDislikedSphere?.Invoke(sphere.type);
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
        currentLevel = Random.Range(0, levels.Length);
        currentTypes.Clear();
        currentTypes.AddRange(levels[currentLevel].likes);
        currentTypes.AddRange(levels[currentLevel].hates);
        List<SphereTypeEntry> remaining = new List<SphereTypeEntry>();
        foreach (SphereTypeEntry type in sphereTypes.typeList)
            if (!levels[currentLevel].likes.Contains(type) && !levels[currentLevel].hates.Contains(type)) remaining.Add(type);
        remaining = remaining.OrderBy( x => Random.value ).ToList( );
        currentTypes.AddRange(remaining.Take(3));
        foreach (SphereSpawn sphereSpawn in sphereSpawns) sphereSpawn.SetCurrentTypes(currentTypes);
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
