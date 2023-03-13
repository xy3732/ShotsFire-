using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class PlayerInputs : MonoBehaviour
{
    // 부스트 작동
    public Action MissileFire ,FlareFire, DownShift, UpShift;

    public bool ShotsFire { get; private set; }
    public bool OnBoost { get; private set; }
    public bool OnLeft  { get; private set; }
    public bool OnRight { get; private set; }

    // 플레이어 이동
    public Vector2 MovementInput { get; private set; }


    private void Update()
    {
        KeycodeInput();
    }

    private void KeycodeInput()
    {
        GetMovementInput();
        Weapons();
    }


    private void GetMovementInput()
    {
        // 부스트
        GetShiftInput();
        ShiftDown();
        ShiftUp();

        MovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        OnLeft = Input.GetKey(KeyCode.A);
        OnRight = Input.GetKey(KeyCode.D);
    }

    private void Weapons()
    {
        GetMouseInput();
        GetSpaceInput();
        GetFlareInput();
    }

    private void GetShiftInput()
    {
        OnBoost = Input.GetKey(KeyCode.LeftShift);
    }

    private void ShiftDown()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)) DownShift?.Invoke();
    }

    private void ShiftUp()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) UpShift?.Invoke();
    }

    private void GetMouseInput()
    {
        ShotsFire = Input.GetMouseButton(0);
    }

    private void GetFlareInput()
    {
        if(Input.GetKeyDown(KeyCode.F)) FlareFire?.Invoke();
    }

    private void GetSpaceInput()
    {
        if (Input.GetKeyDown(KeyCode.Space)) MissileFire?.Invoke();
    }
}
