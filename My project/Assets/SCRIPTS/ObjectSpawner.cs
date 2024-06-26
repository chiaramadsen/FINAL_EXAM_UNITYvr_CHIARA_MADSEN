using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject objectPrefab; // The object prefab to spawn
    public float spawnInterval = 5f; // Time in seconds between spawns
    public Transform[] spawnPoints; // Optional: Specific points to spawn objects

    private Coroutine spawnCoroutine; // Reference to the current spawning coroutine

    // Method to start spawning objects indefinitely at intervals
    [ContextMenu("Start Spawning Indefinitely")]
    public void StartSpawningIndefinitely()
    {
        if (spawnCoroutine != null) // Ensure no other spawn is active
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnObjectsIndefinitely());
    }
    
    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    // Method to spawn a single object
    [ContextMenu("Spawn Single Object")]
    public void SpawnSingleObject()
    {
        SpawnObject();
    }

    // Method to spawn a specific number of objects with an interval
    public void SpawnMultipleObjects(int count)
    {
        if (spawnCoroutine != null) // Ensure no other spawn is active
        {
            StopCoroutine(spawnCoroutine);
        }
        spawnCoroutine = StartCoroutine(SpawnMultiple(count));
    }

    private IEnumerator SpawnObjectsIndefinitely()
    {
        while (true) // Loop indefinitely
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnObject();
        }
    }

    private IEnumerator SpawnMultiple(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Helper method to handle the actual instantiation of the object
    private void SpawnObject()
    {
        if (spawnPoints.Length > 0)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; //change this if you don't want to randomize spawn points
            
            Instantiate(objectPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Instantiate(objectPrefab, transform.position, transform.rotation);
        }
    }
}

