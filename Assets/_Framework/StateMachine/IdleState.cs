using UnityEngine;
using _Game.Utils;
using Utils;
using _Game.Character;

namespace _Game.Framework.StateMachine
{
    public class IdleState : IState<Bot>
    {
        private float _timer;
        private const float IdleTime = Constants.TIME_TO_START;

        public void OnEnter(Bot bot)
        {
            _timer = 0;
            bot.ChangeAnim(AnimType.IDLE);
        }

        public void OnExecute(Bot bot)
        {
            _timer += Time.deltaTime;
            if (_timer >= IdleTime)
            {
                bot.ChangeState(bot.CollectState);
            }
        }

        public void OnExit(Bot bot)
        {
            
        }
    }
}