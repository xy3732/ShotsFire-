using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePosition : MonoBehaviour
{
    [SerializeField] private GameObject ParentObject;

    void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어 시야에서 없어질때만 발동
        if(!other.CompareTag("Area")) return;

        // 플레이어 정보
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector2 playerDir = GameManager.instance.player.playerAction.rigid.velocity;
        // 현재 그라운드 위치
        Vector3 myPos = transform.position;

        // 플레이어와 거리 측정

        // 시야에서 없어지면 발동
        switch (transform.tag)
        {
            // 그라운드 일시 다음장소에 재배치
            case "Ground":
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;
                // 플레이어 바라보는 방향
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;

                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                if (diffX > diffY) transform.Translate(Vector3.right * (dirX * 80));
                else if (diffX < diffY) transform.Translate(Vector3.up * (dirY * 80) );
            break;

            // 적일경우 삭제 또는 재배치 
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
