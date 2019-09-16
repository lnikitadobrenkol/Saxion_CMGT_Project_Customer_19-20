using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public GameObject[] roadBlockPrefabs; // What are we going to spawn

    private List<GameObject> activeRoadBlocks;

    private Transform playerTransform; // Where is the player

    private float spawnPoint = -10.0f; // Where exactly prefabs will be spawned 
    private float roadBlocksLength = 10.0f;
    private float safeZone = 15.0f; // Zone where road blocks are not being deleted

    private int limitOfRoadBlocks = 5; // How many blocks on the road in the same time

    private int lastPrefabIndex = 0;


    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        activeRoadBlocks = new List<GameObject>();

        StartRoadBlocks();
    }

    private void Update()
    {
        if (CanRunRoadEngine())
        {
            SpawnRoadBlock();
            DeleteRoadBlock();
        }
    }

    private void SpawnRoadBlock(int prefabIndex = -1)
    {
        GameObject currentTrack;

        if (prefabIndex == -1)
        {
            currentTrack = Instantiate(roadBlockPrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            currentTrack = Instantiate(roadBlockPrefabs[prefabIndex]) as GameObject;
        }

        currentTrack.transform.SetParent(transform);
        currentTrack.transform.position = Vector3.forward * spawnPoint;

        spawnPoint += roadBlocksLength;

        activeRoadBlocks.Add(currentTrack);
    }

    private void DeleteRoadBlock()
    {
        Destroy(activeRoadBlocks[0]);
        activeRoadBlocks.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        int randomIndex = lastPrefabIndex;

        if (roadBlockPrefabs.Length <= 1)
        {
            return 0;
        }

        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, roadBlockPrefabs.Length);
        }

        lastPrefabIndex = randomIndex;

        return randomIndex;
    }

    private void StartRoadBlocks()
    {
        for (int roadBlocksOnTrack = 0; roadBlocksOnTrack < limitOfRoadBlocks; roadBlocksOnTrack++)
        {
            if (roadBlocksOnTrack < 2)
            {
                SpawnRoadBlock(0);
            }
            else
            {
                SpawnRoadBlock();
            }
        }
    }

    private bool CanRunRoadEngine()
    {
        if ((playerTransform.position.z - safeZone) > (spawnPoint - limitOfRoadBlocks * roadBlocksLength))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
