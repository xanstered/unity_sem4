namespace Player
{

    public class Player : StateMachine.StateMachine
    {

        private void Start()
        {
           Begin(new PlayerGroundedState(this));
        }

        void Update()
        {

        }
    }
}
