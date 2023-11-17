using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerState
{
    public class MoveState : PlayerState
    {
        private Entity _entity;
        private PlayerInputSystem _input;
        public MoveState(StateMachine<PlayerState> stateMachine, Entity entity, PlayerInputSystem input) 
            : base(stateMachine)
        {
            _entity = entity;

            _input = input;
        }

        public override void Update()
        {
            _entity.Move();
        }
        public override void StartState()
        {
            UnityEngine.Debug.Log("Start Move");
            _input.Player.Move.canceled += StartIdle;
            _input.Player.Move.performed += SetDirection;
        }
        public override void StopState()
        {
            _input.Player.Move.canceled -= StartIdle;
            _input.Player.Move.performed -= SetDirection;
        }
        private void StartIdle(InputAction.CallbackContext callback)
        {
            _stateMachine.ChangeState(typeof(IdleState));
        }

        private void SetDirection(InputAction.CallbackContext callback)
        {
            Vector2 vector = callback.ReadValue<Vector2>();

            Debug.Log(vector);

            _entity.SetDirection(new Vector3(vector.x, 0, vector.y));
        }
    }
}
