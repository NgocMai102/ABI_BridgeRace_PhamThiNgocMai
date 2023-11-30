using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

namespace Utils
{
    public class Constants
    {
        
        public static float RESPAWN_BRICK_TIME = 2f;
        
        public const int MAX_LEVEL = 1;
        public const int MAX_DIS_CHAR_POS = 20;
        public const float TIME_TO_START = 2f;
        
        public const float STUN_TIME = 1.5f;
    }
}

namespace _Game.Character
{
    public class AnimType
    {
        public static string IDLE = "idle";
        public static string RUN = "run";
        public static string FALL = "fall";
        public static string WIN = "win";
    }
}

public class BrickUtils
{ 
    public static Quaternion ROTATION = Quaternion.Euler(0, 90, 0);
}

public class BridgeBrickUtils
{
        public static Vector3 SIZE = new Vector3(2f, 0.5f, 2f);
}

public class TagType
{
        public static string PLAYER = "Player";
        public static string CHARACTER = "Character";
}

