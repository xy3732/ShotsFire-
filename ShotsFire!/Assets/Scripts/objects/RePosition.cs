using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour
{
    [SerializeField] private GameObject ParentObject;

    void OnTriggerExit2D(Collider2D other)
    {
        // �÷��̾� �þ߿��� ���������� �ߵ�
        if(!other.CompareTag("Area")) return;

        // �÷��̾� ����
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector2 playerDir = GameManager.instance.player.playerAction.rigid.velocity;
        // ���� �׶��� ��ġ
        Vector3 myPos = transform.position;

        // �÷��̾�� �Ÿ� ����

        // �þ߿��� �������� �ߵ�
        switch (transform.tag)
        {
            // �׶��� �Ͻ� ������ҿ� ���ġ
            case "Ground":
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;
                // �÷��̾� �ٶ󺸴� ����
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY) transform.Translate(Vector3.right * (dirX * 80));
                else if (diffX < diffY) transform.Translate(Vector3.up * (dirY * 80) );
            break;

            // ���ϰ�� ���� �Ǵ� ���ġ 
            case "enemy":
                if (ParentObject.activeSelf && Spawner.instance.PlayerObject.activeSelf)
                {
                    Vector2 temp = new Vector2(playerPos.x + Random.Range(- 5, 5), playerPos.y + Random.Range(-5, 5));
                    transform.position = temp + (playerDir * 5);
                }
            break;
        }
    }
}
