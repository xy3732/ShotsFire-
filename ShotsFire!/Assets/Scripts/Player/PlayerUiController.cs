using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;
public class PlayerUiController : MonoBehaviour
{
    public Image _HpBar;
    public Image _ExpBar;
    public Image _WeaponCurrBar;
    public Image _MissileCurrBar;
    public Image _ReloadWeaponBar;

    [Space(20)]
    public GameObject _TargetImg;

    public static PlayerUiController instance;

    private void Awake()
    {
        instance = this;
    }

    public void Targeting(bool _active)
    {
        _TargetImg.SetActive(_active);
    }

    public void TargettingTransfrom( Vector3 Pos)
    { 
        _TargetImg.transform.position = Pos;
    }

    public void BarUpdate(float _value, float _maxValue, Image _bar)
    {
        //float amount = (float)Math.Truncate((_value / _maxValue) * 10) * 0.1f;
        //_HpBar.fillAmount = amount;

        float tempAmount = (_value / _maxValue);
        _bar.fillAmount = tempAmount;
    }

    public void ExoBarUpdate()
    {
        BarUpdate(PlayerAction.instance.curExp, PlayerAction.instance.maxExp, _ExpBar);
    }
}
