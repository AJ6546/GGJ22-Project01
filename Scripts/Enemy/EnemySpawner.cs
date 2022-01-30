using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float cooldown = 50, nextSpawn;
    [SerializeField] GameObject enemy1, enemy2;
    void Start()
    {
        nextSpawn = cooldown + (int)Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            GameObject enemy = null;
            Vector3 spawnPos = new Vector3(Random.Range(0, 100), 0.5f, Random.Range(0, 100));
            if(Random.Range(0,100)%2==0)
            {
                enemy = enemy1;
            }
            else
            {
                enemy = enemy2;
            }
            Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
            nextSpawn = Time.time + cooldown;
        }
    }
}
