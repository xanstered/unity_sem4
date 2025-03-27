using StateMachine;
using UnityEngine;

namespace Player
{

    public class PlayerGroundedState : State
    {
        private Vector3 _input;
        private float _currentMovementSpeed = 15f;
        private Rigidbody _rb;

        public PlayerGroundedState(StateMachine.StateMachine stateMachine) : base(stateMachine) { }

        public override void Update()
        {
            _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            _rb.velocity += _input * (_currentMovementSpeed * Time.deltaTime);
        }
    }
}
