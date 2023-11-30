using _Game.Character;
using _Game.Framework.StateMachine;
using UnityEngine;
using Utils;

namespace _Game.Pattern.StateMachine
{
    public class FallState: IState<Bot>
    {
        private float _timer;
        private const float StunTime = Constants.STUN_TIME;
        
        public void OnEnter(Bot bot)
        {
            _timer = 0;
            bot.isFalling = true;
            
            bot.StopMove();
            bot.ChangeAnim(AnimType.FALL);
        }

        public void OnExecute(Bot bot)
        {
            _timer += Time.deltaTime;
            if (_timer >= StunTime)
            {
                bot.ChangeState(bot.CollectState);
            }
        }

        public void OnExit(Bot bot)
        {
            bot.isFalling = false;
        }
    }
}