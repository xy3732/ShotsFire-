using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameManager.instance.pool.EnemyGet(0);
            Debug.Log("Spawned");
        }
    }
}
