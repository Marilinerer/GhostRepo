using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{

    public GameObject[] objectToSpawn;
    [SerializeField] private float spawnRate;
    [SerializeField] private bool canSpawn = true;

    void Start()
    {
        SpawnEnemies();
        StartCoroutine(Spawn());
    }

    void Update()
    {

    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;
            SpawnEnemies();
        }
    }
    public void StopSpawning()
    {
        canSpawn = false;
    }

    void SpawnEnemies()
    {
        if (objectToSpawn.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, objectToSpawn.Length);
            Instantiate(objectToSpawn[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
