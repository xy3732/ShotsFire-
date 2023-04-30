using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class HUD : MonoBehaviour
{
    public enum InfoType { Kill, Timer, Level, Bullets }
    public InfoType type;

    TextMeshProUGUI textObj;

    private void Awake()
    {
        textObj = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Kill:
                
                break;

            case InfoType.Timer:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                textObj.text = string.Format("{0:D1}:{1:D2}",min, sec);
                break;

            case InfoType.Level:
                int level = PlayerAction.instance.level;
                textObj.text = string.Format("LV. {0:D1}",level);
                break;

            case InfoType.Bullets:
                int curBullets = MainWeapon.instance.curClip;
                int maxBullets = MainWeapon.instance.mainSlot.maxClip;

                if (curBullets == 0) textObj.color = new Color32(255, 85, 0, 255);
                else if (curBullets <= maxBullets * 0.33f) textObj.color = new Color32(255, 195, 75, 255);
                else textObj.color = new Color32(255, 255, 255, 255);

                textObj.text = string.Format("{0:D1} | {1:D2}", curBullets, maxBullets);
                break;
        }
    }
}
