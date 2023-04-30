using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    [Header("Settings")]
    public float rotateSpeed;

    [Header("Bullet Settings")]
    [SerializeField]private float maxShotDelay;
    private float curShotDelay;

    GameObject player;
    Rigidbody2D rigid;
    [HideInInspector]public sight2D sight;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sight = GetComponent<sight2D>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Reload();

        if(TargetInSight())
        {
            Shot(TargetInSight());
        }
    }

    private void FixedUpdate()
    {
        Roate();
    }

    private void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    private void Shot(bool onShot)
    {
        if (curShotDelay < maxShotDelay) return;
        curShotDelay = 0;

        if (onShot) GameManager.instance.pool.EnemyBulletGet(0, transform);
    }

    private void Roate()
    {
        Vector2 dir = (Vector2)player.transform.position - rigid.position;
        dir.Normalize();

        float rotateAmount = Vector3.Cross(dir, transform.up).z;
        rigid.angularVelocity = -rotateAmount * rotateSpeed;
    }

    private bool TargetInSight()
    {
        sight.FindViewTargets();

        foreach (var hit in sight.hitedTargetContainer)
        {
            if (hit != null) return true;
        }
        return false;
    }
}
