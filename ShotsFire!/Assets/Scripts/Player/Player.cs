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
        // 2023.04.05 ���� �������� �����
        //playerInput.DownShift += ShiftDown;
        //playerInput.UpShift += ShiftUp;
        // -------------------------------
    }

    private void Update()
    {
        // ���� ����
        mainWeapon.Reload();
        subWeapon.Reload();
        playerAction.UpdateSight();
        ShotsFire();
    }

    private void FixedUpdate()
    {
        PlayerUpdate();
        
        // �ν�Ʈ - 2023.04.05 ���� �������� �����
        //SpeedBoost();
    }

    // W, A, S, D�� ȣ��           - �÷��̾�, �÷��̾� �׸���
    private void PlayerUpdate() 
    {
        playerAction.Move(playerInput.MovementInput, playerInput.OnBoost, playerInput.OnLeft, playerInput.OnRight);
        playerAction.PlayerShadow(playerInput.OnLeft, playerInput.OnRight);
    }

    // ���콺 ���� ȣ��            - �Ѿ�
    private void ShotsFire()
    {
        mainWeapon.Shot(playerInput.ShotsFire);
    }

    /*
    // Shift �� ȣ��               - �ν�Ʈ
    private void SpeedBoost()
    {
        playerAction.BoostSpeed(playerInput.OnBoost);
    }

    // Shift ������ 1�� ȣ��      - �ν�Ʈ�� �ʱ�ȭ
    // 2023.04.05 ���� �������� �����
    private void ShiftDown()
    {
        playerAction.ShiftInputs();
    }

    // Shift ���� 1�� ȣ��       - �ν�Ʈ�� �ʱ�ȭ
    // 2023.04.05 ���� �������� �����
    private void ShiftUp()
    {
        playerAction.ShiftInputs();
    }
    */

    // �����̽��ٷ� ȣ��        - �̻���
    private void LockTarget()
    {
        target = subWeapon.LockTarget();
    }

    private void MissilesFire()
    {
        subWeapon.Missile();
    }

    // F�� ȣ��                - �÷���
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
