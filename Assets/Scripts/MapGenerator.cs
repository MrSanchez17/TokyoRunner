using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 20f;

    [SerializeField] private Transform StartPlatform;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Player player;
    [SerializeField] GameObject PlatformsObjects;

 
    Vector3 lastEndPosition;
    Transform LastLevelPartTransform;

    private void Awake()
    {
        lastEndPosition = StartPlatform.Find("EndPosition").position;
        //LastLevelPartTransform = StartPlatform;

    }

    private void Update()
    {
         if(Vector3.Distance(player.transform.position, lastEndPosition)<PLAYER_DISTANCE_SPAWN_LEVEL_PART)
         {
            SpawnPart1();
            //Destroy(PlatformsObjects.gameObject, 20f);
        }
    }

    void SpawnPart1()
    {
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastPartPlatform;
        lastPartPlatform = SpawnPart1(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastPartPlatform.Find("EndPosition").position;
    }

    Transform SpawnPart1(Transform levelPart, Vector3 spawnPositions)
    {
        Transform levelPlatforms = Instantiate(levelPart, spawnPositions, Quaternion.identity);
        levelPlatforms.transform.parent = PlatformsObjects.transform;
        return levelPlatforms;
    }



    public void DestroyPlatforms()
    {
        Destroy(PlatformsObjects.gameObject);
    }


}
