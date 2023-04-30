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
    [Space(10)]
    public int level;
    [HideInInspector] public float maxExp;
    [HideInInspector] public float curExp;


    [Header("Missile Settings")]
    //public ItemDataSO missileSlot;
    //private float curMissDelay;
    //[HideInInspector] public bool isTargeting = false;


    [Header("Player Shadow Settings")]
    public GameObject PlayerObjects;
    public GameObject PlayerObject;
    public GameObject playerShadow;
    public GameObject Area;
    public Vector3 shadowAnchor;
    public Sprite[] shadowSprites;
    private SpriteRenderer ShadowSprite;

    public static PlayerAction instance;
    [HideInInspector] public Rigidbody2D rigid;
    private Animator animator;
    private sight2D sight;
    [HideInInspector] public Transform target;

    private void Awake()
    {
        instance = this;

        nowSpeed = normalSpeed;
        nowHp = maxHp;

        level = 1;
        maxExp = LevelUpExpChange();
        curExp = 0;

        sight = GetComponent<sight2D>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        ShadowSprite = playerShadow.GetComponent<SpriteRenderer>();


    }

    public void Move(Vector2 _input, bool _onBoost, bool _onLeft, bool _onRight)
    {

        // rotate
        if ((_onLeft && _onRight)) rigid.angularVelocity = 0 * rotateSpeed;
        else rigid.angularVelocity = -_input.x * rotateSpeed;

        // 둘다 누르고 있으면 디폴트 설정
        if ((_onLeft && _onRight))
        {
            animator.SetBool("LEFT", false);
            animator.SetBool("RIGHT", false);
        }
        // 아니면 현재 누르고 있는키 배정
        else
        {
            animator.SetBool("LEFT", _onLeft);
            animator.SetBool("RIGHT", _onRight);
        }

        // Area
        Area.transform.position = PlayerObject.transform.position;
       
        // speed
        rigid.velocity = transform.up * nowSpeed;
    }

    public void PlayerShadow(bool _onLeft, bool _onRight)
    {
        // 플레이어 밑에 그림자 생기게 앵커.
        playerShadow.transform.position = PlayerObject.transform.position + shadowAnchor;
        // 로테이션 값도 같게 하기.
        playerShadow.transform.rotation = PlayerObject.transform.rotation;

        int Inputs = 0;
        // 둘다 누르고 있으면 디폴트
        if (_onLeft && _onRight) Inputs = 0;
        //아니면 현재 누르고 있는키 배정
        else if (_onLeft) Inputs = 1;
        else if (_onRight) Inputs = 2;
        else Inputs = 0;

        // 그림자.
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

                // 디폴트
            default:
                ShadowSprite.sprite = shadowSprites[1];
                break;
        }
    }


    private float t = 0;
    public void BoostSpeed(bool onBoost)
    {
        // 다음속도
        float tempSpeed = onBoost ? boostSpeed : normalSpeed;

        // 제로백 수치 될때 까지 증가
        if (t >= zeroback) t = zeroback;
        t += Time.deltaTime;
        // 가속
        nowSpeed = Mathf.Lerp(nowSpeed, tempSpeed, Mathf.InverseLerp(0, zeroback, t));

        //Debug.Log("now speed : " + nowSpeed +", " + tempSpeed + ", " + "zeroback now : " + Mathf.InverseLerp(0, zeroback, t));
    }

    private float LevelUpExpChange()
    {
        // =(K3^2*K3) + 150

        float temp = Mathf.Pow(level, 2f) * level + 250f;// Mathf.Round((50 * level ^ 4) + (75 * level ^ 2) / 47.5f + 250f);
        Debug.Log(Mathf.Pow(level, 2f) * level + 250f );
        return temp;
    }
    public void addExp(float exp)
    {
        curExp += exp;

        if (curExp >= maxExp) LevelUp();
    }

    private void LevelUp()
    {
        float cutExp = curExp - maxExp;

        level += 1;

        curExp = 0;
        curExp += cutExp;

        nowHp += 5;
        if (maxHp < nowHp) nowHp = maxHp;

        maxExp =  LevelUpExpChange();
        PlayerUiController.instance.BarUpdate(nowHp, maxHp, PlayerUiController.instance._HpBar);
    }

    /*
    public void Reload()
    {
        //curShotDelay += Time.deltaTime;
        curMissDelay += Time.deltaTime;
        if(curMissDelay <= missileSlot.maxClipDelay) PlayerUiController.instance.BarUpdate(curMissDelay, missileSlot.maxClipDelay, PlayerUiController.instance._MissileCurrBar);

        //if (curClip <= 0)  ReloadBullets(false);
    }
    */

    public void ShiftInputs()
    {
        t = 0;
    }

    public void FlareFire()
    {
        Debug.Log("FLARE");
    }

    
    public void UpdateSight()
    {
        sight.FindViewTargets();
        // 가까운 타겟을 검색함.
        target = NearEnemyFinder("enemy");
    }

    // 타겟 오브젝트 파인더
    private Transform NearEnemyFinder(string targetName)
    {
        // 현재 필드에 있는 타겟 오브젝트들을 배열화 시킨다.
        var targets = GameObject.FindGameObjectsWithTag(targetName);

        // 그렇지 않으면 제일 가까운 타겟으로 잡는다.
        GameObject result = null;
        var minTargetDistance = float.MaxValue;

        foreach (var target in targets)
        {
            float targetDistance = Vector3.Distance(transform.position, target.transform.position);
            // 현재 타겟팅 가능한 사이트 안에 없으면 타겟으로 잡지 않기.
            if (!sight.hitedTargetContainer.Contains(target.GetComponent<Collider2D>())) continue;
            // 최근 검색한 타겟이 더 가깝고, 오브젝트가 활성화 상태이면 타겟으로 잡기.
            if (!(targetDistance < minTargetDistance) && target.activeSelf) continue;
            minTargetDistance = targetDistance;
            result = target.transform.gameObject;
        }

        return result?.transform;
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
            if (nowHp <= 0)
            {
                CameraManager.Instance.ShakeCamera(2f, 0.75f);
                GameManager.instance.pool.EffectGet(0, this.transform.position);
                PlayerObjects.SetActive(false);
            }

            CameraManager.Instance.ShakeCamera(0.75f, 0.5f);
            PlayerUiController.instance.BarUpdate(nowHp, maxHp, PlayerUiController.instance._HpBar);
        }
    }
}
