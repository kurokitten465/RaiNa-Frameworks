using UnityEngine;
using KuroKitten.UniKuroKit.StateMachine;
using UniKuroKit.Services;

namespace KuroKitten.UniKuroKit.Samples
{
    public class PlayerWalkState : IState<PlayerContext>
    {
        Rigidbody2D _rb2D;

        public void OnEnter(in PlayerContext ctx)
        {
            _rb2D = ctx.Rigidbody2D;
            Debug.Log($"Entering: {this}");
        }

        public void OnExit(in PlayerContext ctx)
        {
            Debug.Log($"Exiting: {this}");
        }

        public void OnFixedUpdate(in PlayerContext ctx)
        {
            
        }

        public void OnUpdate(in PlayerContext ctx)
        {
            var horizontal = Input.GetAxis("Horizontal");

            var velocity = ctx.Owner.MovementSpeed * horizontal * Time.deltaTime;

            _rb2D.velocity = new Vector2(velocity, _rb2D.velocity.y);

            if (_rb2D.velocity.magnitude <= 0f)
                ctx.Owner.ChangeState(new PlayerIdleState());
        }
    }
}
