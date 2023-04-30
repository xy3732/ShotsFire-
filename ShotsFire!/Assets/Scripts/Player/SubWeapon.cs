using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeapon : MonoBehaviour
{
    public static SubWeapon instance;

    [Header("sub Settings")]
    public ItemDataSO subSlot;
    private float curSubDelay;
    [HideInInspector] public bool isTargeting = false;

    Transform target;

    public void Awake()
    {
        instance = this;
    }

    public void Reload()
    {
        curSubDelay += Time.deltaTime;
        if (curSubDelay <= subSlot.maxClipDelay) PlayerUiController.instance.BarUpdate(curSubDelay, subSlot.maxClipDelay, PlayerUiController.instance._MissileCurrBar);
    }

    public void Missile()
    {
        isTargeting = false;
        PlayerUiController.instance.Targeting(isTargeting);

        if (curSubDelay < subSlot.maxClipDelay) return;
        curSubDelay = 0;

        GameManager.instance.pool.PlayerBulletsGet(subSlot);

        Debug.Log("Shot Fire");
    }

    public Transform LockTarget()
    {
        target = PlayerAction.instance.target;
        isTargeting = true;
        PlayerUiController.instance.Targeting(isTargeting);

        // 타겟 오브젝트를 찾을수가 없으면 null값 반환.
        if (target == null)
        {
            PlayerUiController.instance.Targeting(false);
            return null;
        }

        PlayerUiController.instance.TargettingTransfrom(target.transform.position);
        return target;
    }

    public void MissileInit(ItemDataSO data)
    {
        subSlot = data;

        for (int index = 0; index < GameManager.instance.pool.playerBulletPrefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.playerBulletPrefabs[index])
            {
                subSlot.prefabID = index; break;
            }
        }
    }
}
