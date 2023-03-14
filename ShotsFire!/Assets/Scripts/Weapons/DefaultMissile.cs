using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultMissile : MonoBehaviour
{
    [HideInInspector]
    public WeaponsSettings settings;

    [Header("Settings")]
    private Transform target;
    public float rotateSpeed;

    [Header(" Missile Shadow Settings")]
    public GameObject thisMissile;
    public GameObject missileShadow;
    public Vector3 ShadowAnchor;

    private void OnEnable()
    {
        Init();

        Invoke("destroys", settings.lifeTime);
        target = NearEnemyFinder("enemy");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Init()
    {
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
        missileShadow.transform.position = this.gameObject.transform.position + ShadowAnchor;
        missileShadow.transform.rotation = this.gameObject.transform.rotation;
    }

    // Ÿ�� ���δ�
    private Transform NearEnemyFinder(string targetName)
    {
        // Ÿ�� ������Ʈ���� �ױ�.
        var targets = GameObject.FindGameObjectsWithTag(targetName);
        // ���� �� �Ѱ��� ������ �� Ÿ���� ����
        if (targets.Length == 1) return targets[0].transform;

        // �׷��� ������ ���� ����� Ÿ������ ����
        GameObject result = null;
        var minTargetDistance = float.MaxValue;
        foreach (var target in targets)
        {
            float targetDistance = Vector3.Distance(transform.position, target.transform.position);

            if (!(targetDistance < minTargetDistance)) continue;

            minTargetDistance = targetDistance;
            result = target.transform.gameObject;
        }

        // �� ��ȯ
        return result?.transform;
    }

    private void destroys()
    {
        thisMissile.SetActive(false);
    }
}
