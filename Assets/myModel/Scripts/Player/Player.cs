using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerState;


public class Player : Entity
{
    private StateMachine<PlayerState.PlayerState> _stateMachine;

    private PlayerInputSystem _playerInputSystem;

    private void Awake()
    {
        SetValue();
        SetStateMachine();
    }
    private void OnEnable()
    {
        _playerInputSystem.Enable();
    }

    private void OnDisable()
    {
        _playerInputSystem.Disable();
    }

    private void Update()
    {
        _stateMachine.State.Update();
    }

    public override void Move()
    {
        _entityInfo.CharacterController.Move(GlobalDirection * Time.deltaTime * _entityInfo.Speed);
    }


    private void InputRotate(InputAction.CallbackContext callback)
    {
        _entityInfo.PlayerTransform.localEulerAngles += new Vector3(0, callback.ReadValue<Vector2>().x, 0) * Time.deltaTime * _entityInfo.RotateSpeed;
    }

    private void SetValue()
    {
        _playerInputSystem = new PlayerInputSystem();

        _playerInputSystem.Player.CameraRotate.performed += InputRotate;
    }
    private void SetStateMachine()
    {
        _stateMachine = new StateMachine<PlayerState.PlayerState>();

        Dictionary<Type, PlayerState.PlayerState> stateDictionary = new()
        {
            { typeof(IdleState), new IdleState(_stateMachine, this, _playerInputSystem) },
            { typeof(MoveState), new MoveState(_stateMachine, this, _playerInputSystem) },

        };

        _stateMachine.SetInfo(stateDictionary);
        _stateMachine.ChangeState(typeof(IdleState));
    }
}
