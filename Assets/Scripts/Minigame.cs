using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame {

    public string minigame_name;
    public string sceneName;
    public MinigameType type;
    public Difficulty difficulty;

    public enum MinigameType
    {
        VS13,
        VS4,
        VS22,
        Single,
        Item,
        Duel,
        Bowser,
        Battle,
        BattleRoyal,
        Boss
    }

    public enum Difficulty
    {
        easy,
        medium,
        hard,
        diabolic
    }
}
