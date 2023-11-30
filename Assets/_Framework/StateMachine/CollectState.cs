using _Game.Pattern.StateMachine;

namespace _Game.Framework.StateMachine
{
    public class CollectState: IState<Bot>
    {
        public void OnEnter(Bot bot)
        {
            bot.MoveToRandomBrick();
        }

        public void OnExecute(Bot bot)
        {
            
        }

        public void OnExit(Bot bot)
        {
            
        }
    }
}