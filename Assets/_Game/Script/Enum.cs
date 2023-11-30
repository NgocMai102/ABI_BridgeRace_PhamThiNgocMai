using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType
{
    None = 0,
    White = 1,
    Red = 2,
    Green = 3,
    Pink = 4,
    Blue = 5,
    Yellow = 6,
    Cyan = 7,
    Black = 8,
}

public enum PoolType
{
    None,
    PlatformBrick,
    BridgeBrick,
    PlayerBrick,
    DropBrick,
}

namespace _Game.Framework.Event
{
    public enum EventID
    {
        None = 0,
        CharacterOnNextStage = 1,
        PlayerWin = 2,
        PlayerLose = 3,
    }
}


namespace _UI.Scripts.UI
{
    public enum GameState
    {
        None = 0,
        MainMenu = 1,
        GamePlay = 2,
    }
}