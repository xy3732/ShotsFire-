using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Player settings")]
    public float normalSpeed;
    public float boostSpeed;
    private float nowSpeed;
    public float rotateSpeed;
    public float zeroback;
    [Space(10)]
    [SerializeField]
    private int maxHp;
    private int nowHp;

    [Header("Bullet Settings")]
    public float MaxShotDelay;
    private float curShotDelay;

    [Header("Missile Settings")]
    public float MaxMissDelay;
    private float curMissDelay;


    [Header("Player Shadow Settings")]
    public GameObject playerAll;
    public GameObject PlayerObject;
    public GameObject playerShadow;
    public Vector3 shadowAnchor;
    public Sprite[] shadowSprites;
    private SpriteRenderer ShadowSprite;
    private int shadowInput;

    private Rigidbody2D rigid;
    private Animator animator;
    

    private void Awake()
    {
        nowSpeed = normalSpeed;
        nowHp = maxHp;

        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        ShadowSprite = playerShadow.GetComponent<SpriteRenderer>();
    }

    public void Move(Vector2 _input, bool _onBoost, bool _onLeft, bool _onRight)
    {
        // rotate
        if ((_onLeft && _onRight)) rigid.angularVelocity = 0 * rotateSpeed;
        else rigid.angularVelocity = -_input.x * rotateSpeed;

        // �Ѵ� ������ ������ ����Ʈ ����
        if ((_onLeft && _onRight))
        {
            animator.SetBool("LEFT", false);
            animator.SetBool("RIGHT", false);
        }
        // �ƴϸ� ���� ������ �ִ�Ű ����
        else
        {
            animator.SetBool("LEFT", _onLeft);
            animator.SetBool("RIGHT", _onRight);
        }

        // speed
        rigid.velocity = transform.up * nowSpeed;
    }

    public void PlayerShadow(bool _onLeft, bool _onRight)
    {
        // �÷��̾� �ؿ� �׸��� ����� ��Ŀ.
        playerShadow.transform.position = PlayerObject.transform.position + shadowAnchor;
        // �����̼� ���� ���� �ϱ�.
        playerShadow.transform.rotation = PlayerObject.transform.rotation;

        int Inputs = 0;
        // �Ѵ� ������ ������ ����Ʈ
        if (_onLeft && _onRight) Inputs = 0;
        //�ƴϸ� ���� ������ �ִ�Ű ����
        else if (_onLeft) Inputs = 1;
        else if (_onRight) Inputs = 2;
        else Inputs = 0;

        // �׸���.
        switch (Inputs)
        {
            // Shadow Sprite - C
            case 0:
                ShadowSprite.sprite = shadowSprites[1];
                break;

            // Shadow Sprite - L
            case 1:
                ShadowSprite.sprite = shadowSprites[0];
                break;

            // Shadow Sprite - R
            case 2:
                ShadowSprite.sprite = shadowSprites[2];
                break;

                // ����Ʈ
            default:
                ShadowSprite.sprite = shadowSprites[1];
                break;
        }
    }


    private float t = 0;
    public void BoostSpeed(bool onBoost)
    {
        // �����ӵ�
        float tempSpeed = onBoost ? boostSpeed : normalSpeed;

        // ���ι� ��ġ �ɶ� ���� ����
        if (t >= zeroback) t = zeroback;
        t += Time.deltaTime;
        // ����
        nowSpeed = Mathf.Lerp(nowSpeed, tempSpeed, Mathf.InverseLerp(0, zeroback, t));

        //Debug.Log("now speed : " + nowSpeed +", " + tempSpeed + ", " + "zeroback now : " + Mathf.InverseLerp(0, zeroback, t));
    }

    public void Reload()
    {
        curShotDelay += Time.deltaTime;
        curMissDelay += Time.deltaTime;
    }

    public void ShiftInputs()
    {
        t = 0;
    }

    public void FlareFire()
    {
        Debug.Log("FLARE");
    }

    public void Missile()
    {
        if (curMissDelay < MaxMissDelay) return;
        curMissDelay = 0;

        GameManager.instance.pool.PlayerBulletsGet(1);
    }

    public void Shot(bool onShot)
    {
        if (curShotDelay < MaxShotDelay) return;
        curShotDelay = 0;

        if(onShot) GameManager.instance.pool.PlayerBulletsGet(0); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        WeaponsSettings hit;
        if(other.gameObject.CompareTag("enemyBullets"))
        {
            hit = other.gameObject.GetComponent<DefaultBullets>().settings;

            hit.PenertrateAble -= 1;
            nowHp -= hit.damage;

            if (hit.PenertrateAble <= 0) other.gameObject.SetActive(false);
            if (nowHp <= 0) playerAll.SetActive(false);
        }
    }
}
