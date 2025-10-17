using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemise;
    private float[] arrPosx = { -2.2f, -1.1f, 0f, 1.1f, 2.2f };
    [SerializeField]
    private float spawnInterval = 1.5f;
    void Start()
    {
        StartEnemyRoutine();
    }
    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
    }
    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        float moveSpeed = 5f;
        int spawnCount = 0;
        int enemyIndex = 0;
        while (true)
        {
            foreach (float posX in arrPosx)
            {
                SpawnEnemy(posX, enemyIndex, moveSpeed);
            }
            spawnCount += 1;
            if(spawnCount % 10 == 0)
            {
                enemyIndex += 1;
                moveSpeed += 2;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void SpawnEnemy(float posX, int index,float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);
        if (Random.Range(0, 5) == 0)
        {
            index += 1;
        }
        if (index >= enemise.Length)
        {
            index = enemise.Length - 1;
        }
        GameObject enemyObject = Instantiate(enemise[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }
}
