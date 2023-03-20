using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlight_1 : MonoBehaviour
{
    [Header("Settings")]
    public GameObject objects;
    public sight2D sight;
    [Space(10)]
    public float normalSpeed;
    public float rotateSpeed;
    [Space(10)]
    [SerializeField]
    private int maxHp;
    private int nowHp;
    [Space(10)]
    public ParticleSystem smoke;
    public GameObject smokeObject;

    [Header("Bullet Settings")]
    public float MaxShotDelay;
    private float curShotDelay;

    [Header("Enemy Shadow Settings")]
    public GameObject shadow;
    public Vector3 shadowAnchor;

    private GameObject player;
    Transform target;
    private Rigidbody2D rigid;
    private Animator animator;

    private void OnEnable()
    {
        Init();
    }

    private void Update()
    {
        Reload();
        if (targetInSight())
        {
            Shot(targetInSight());
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Init()
    {
        nowHp = maxHp;

        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sight = GetComponent<sight2D>();

        this.transform.position = objects.transform.position;
        this.transform.rotation = objects.transform.rotation;

        smoke.Play();

        player = GameObject.Find("Player");
        target = player.transform;
    }

    private void flightShadow()
    {
        smokeObject.transform.position = this.gameObject.transform.position;

        shadow.transform.position = this.gameObject.transform.position + shadowAnchor;
        shadow.transform.rotation = this.transform.rotation;
    }

    private void Move()
    {
        // 현재 타겟과의 direction 찾기
        Vector2 dir = (Vector2)target.position - rigid.position;
        dir.Normalize();

        float rotateAmount = Vector3.Cross(dir, transform.up).z;
        rigid.angularVelocity = -rotateAmount * rotateSpeed;
        rigid.velocity = transform.up * normalSpeed;

        flightShadow();
    }

    private bool targetInSight()
    {
        sight.FindViewTargets();

        foreach (var hit in sight.hitedTargetContainer)
        {
            if (hit != null)
            {
                Invoke("ShotMiss",3f);
                return true;
            }
        }
        CancelInvoke("ShotMiss");
        return false;
    }

    private void ShotMiss()
    {
        Debug.Log("Shots Fires");
    }

    private void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    private void destroys()
    {
        this.objects.SetActive(false);
        CameraManager.Instance.ShakeCamera(2f, 0.75f);
        GameManager.instance.pool.EffectGet(0, this.transform);
        smoke.Stop();
    }

    private void Shot(bool onShot)
    {
        if (curShotDelay < MaxShotDelay) return;
        curShotDelay = 0;

        if (onShot) GameManager.instance.pool.EnemyBulletGet(0, this.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        WeaponsSettings hit;
        if (other.gameObject.CompareTag("playerBullets"))
        {
            hit = other.gameObject.GetComponent<DefaultBullets>().settings; 

            hit.PenertrateAble -= 1;
            nowHp -= hit.damage;

            if (hit.PenertrateAble <= 0) other.gameObject.SetActive(false);
            if (nowHp <= 0) destroys();
        }

        if (other.gameObject.CompareTag("playerMissiles"))
        {
            DefaultMissile otherObject = other.gameObject.GetComponent<DefaultMissile>();
            hit = otherObject.settings;

            nowHp -= hit.damage;

            otherObject.destroys();

            if (nowHp <= 0) destroys();
        }
    }

}
