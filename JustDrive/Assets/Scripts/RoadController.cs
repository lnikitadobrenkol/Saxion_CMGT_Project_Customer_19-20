using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public GameObject[] roadBlockPrefabs;

    private List<GameObject> activeRoadBlocks;

    private Transform playerTransform;

    private float spawnPoint = -10.0f;
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
            SpawnTrack();
            DeleteTrack();
        }
    }

    private void SpawnTrack(int prefabIndex = -1)
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

    private void DeleteTrack()
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
                SpawnTrack(0);
            }
            else
            {
                SpawnTrack();
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
