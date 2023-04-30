using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;
public class UIItem : MonoBehaviour
{
    public ItemDataSO data;
    public int level;

    Image icon;
    TextMeshProUGUI levelText;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        levelText = texts[0];
    }

    private void LateUpdate()
    {
        levelText.text = "LV." + (level + 1);
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
            case ItemDataSO.ItemType.Bullet:
                if(level == 0)
                {
                    MainWeapon.instance.BulletInit(data);
                }
                else
                {
                    int nextDamage = Mathf.RoundToInt(data.baseDamage);
                    int nextCount = 0;

                    nextDamage += Mathf.RoundToInt(data.baseDamage * data.damages[level]);
                    nextCount += data.count[level];
                    data.nowDamage = nextDamage;
                }
                break;
            case ItemDataSO.ItemType.Missile:
                if (level == 0)
                {
                    SubWeapon.instance.MissileInit(data);
                }
                break;
            case ItemDataSO.ItemType.Engine:
                break;
            case ItemDataSO.ItemType.Repair:
                break;
            default:
                break;
        }

        level++;

        if(level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false; 
        }
    }
}
