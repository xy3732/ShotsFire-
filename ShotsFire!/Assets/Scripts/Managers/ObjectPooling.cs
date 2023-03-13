using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������
using System.Threading;
using System;
using System.Threading.Tasks;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // ������ ���� �Լ�
    public GameObject[] enemyPrefabs;
    public GameObject[] playerBulletPrefabs;

    // ������Ʈ Ǯ
    List<GameObject>[] enemyPools;
    List<GameObject>[] playerBulletPools;

    private void Awake()
    {
        enemyPools = new List<GameObject>[enemyPrefabs.Length];
        playerBulletPools = new List<GameObject>[playerBulletPrefabs.Length];

        for (int index = 0; index < enemyPools.Length; index++)
        {
            enemyPools[index] = new List<GameObject>();
        }

        for(int index = 0; index < playerBulletPools.Length; index++)
        {
            playerBulletPools[index] = new List<GameObject>();
        }

        Debug.Log("enemys : " + enemyPools.Length + ", playerBullets : " + playerBulletPools.Length);
    }

    public GameObject EnemyGet(int index)
    {
        GameObject select = null;

        // ���� Ǯ���� ��Ȱ��ȭ�� ������Ʈ ã��
        foreach (var item in enemyPools[index])
        {
            // ��Ȱ��ȭ�� ������Ʈ�� ������ select ������ �Ҵ�
            if(!item.activeSelf)
            {
                select = item;
                // Ȱ��ȭ
                select.SetActive(true);
                break;
            }   
        }

        // ���õȰ� ������ ����
        if(!select)
        {
            select = Instantiate(enemyPrefabs[index], transform);
            // ����Ʈ�� �߰�
            enemyPools[index].Add(select);
        }

        // ���� ��ȯ
        return select;
    }

    public GameObject PlayerBulletsGet(int index)
    {
        GameObject select = null;

        foreach (var item in playerBulletPools[index])
        {
            // ��Ȱ��ȭ�� ������Ʈ�� ������ select ������ �Ҵ�
            if(!item.activeSelf)
            {
                select = item;
                // Ȱ��ȭ
                select.SetActive(true);
                break;
            }
        }
        // ���õȰ� ������ ����
        if(!select)
        {
            // ������Ʈ ����
            select = Instantiate(playerBulletPrefabs[index], player.transform.position, player.transform.rotation);
            // ����Ʈ�� �߰�
            playerBulletPools[index].Add(select);
        }

        return select;
    }
}
