using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public int nrOfEnemies;
    public float[] health;
    public float[] position;

    public EnemyData (GameObject[] enemy)
    {
        nrOfEnemies = enemy.Length;
        health = new float[nrOfEnemies];
        position = new float[3 * nrOfEnemies];
        for(int i = 0; i < enemy.Length; i++)
        {
            health[i] = enemy[i].GetComponent<EnemyController>().currentHealth;
            position[3 * i] = enemy[i].transform.position.x;
            position[3 * i + 1] = enemy[i].transform.position.y;
            position[3 * i + 2] = enemy[i].transform.position.z;
        }
    }
}
