using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    private int spawnInterval = 20; //11
    private int lastSpawnZ = 30; //22
    private int spawnAmount = 3; //4

    public List<GameObject> obstacles = new List<GameObject>();
    public GameObject coins;
    public GameObject magnet;

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
            bool spawned = false;
            // chance 75%
            if (Random.Range(0,4) > 0)
            {
                GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];
                if (Random.Range(0, 2) == 1)
                {
                    Instantiate(obstacle, new Vector3(0, 0.7f, lastSpawnZ), obstacle.transform.rotation);
                }
                else
                {
                    Instantiate(obstacle, new Vector3(0, 0.7f, lastSpawnZ), new Quaternion(0, 180, 0, 0));
                }
                spawned = true;
            }          

            if (!spawned)
            {
                // 0.7 + 0.5 = 1.3
                int rand = Random.Range(0, 3);
                float xComp = 0;
                switch (rand)
                {
                    case 0:
                        xComp = -3.5f;
                        break;
                    case 2:
                        xComp = 3.5f;
                        break;

                }
                if (Random.Range(0, 3) == 1)
                {
                    Instantiate(magnet, new Vector3(xComp, 1.3f, lastSpawnZ), magnet.transform.rotation);
                }
                else
                {
                    Instantiate(coins, new Vector3(xComp, 1.3f, lastSpawnZ), coins.transform.rotation);
                }
            }
            lastSpawnZ += spawnInterval;
        }
    }
}
