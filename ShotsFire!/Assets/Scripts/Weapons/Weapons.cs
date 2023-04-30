using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponsSettings
{
    public float speed;
    public float lifeTime;

    public int damage;
    public int PenertrateAble = 1;

    public Rigidbody2D rigid;

    public WeaponsSettings()
    {
        speed = 0;
        damage = 0;
        rigid = null;
    }
}
