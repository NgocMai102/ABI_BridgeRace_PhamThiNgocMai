using UnityEngine;

namespace _Game.Framework.StateMachine
{
    public class MoveToFinishPointState: IState<Bot>
    {
        public void OnEnter(Bot bot)
        {
            bot.MoveToFinishPos();
        }

        public void OnExecute(Bot bot)
        {
            if (!bot.CheckBridge(bot.NextPosition))
            {
                bot.NotEnoughBrick();
            }

            bot.CheckGate();
        }

        public void OnExit(Bot bot)
        {
            
        }
    }
}