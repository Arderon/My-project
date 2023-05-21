using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject player;
    public List<GameObject> enemies = new List<GameObject> {};
    private Vector2 spawnPosition;

    private float spawnMinDistance = 9;
    private float spawnMaxDistance = 14;
    private float randomX;
    private float randomY;

    public GameObject nearestEnemy;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 0.5f);
        /*spawnPosition = new Vector2(Random.Range(spawnMinDistance, spawnMaxDistance), Random.Range(spawnMinDistance, spawnMaxDistance));*/
    }

    // Update is called once per frame
    void Update()
    {
        GetNearestEnemy();
    }

    void SpawnEnemy()
    {
        if (Random.Range(0, 2) == 0)
        {
            randomX = Random.Range(spawnMinDistance, spawnMaxDistance) + player.transform.position.x;
        }
        else
        {
            randomX = Random.Range(-spawnMinDistance, -spawnMaxDistance) + player.transform.position.x;
        }
        if (Random.Range(0, 2) == 0)
        {
            randomY = Random.Range(spawnMinDistance, spawnMaxDistance) + player.transform.position.y;
        }
        else
        {
            randomY = Random.Range(-spawnMinDistance, -spawnMaxDistance) + player.transform.position.y;
        }
        spawnPosition = new Vector2(randomX, randomY);
        enemies.Add(Instantiate(spawnObject, spawnPosition, transform.rotation));
    }

    public GameObject GetNearestEnemy()
    {
        float smallestDistance = 100000;
        foreach (GameObject enemy in enemies)
        {
            Vector2 heading = enemy.transform.position - player.transform.position;
            float distance = heading.magnitude;
            if (distance < smallestDistance)
            {
                smallestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
