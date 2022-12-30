using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyShipPrefab;

    [SerializeField] private GameObject[] powerUps;

    private GameManager gameManager; 

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    public void StarSpawnCoroutine()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }
    public IEnumerator EnemySpawnRoutine()
    {
        while (gameManager.gameOver == false)
        {
            float randomX = Random.Range(-9.0f, 9.0f);
            Instantiate(_enemyShipPrefab, new Vector3(randomX, 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(4.0f);
        }
    }

    public IEnumerator PowerUpSpawnRoutine()
    {
        while (gameManager.gameOver == false)
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerUp], new Vector3(Random.Range(-9.0f, 9.0f), 6, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
