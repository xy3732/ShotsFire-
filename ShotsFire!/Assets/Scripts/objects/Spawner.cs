using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    private Transform[] spawnPoint;
    public SpawnSlots[] SpawnSlots;
    public GameObject PlayerObject;

    public int spawnLevel;
    float[] timer = new float[10];

    private void Awake()
    {
        instance = this;

        for(int i =0; i<timer.Length; i++)
        {
            timer[i] = 0;
        }
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        for(int i =0; i<5; i++)Spawn(0);
    }

    private void Update()
    {
        spawnLevel = Mathf.FloorToInt(GameManager.instance.gameTime / GameManager.instance.RoundLevelDuration);

        for(int i =0; i < SpawnSlots[spawnLevel].spawnDatas.Length; i++)
        {
            timer[i] += Time.deltaTime;

            if (timer[i] > SpawnSlots[spawnLevel].spawnDatas[i].spawnTime && PlayerObject.activeSelf)
            {
                Spawn(i);
                timer[i] = 0;
            }
        }
    }

    void Spawn(int i)
    {
        GameObject enemy = GameManager.instance.pool.EnemyGet(SpawnSlots[spawnLevel].spawnDatas[i].spawn);

        Debug.Log(SpawnSlots[spawnLevel].spawnDatas[i].spawn);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}

[System.Serializable]
public class SpawnSlots
{
    [SerializeField]
    [HideInInspector] public string name;
    [SerializeField]
    public SpawnDataSO[] spawnDatas;
}


