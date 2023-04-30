using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWeapon : MonoBehaviour
{
    public static MainWeapon instance;

    [Header("Bullet Settings")]
    public ItemDataSO mainSlot;
    private float curMainDelay;
    [HideInInspector] public int curClip;
    private float curClipDelay;

    private void Awake()
    {
        instance = this;
        curClip = mainSlot.maxClip;
    }

    public void Shot(bool onShot)
    {
        if (curMainDelay < mainSlot.maxShotDelay) return;
        if (curClip <= 0) return;
        curMainDelay = 0;

        if(onShot)
        {
            GameManager.instance.pool.PlayerBulletsGet(mainSlot);
            curClip -= 1;

            PlayerUiController.instance.BarUpdate(curClip, mainSlot.maxClip, PlayerUiController.instance._WeaponCurrBar);
        }
    }

    public void Reload()
    {
        curMainDelay += Time.deltaTime;
        if(curClip <= 0) ReloadBullet(false);
    }

    public bool ReloadBullet(bool active)
    {
        curClip = 0;
        curClipDelay += Time.deltaTime;

        if(curClipDelay >= mainSlot.maxClipDelay)
        {
            curClip = mainSlot.maxClip;
            curClipDelay = 0;

            PlayerUiController.instance.BarUpdate(curClip, mainSlot.maxClip, PlayerUiController.instance._WeaponCurrBar);
            PlayerUiController.instance.BarUpdate(curClipDelay, mainSlot.maxClipDelay, PlayerUiController.instance._ReloadWeaponBar);
            return false;
        }

        PlayerUiController.instance.BarUpdate(curClip, mainSlot.maxClip, PlayerUiController.instance._WeaponCurrBar);
        PlayerUiController.instance.BarUpdate(curClipDelay, mainSlot.maxClipDelay, PlayerUiController.instance._ReloadWeaponBar);
        return true;
    }

    public void BulletInit(ItemDataSO data)
    {
        mainSlot = data;

        for (int index = 0; index < GameManager.instance.pool.playerBulletPrefabs.Length; index++)
        {
            if (data.projectile == GameManager.instance.pool.playerBulletPrefabs[index])
            {
                mainSlot.prefabID = index; break;
            }
        }
    }
}
