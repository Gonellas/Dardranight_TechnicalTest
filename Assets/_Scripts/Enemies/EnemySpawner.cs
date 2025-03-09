using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] float _spawnTime = 2f;
    [SerializeField] float _minDistanceToSpawn = 1.5f;
    [SerializeField] int _maxEnemiesInScene = 20;

    private int _spawnedEnemies = 0;
    private Camera _mainCamera;
    private List<Vector2> _spawnedPositions = new List<Vector2>();

    public static int activeEnemies = 0;

    [Header("Power Up Settings")]
    [SerializeField] private GameObject _PUPrefab1;
    [SerializeField] private GameObject _PUPrefab2;

    public static int enemiesKilled = 0;

    private void Start()
    {
        _mainCamera = Camera.main;
        StartCoroutine(SpawnEnemies());
    }

    #region EnemySpawner
    private IEnumerator SpawnEnemies()
    {
        while(true)
        {
            if(activeEnemies < _maxEnemiesInScene)
            {
                EnemyType enemyType = GetEnemyType(_spawnedEnemies);

                Vector2 spawnPos = GetValidSpawnPos();

                if (spawnPos != Vector2.zero)
                {
                    Enemy enemy = EnemyFactory.Instance.GetObjectFromPool(enemyType);

                    if (enemy != null)
                    {
                        enemy.transform.position = spawnPos;
                        enemy.gameObject.SetActive(true);
                        _spawnedPositions.Add(spawnPos);
                        activeEnemies++;
                    }

                    _spawnedEnemies++;
                }
            }

            yield return new WaitForSeconds(_spawnTime);
        }
    }

    private EnemyType GetEnemyType(int spawnCount)
    {
        if (spawnCount < 5)
        {
            return EnemyType.Small;
        }
        else if (spawnCount < 10)
        {
            return (spawnCount % 2 == 0) ? EnemyType.Small : EnemyType.Medium;
        }
        else if(spawnCount == 10)
        {
             return EnemyType.Big;
        }
        else
        {
            int random = Random.Range(0, 3);
            return (EnemyType)random;
        }
    }

    private Vector2 GetValidSpawnPos()
    {
        int maxAttemps = 10;

        for (int i = 0; i < maxAttemps; i++)
        {
            Vector2 potencialPos = GetRandomPosInView();

            bool tooClose = false;

            foreach (Vector2 pos in _spawnedPositions)
            {
                if(Vector2.Distance(pos, potencialPos) < _minDistanceToSpawn)
                {
                    tooClose = true; 
                    break;
                }
            }

            if (!tooClose) 
            {
                return potencialPos;
            }
        }

        return Vector3.zero;
    }

    private Vector2 GetRandomPosInView()
    {
        float camHeight = _mainCamera.orthographicSize * 2f;
        float camWidth = camHeight * _mainCamera.aspect;

        float marginX = camWidth * .1f;
        float marginY = camHeight * .1f;

        Vector3 camPos = _mainCamera.transform.position;

        float spawnX = Random.Range((camPos.x - camWidth / 2) + marginX, (camPos.x + camWidth / 2) - marginX);
        float spawnY = Random.Range(camPos.y, (camPos.y + camHeight / 2) - marginY);

        return new Vector2(spawnX, spawnY);
    }
    #endregion

    #region Power Up Spawner
    public void SpawnPowerUp(Vector2 pos)
    {
        GameObject randomPU = (Random.Range(0,2) == 0) ? _PUPrefab1 : _PUPrefab2;

        Instantiate(randomPU, pos, Quaternion.identity);
    }
    #endregion
}
