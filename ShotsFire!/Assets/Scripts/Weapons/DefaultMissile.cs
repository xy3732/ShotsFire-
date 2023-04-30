using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMissile : MonoBehaviour
{
    public WeaponsSettings settings;

    [Header("Settings")]
    public Transform target;
    public float rotateSpeed;

    [Header(" Missile Shadow Settings")]
    public GameObject thisMissile;
    public GameObject missileShadow;
    public Vector3 ShadowAnchor;
    [Space(10)]
    public ParticleSystem smoke;
    public GameObject smokeObject;

    private void OnEnable()
    {
        Init();

        Invoke("destroys", settings.lifeTime);
        target = Player.instance.target;
        //target = NearEnemyFinder("enemy");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Init()
    {
        Onable();

        settings = new WeaponsSettings();
        settings.rigid = GetComponent<Rigidbody2D>();

        settings.speed = 11f;
        settings.damage = 10;
        settings.lifeTime = 5f;

        this.gameObject.transform.position = thisMissile.transform.position;
        this.gameObject.transform.rotation = thisMissile.transform.rotation;

        target = null;
    }

    private void Move()
    {
        if(target != null)
        {
            // 현재 타겟과의 direction 찾기
            Vector2 dir = (Vector2)target.position - settings.rigid.position;
            dir.Normalize();

            // 회전값
            float rotatedAmount = Vector3.Cross(dir, transform.up).z;

            // 타겟이 도중에 없어질수도 있으므로 체크
            if (target != null) rotatedAmount = Vector3.Cross(dir, transform.up).z;
            else rotatedAmount = 0;

            // 회전값 * 회전속도
            settings.rigid.angularVelocity = -rotatedAmount * rotateSpeed;
        }
        else
        {
            // 타겟이 없으므로 회전값을 없에서 일직선으로 날라가게 한다.
            settings.rigid.angularVelocity = 0 * rotateSpeed;
        }

        settings.rigid.velocity = transform.up * settings.speed;
        MissileShadow();
    }

    private void MissileShadow()
    {
        smokeObject.transform.position = gameObject.transform.position;

        missileShadow.transform.position = this.gameObject.transform.position + ShadowAnchor;
        missileShadow.transform.rotation = this.gameObject.transform.rotation;
    }

    public void Onable()
    {
        SpriteRenderer SpriteTemp;
        SpriteTemp = GetComponent<SpriteRenderer>();
        SpriteTemp.enabled = true;

        Collider2D colliderTemp;
        colliderTemp = GetComponent<Collider2D>();
        colliderTemp.enabled = true;

        smoke.Play();

        missileShadow.SetActive(true);
    }

    public void destroys()
    {
        SpriteRenderer SpriteTemp;
        SpriteTemp = GetComponent<SpriteRenderer>();
        SpriteTemp.enabled = false;

        Collider2D colliderTemp;
        colliderTemp = GetComponent<Collider2D>();
        colliderTemp.enabled = false;

        smoke.Stop();

        missileShadow.SetActive(false);

        CameraManager.Instance.ShakeCamera(1f, 0.75f);
        GameManager.instance.pool.EffectGet(1, this.transform.position);

        CancelInvoke("destroys");
        Invoke("destroyObject",1f);
    }

    private void destroyObject()
    {
        thisMissile.SetActive(false);

        CancelInvoke("destroyObject");
    }
}
