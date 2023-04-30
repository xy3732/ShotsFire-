using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullets : MonoBehaviour
{
    [HideInInspector]
    public WeaponsSettings settings;
    public ItemDataSO setting;

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
        settings.lifeTime = 2f;
        settings.damage = (int)setting.nowDamage;

        //enemy 에도 설정값 만들어주기

        Debug.Log(settings.damage);
    }  

   public void Get(float damage)
    {
        settings.damage = Mathf.RoundToInt(damage);
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
