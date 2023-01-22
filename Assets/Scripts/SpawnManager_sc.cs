using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_sc : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject enemylaser;

    [SerializeField]
    private GameObject[] powerupPrefabs;

    [SerializeField]
    private GameObject enemyContainer;

    private bool stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPlayerDeath() {
        stopSpawning = true;
    }

    IEnumerator SpawnEnemyRoutine() {
        yield return new WaitForSeconds(3.0f);
        while (stopSpawning == false) {
            Vector3 position = new Vector3(Random.Range(-12f, 12f), 7.3f, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            Instantiate(enemylaser, position + new Vector3(0, -1.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnBonusRoutine() {
        yield return new WaitForSeconds(3.0f);
        while (stopSpawning == false) {
            Vector3 position = new Vector3(Random.Range(-12f, 12f), 7.3f, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerupPrefabs[randomPowerup], position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,8));
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnBonusRoutine());
    }
}
