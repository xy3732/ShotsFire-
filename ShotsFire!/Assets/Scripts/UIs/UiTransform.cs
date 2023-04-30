using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTransform : MonoBehaviour
{
    private void FixedUpdate()
    {
        UitoWorld();
    }

    void UitoWorld()
    {
        gameObject.transform.position = Camera.main.WorldToScreenPoint(Player.instance.transform.position);
    }
}
