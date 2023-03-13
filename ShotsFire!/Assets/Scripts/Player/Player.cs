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

        // bool �Ķ����
        SpeedBoost();
        ShotsFire();
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
        playerAction.Shot(playerInput.ShotsFire);
    }

    // Shift �� ȣ��               - �ν�Ʈ
    private void SpeedBoost()
    {
        playerAction.BoostSpeed(playerInput.OnBoost);
    }

    // Shift ������ 1�� ȣ��      - �ν�Ʈ�� �ʱ�ȭ
    private void ShiftDown()
    {
        playerAction.ShiftInputs();
    }

    // Shift ���� 1�� ȣ��       - �ν�Ʈ�� �ʱ�ȭ
    private void ShiftUp()
    {
        playerAction.ShiftInputs();
    }

    // �����̽��ٷ� ȣ��        - �̻���
    private void MissilesFire()
    {
        playerAction.Missile();
    }

    // F�� ȣ��                - �÷���
    private void FlareFire()
    {
        playerAction.FlareFire();
    }

}
