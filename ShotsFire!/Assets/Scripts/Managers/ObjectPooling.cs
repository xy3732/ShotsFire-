using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 쓰레드
using System.Threading;
using System;
using System.Threading.Tasks;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // 프리펩 보관 함수
    public GameObject[] enemyPrefabs;
    public GameObject[] playerBulletPrefabs;

    // 오브젝트 풀
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

        // 현재 풀에서 비활성화된 오브젝트 찾기
        foreach (var item in enemyPools[index])
        {
            // 비활성화된 오브젝트가 있으면 select 변수에 할당
            if(!item.activeSelf)
            {
                select = item;
                // 활성화
                select.SetActive(true);
                break;
            }   
        }

        // 선택된게 없으면 생성
        if(!select)
        {
            select = Instantiate(enemyPrefabs[index], transform);
            // 리스트에 추가
            enemyPools[index].Add(select);
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
