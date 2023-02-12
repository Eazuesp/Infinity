using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    private int spawnInterval = 20; //11
    private int lastSpawnZ = 30; //22
    private int spawnAmount = 3; //4

    public List<GameObject> obstacles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnObstacles();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnObstacles()
    {
       // lastSpawnZ += spawnInterval;

        for (int i = 0; i < spawnAmount; i++)
        {
            // chance 77%
            if (Random.Range(0,4) > 0)
            {
                GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];
                Instantiate(obstacle, new Vector3(0, 0.7f, lastSpawnZ), obstacle.transform.rotation);                
            }
            lastSpawnZ += spawnInterval;
        }
    }
}
