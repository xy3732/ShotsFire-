using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    // 오브젝트 풀링
    public static GameManager instance;
    public ObjectPooling pool;
    public Player player;

    [Header("VCAM Settings")]
    public CinemachineVirtualCamera vcam_0;
    

    [Header("Mouse Settings")]
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

    private void Update()
    {
        scrollControll();
    }

    private void scrollControll()
    {
        float min = 7;
        float max = 9;
        float temp = vcam_0.m_Lens.OrthographicSize;

        float speed = 2f;
        float scroll = Input.GetAxis("Mouse ScrollWheel") * speed;

        temp += scroll;
        if (min > temp) temp = min;
        else if (max < temp) temp = max;
        vcam_0.m_Lens.OrthographicSize = temp;



        Debug.Log(vcam_0.m_Lens.OrthographicSize);
    }

}
