using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerInputs playerInput;
    PlayerAction playerAction;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInputs>();
        playerAction = GetComponent<PlayerAction>();
    }

    private void Start()
    {
        playerInput.MissileFire += MissilesFire;
        playerInput.FlareFire += FlareFire;
        playerInput.DownShift += ShiftDown;
        playerInput.UpShift += ShiftUp;
    }

    private void FixedUpdate()
    {
        PlayerUpdate();

        // bool 파라메터
        SpeedBoost();
        ShotsFire();
    }

    // W, A, S, D값 호출           - 플레이어, 플레이어 그림자
    private void PlayerUpdate() 
    {
        playerAction.Move(playerInput.MovementInput, playerInput.OnBoost, playerInput.OnLeft, playerInput.OnRight);
        playerAction.PlayerShadow(playerInput.OnLeft, playerInput.OnRight);
    }

    // 마우스 왼쪽 호출            - 총알
    private void ShotsFire()
    {
        playerAction.Shot(playerInput.ShotsFire);
    }

    // Shift 값 호출               - 부스트
    private void SpeedBoost()
    {
        playerAction.BoostSpeed(playerInput.OnBoost);
    }

    // Shift 누를때 1번 호출      - 부스트값 초기화
    private void ShiftDown()
    {
        playerAction.ShiftInputs();
    }

    // Shift 땔때 1번 호출       - 부스트값 초기화
    private void ShiftUp()
    {
        playerAction.ShiftInputs();
    }

    // 스페이스바로 호출        - 미사일
    private void MissilesFire()
    {
        playerAction.Missile();
    }

    // F로 호출                - 플레어
    private void FlareFire()
    {
        playerAction.FlareFire();
    }

}
