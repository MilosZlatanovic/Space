using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] powerups;


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRutine());

    }

    // Update is called once per frame

    //spawp game oject in every 5 seconds
    //Create a coroutine of type IEnumerator --Yield Events
    //While loop
    IEnumerator SpawnEnemyRoutine()
    {
        //while loop
        //Instatiate enemy prefab
        //yiald wait for 5 seconds
        // Enemy enemy = transform.GetComponent<Enemy>();

        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(3f);
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEn = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEn.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }

    }

    IEnumerator SpawnPowerupRutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawnPowerup = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPoweruUp = Random.Range(0, 3);
            Instantiate(powerups[randomPoweruUp], posToSpawnPowerup, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(6, 10));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

