using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Xsl;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    private int spawnInterval = 20; //11
    private int lastSpawnZ = 30; //22
    private int spawnAmount = 3; //4

    public List<GameObject> obstacles = new List<GameObject>();
    public GameObject coins;
    public GameObject magnet;
    public GameObject enemy;

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

            // chance 75%
            if (Random.Range(0,4) > 1)
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
            }          
            else
            {
                int line1, line2;
                do
                {
                    line1 = Random.Range(0, 3);
                    line2 = Random.Range(0, 3);
                } while (line1 == line2);

                int type = Random.Range(0, 2);
                // chance to spawn magnet on no obstacle spot
                if (type == 0)
                {
                    // 0.7 + 0.5 = 1.3
                    Instantiate(magnet, new Vector3(getX(line1), 1.3f, lastSpawnZ), magnet.transform.rotation);
                }
                else if (type == 1)
                {
                    Instantiate(coins, new Vector3(getX(line1), 1.3f, lastSpawnZ), coins.transform.rotation);
                }
             Instantiate(enemy, new Vector3(getX(line2), 1.3f, lastSpawnZ), enemy.transform.rotation);
            }
            lastSpawnZ += spawnInterval;
        }
    }

    float getX(int line)
    {
        float xComp = 0;
        switch (line)
        {
            case 0:
                xComp = -3.5f;
                break;
            case 2:
                xComp = 3.5f;
                break;
        }
        return xComp;
    }
}
