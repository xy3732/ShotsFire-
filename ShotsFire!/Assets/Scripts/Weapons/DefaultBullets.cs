using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullets : MonoBehaviour
{
    [HideInInspector]
    public WeaponsSettings settings;
    private void OnEnable()
    {
        Init();

        Move();
        Invoke("destroys", settings.lifeTime);
    }

    private void Init()
    {
        settings = new WeaponsSettings();
        settings.rigid = GetComponent<Rigidbody2D>();

        settings.speed = 15f;
        settings.damage = 1;
        settings.lifeTime = 2f;
    }  

    private void Move()
    {
        settings.rigid.velocity = transform.up * settings.speed;
    }

    private void destroys()
    {
        gameObject.SetActive(false);
        CancelInvoke("destroys");
    }
}
