using UnityEngine;

namespace _Game.Utils
{
    [CreateAssetMenu(fileName = "BotConfig", menuName = "Scriptable/Bot Config", order = 2)]
    public class BotConfig : ScriptableObject
    {
        public int moveSpeed;
    }
}