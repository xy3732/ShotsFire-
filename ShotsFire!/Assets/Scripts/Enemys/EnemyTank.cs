using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]private GameObject objects;
    [SerializeField]private int maxHp;
    private int nowHp;
    [SerializeField] private int exp;
    [Space(10)]
    [SerializeField]private float normalSpeed;
    [SerializeField]private float rotateSpeed;
    [Space(10)]
    [SerializeField] private GameObject Turret;
        
    [Header("Enemy Shadow Settings")]
    [SerializeField] private GameObject Shadow;
    [SerializeField] private Vector3 shadowAncor;

    Animator animator;
    Rigidbody2D rigid;

    GameObject player;
    Transform target;

    private void OnEnable()
    {
        nowHp = maxHp;

        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    private void FixedUpdate()
    {
       Move(); 
    }

    private void tankShadow()
    {
        Shadow.transform.position = gameObject.transform.position + shadowAncor;
        Shadow.transform.rotation = transform.rotation;
    }

    private void Move()
    {
        Vector2 dir = (Vector2)target.position - rigid.position;
        dir.Normalize();
        float speed = normalSpeed;

        float rotateAmount = Vector3.Cross(dir, transform.up).z;
        rigid.angularVelocity = -rotateAmount * rotateSpeed;

        if (Vector2.Distance(target.position, rigid.position) <= Turret.GetComponent<sight2D>().m_viewRadius) speed =0;
        rigid.velocity = transform.up * speed;

        Turret.transform.position = gameObject.transform.position;
        tankShadow();
    }

    private void destroys()
    {
        GameManager.instance.pool.EffectGet(0, this.transform.position);
        this.objects.SetActive(false);
        CameraManager.Instance.ShakeCamera(2f, 0.85f);
        PlayerAction.instance.addExp(exp);
        PlayerUiController.instance.ExoBarUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        WeaponsSettings hit;

        if(other.gameObject.CompareTag("playerBullets"))
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

            nowHp -= hit.damage + PlayerAction.instance.level;

            otherObject.destroys();

            if (nowHp <= 0) destroys();
        }
    }
}
