using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //���콺 Ŀ�� �ؽ���
    [SerializeField]
    private Texture2D cursorTexture;

    private void Awake()
    {
        // ������ Ÿ��
        Application.targetFrameRate = 60;

        // Ŀ��
        Vector2 cursorOffset = new Vector2(cursorTexture.width * 0.5f, cursorTexture.height * 0.5f);
        Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.ForceSoftware);
    }
}
