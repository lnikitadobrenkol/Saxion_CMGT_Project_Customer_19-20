using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public GameObject[] trackPrefabs;

    private Transform playerTransform;
    private float spawnZ = -10.0f;
    private float trackLength = 10.0f;
    private float safeZone = 15.0f;
    private int amnOfTrackOnScreen = 7;
    private int lastPrefabIndex = 0;

    private List<GameObject> activeTracks;

    private void Start()
    {
        activeTracks = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < amnOfTrackOnScreen; i++)
        {
            if (i < 2)
            {
                SpawnTrack(0);
            }
            else
            {
                SpawnTrack();
            }
        }
    }

    private void Update()
    {
        if ((playerTransform.position.z - safeZone) > (spawnZ - amnOfTrackOnScreen * trackLength))
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
            currentTrack = Instantiate(trackPrefabs[RandomPrefabIndex()]) as GameObject;
        }
        else
        {
            currentTrack = Instantiate(trackPrefabs[prefabIndex]) as GameObject;
        }

        currentTrack.transform.SetParent(transform);
        currentTrack.transform.position = Vector3.forward * spawnZ;
        spawnZ += trackLength;
        activeTracks.Add(currentTrack);
    }

    private void DeleteTrack()
    {
        Destroy(activeTracks[0]);
        activeTracks.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (trackPrefabs.Length <= 1)
        {
            return 0;
        }
        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, trackPrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
