using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooling : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // ������ ���� �Լ�
    public GameObject[] enemyPrefabs;
    public GameObject[] enemyBulletPrefabs;
    public GameObject[] playerBulletPrefabs;
    public GameObject[] effectPrefabs;

    // ������Ʈ Ǯ
    List<GameObject>[] enemyPools;
    List<GameObject>[] enemyBulletPools;
    List<GameObject>[] playerBulletPools;
    List<GameObject>[] effectPools;

    private void Awake()
    {
        enemyPools = new List<GameObject>[enemyPrefabs.Length];
        enemyBulletPools = new List<GameObject>[enemyBulletPrefabs.Length];
        playerBulletPools = new List<GameObject>[playerBulletPrefabs.Length];
        effectPools = new List<GameObject>[effectPrefabs.Length];

        for (int index = 0; index < enemyPools.Length; index++)
        {
            enemyPools[index] = new List<GameObject>();
        }

        for (int index = 0; index < enemyBulletPools.Length; index++)
        {
            enemyBulletPools[index] = new List<GameObject>();
        }

        for (int index = 0; index < playerBulletPools.Length; index++)
        {
            playerBulletPools[index] = new List<GameObject>();
        }

        for (int index = 0; index < effectPools.Length; index++)
        {
            effectPools[index] = new List<GameObject>();
        }
    }

    public GameObject EnemyGet(int index)
    {
        GameObject select = null;

        // ���� Ǯ���� ��Ȱ��ȭ�� ������Ʈ ã��
        foreach (var item in enemyPools[index])
        {
            // ��Ȱ��ȭ�� ������Ʈ�� ������ select ������ �Ҵ�
            if (!item.activeSelf)
            {
                select = item;
                // Ȱ��ȭ
                select.SetActive(true);
                break;
            }
        }

        // ���õȰ� ������ ����
        if (!select)
        {
            select = Instantiate(enemyPrefabs[index], transform);
            // ����Ʈ�� �߰�
            enemyPools[index].Add(select);
        }

        // ���� ��ȯ
        return select;
    }

    public GameObject EffectGet(int index, Transform transform)
    {
        GameObject select = null;

        foreach (var item in effectPools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.transform.position = transform.position;

                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            {
                select = Instantiate(effectPrefabs[index], transform.position, transform.rotation);
                effectPools[index].Add(select);
            }
        }

        return select;
    } 

    public GameObject EnemyBulletGet(int index, Transform transform)
    {
        GameObject select = null;

        // ���� Ǯ���� ��Ȱ��ȭ�� ������Ʈ ã��
        foreach (var item in enemyBulletPools[index])
        {
            // ��Ȱ��ȭ�� ������Ʈ�� ������ select ������ �Ҵ�
            if (!item.activeSelf)
            {
                select = item;
                select.transform.position = transform.position;
                select.transform.rotation = transform.rotation;
                // Ȱ��ȭ
                select.SetActive(true);
                break;
            }
        }

        // ���õȰ� ������ ����
        if (!select)
        {
            select = Instantiate(enemyBulletPrefabs[index], transform.position, transform.rotation);
            // ����Ʈ�� �߰�
            enemyBulletPools[index].Add(select);
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
                select.transform.position = player.transform.position;
                select.transform.rotation = player.transform.rotation;
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
