using UnityEngine.InputSystem;
using UnityEngine;

namespace PlayerState
{
    public class IdleState : PlayerState
    {
        private Entity _entity;
        private PlayerInputSystem _input;
        public IdleState(StateMachine<PlayerState> stateMachine, Entity entity, PlayerInputSystem input) 
            : base (stateMachine)
        {
            _entity = entity;
            _input = input;
        }


        public override void StartState()
        {
            Debug.Log("Start Idle");
            _input.Player.Move.performed += StartMove;
        }
        public override void StopState()
        {
            _input.Player.Move.performed -= StartMove;
        }

        private void StartMove(InputAction.CallbackContext callback)
        {
            Vector2 vector = callback.ReadValue<Vector2>();

            _entity.SetDirection(new Vector3(vector.x, 0, vector.y));
            Debug.Log("11111");
            _stateMachine.ChangeState(typeof(MoveState));
        }
    }
}
