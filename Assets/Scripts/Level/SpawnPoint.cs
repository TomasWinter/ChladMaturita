using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] float randomDelayLower = 0f;
    [SerializeField] float spawnDelayUpper = 10f;
    float spawnDelay = 0f;

    [SerializeField] SpawnPoolSO spawnPool;

    [SerializeField] List<Transform> spawnGroup;

    float timer = 0f;

    private void Start()
    {
        Randomise();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (StateManager.Instance?.State == WaveState.Loud && timer > spawnDelay)
        {
            timer = 0f;
            Spawn();
        }
    }

    private void Randomise()
    {
        spawnDelay = Random.Range(randomDelayLower, spawnDelayUpper);
    }

    private void Spawn()
    {
        if (spawnGroup.Count > 0)
        {
            GameObject gameObject = spawnPool.fields.GetRandom().Prefab;
            foreach (Transform t in spawnGroup)
            {
                Instantiate(gameObject, t.position, t.rotation);
            }
            
        }
        else
        {
            GameObject gameObject = spawnPool.fields.GetRandom().Prefab;
            Instantiate(gameObject, transform.position, transform.rotation);
        }
        
    }
}
