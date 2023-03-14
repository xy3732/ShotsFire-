using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlight : MonoBehaviour
{
    [Header("Settings")]
    public GameObject objects;
    public float normalSpeed;
    public float rotateSpeed;
    [Space(10)]
    [SerializeField]
    private int maxHp;
    private int nowHp;

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

    private void FixedUpdate()
    {
        Chase();
    }

    void Init()
    {
        nowHp = maxHp;

        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        this.transform.position = objects.transform.position;
        this.transform.rotation = objects.transform.rotation;

        player = GameObject.Find("Player");
        target = player.transform;
    }

    private void flightShadow()
    {
        shadow.transform.position = this.gameObject.transform.position + shadowAnchor;
        shadow.transform.rotation = this.transform.rotation;
    }

    private void Chase()
    {
        // 현재 타겟과의 direction 찾기
        Vector2 dir = (Vector2)target.position - rigid.position;
        dir.Normalize();

        float rotateAmount = Vector3.Cross(dir, transform.up).z;
        rigid.angularVelocity = -rotateAmount * rotateSpeed;
        rigid.velocity = transform.up * normalSpeed;

        flightShadow();
    }

    private void destroys()
    {
        this.objects.SetActive(false);
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

            otherObject.thisMissile.SetActive(false);

            if (nowHp <= 0) destroys();
        }
    }

}
