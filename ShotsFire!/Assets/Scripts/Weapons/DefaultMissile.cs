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
            // ���� Ÿ�ٰ��� direction ã��
            Vector2 dir = (Vector2)target.position - settings.rigid.position;
            dir.Normalize();

            // ȸ����
            float rotatedAmount = Vector3.Cross(dir, transform.up).z;

            // Ÿ���� ���߿� ���������� �����Ƿ� üũ
            if (target != null) rotatedAmount = Vector3.Cross(dir, transform.up).z;
            else rotatedAmount = 0;

            // ȸ���� * ȸ���ӵ�
            settings.rigid.angularVelocity = -rotatedAmount * rotateSpeed;
        }
        else
        {
            // Ÿ���� �����Ƿ� ȸ������ ������ ���������� ���󰡰� �Ѵ�.
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
