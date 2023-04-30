using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public static Player instance;

    PlayerInputs playerInput;

    [HideInInspector] public PlayerAction playerAction;

    [HideInInspector] public Transform target;

    [HideInInspector] MainWeapon mainWeapon;
    [HideInInspector] SubWeapon subWeapon;

    private void Awake()
    {
        instance = this;

        playerInput = GetComponent<PlayerInputs>();
        playerAction = GetComponent<PlayerAction>();
        mainWeapon = GetComponent<MainWeapon>();
        subWeapon = GetComponent<SubWeapon>();
    }

    private void Start()
    {
        playerInput.LockTarget += LockTarget;
        playerInput.MissileFire += MissilesFire;
        playerInput.FlareFire += FlareFire;
        playerInput.ReroadBullets += ReloadBullets;
        // 2023.04.05 현재 적용할지 고민중
        //playerInput.DownShift += ShiftDown;
        //playerInput.UpShift += ShiftUp;
        // -------------------------------
    }

    private void Update()
    {
        // 무기 관련
        mainWeapon.Reload();
        subWeapon.Reload();
        playerAction.UpdateSight();
        ShotsFire();
    }

    private void FixedUpdate()
    {
        PlayerUpdate();
        
        // 부스트 - 2023.04.05 현재 적용할지 고민중
        //SpeedBoost();
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
        mainWeapon.Shot(playerInput.ShotsFire);
    }

    /*
    // Shift 값 호출               - 부스트
    private void SpeedBoost()
    {
        playerAction.BoostSpeed(playerInput.OnBoost);
    }

    // Shift 누를때 1번 호출      - 부스트값 초기화
    // 2023.04.05 현재 적용할지 고민중
    private void ShiftDown()
    {
        playerAction.ShiftInputs();
    }

    // Shift 땔때 1번 호출       - 부스트값 초기화
    // 2023.04.05 현재 적용할지 고민중
    private void ShiftUp()
    {
        playerAction.ShiftInputs();
    }
    */

    // 스페이스바로 호출        - 미사일
    private void LockTarget()
    {
        target = subWeapon.LockTarget();
    }

    private void MissilesFire()
    {
        subWeapon.Missile();
    }

    // F로 호출                - 플레어
    private void FlareFire()
    {
        playerAction.FlareFire();
    }

    private bool isReload = false;
    private void ReloadBullets()
    {
        if (!isReload) mainWeapon.ReloadBullet(isReload);
    }
}
