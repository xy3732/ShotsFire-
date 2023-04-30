using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Object/ItemDataSO")]
public class ItemDataSO : ScriptableObject
{
    public enum ItemType { Bullet, Missile, Engine, Repair}

    [Header("Main Info")]
    public ItemType itemType;
    public int itemID;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;

    [Header("Weapon Info")]
    public float maxShotDelay;
    public float maxClipDelay;
    public int maxClip;

    [Header("Level Data")]
    public float baseDamage;
    public int baseCount;   
    public float[] damages; //레벨당 추가 백분율
    public int[] count;

    [Header("Weapons")]
    public int prefabID;
    public GameObject projectile;

    [Header("nowState")]
    public float nowDamage;
}
