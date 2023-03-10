using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 오브젝트 풀링
    public static GameManager instance;
    public ObjectPooling pool;
    public Player player;    
    [Space(20)]

    // 마우스 커서 텍스쳐
    [SerializeField]
    private Texture2D cursorTexture;

    private void Awake()
    {
        // 프레임 타겟
        Application.targetFrameRate = 60;

        // 인스텐스
        instance = this;

        // 커서
        Vector2 cursorOffset = new Vector2(cursorTexture.width * 0.5f, cursorTexture.height * 0.5f);
        Cursor.SetCursor(cursorTexture, cursorOffset, CursorMode.ForceSoftware);
    }
}
