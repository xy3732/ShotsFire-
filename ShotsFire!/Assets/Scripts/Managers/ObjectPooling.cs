using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooling : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // 프리펩 보관 함수
    public GameObject[] enemyPrefabs;
    public GameObject[] enemyBulletPrefabs;
    public GameObject[] playerBulletPrefabs;
    public GameObject[] effectPrefabs;

    // 오브젝트 풀
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

        // 현재 풀에서 비활성화된 오브젝트 찾기
        foreach (var item in enemyPools[index])
        {
            // 비활성화된 오브젝트가 있으면 select 변수에 할당
            if (!item.activeSelf)
            {
                select = item;
                // 활성화
                select.SetActive(true);
                break;
            }
        }

        // 선택된게 없으면 생성
        if (!select)
        {
            select = Instantiate(enemyPrefabs[index], transform);
            // 리스트에 추가
            enemyPools[index].Add(select);
        }

        // 선택 반환
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

        // 현재 풀에서 비활성화된 오브젝트 찾기
        foreach (var item in enemyBulletPools[index])
        {
            // 비활성화된 오브젝트가 있으면 select 변수에 할당
            if (!item.activeSelf)
            {
                select = item;
                select.transform.position = transform.position;
                select.transform.rotation = transform.rotation;
                // 활성화
                select.SetActive(true);
                break;
            }
        }

        // 선택된게 없으면 생성
        if (!select)
        {
            select = Instantiate(enemyBulletPrefabs[index], transform.position, transform.rotation);
            // 리스트에 추가
            enemyBulletPools[index].Add(select);
        }

        // 선택 반환
        return select;
    }


    public GameObject PlayerBulletsGet(int index)
    {
        GameObject select = null;

        foreach (var item in playerBulletPools[index])
        {
            // 비활성화된 오브젝트가 있으면 select 변수에 할당
            if(!item.activeSelf)
            {
                select = item;
                select.transform.position = player.transform.position;
                select.transform.rotation = player.transform.rotation;
                // 활성화
                select.SetActive(true);

                break;
            }
        }
        // 선택된게 없으면 생성
        if(!select)
        {
            // 오브젝트 생성
            select = Instantiate(playerBulletPrefabs[index], player.transform.position, player.transform.rotation);

            // 리스트에 추가
            playerBulletPools[index].Add(select);
        }

        return select;
    }
}
