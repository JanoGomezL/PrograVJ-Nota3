using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_PlayerController;

    private ULShooter m_ULShooterActions;
    private InputAction m_MovementAction;
    private InputAction m_JumpAction;
    private InputAction m_FireAction;

    private void Awake()
    {
        m_ULShooterActions = new ULShooter();
    }

    private void OnEnable()
    {
        m_MovementAction = m_ULShooterActions.Player.Move;
        m_MovementAction.Enable();

        m_JumpAction = m_ULShooterActions.Player.Jump;
        m_JumpAction.performed += DoJump;
        m_JumpAction.Enable();

        m_FireAction = m_ULShooterActions.Player.Fire;
        m_FireAction.performed += DoFire;
        m_FireAction.Enable();
    }

    private void DoFire(InputAction.CallbackContext obj)
    {
        m_PlayerController.Fire();
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        m_PlayerController.Jump();
    }

    private void Update()
    {
        Vector2 movement = m_MovementAction.ReadValue<Vector2>();
        m_PlayerController.Move(movement);
    }

    private void OnDisable()
    {
        m_MovementAction.Disable();
        m_JumpAction.performed -= DoJump;
        m_JumpAction.Disable();
    }
}
