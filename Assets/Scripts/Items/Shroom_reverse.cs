using System.Collections.Generic;
using UnityEngine;

public class Shroom_reverse : Item {

	public Shroom_reverse()
    {
        name = "Reverse Shroom";
        description = "Move reverse, how cool is that? Applicable to any player. What was that? You are standing right before the star? Too bad. Hope you roll high!";
        itemImage = Resources.Load<Sprite>("Items/shroom_reverse");
        rating = Rating.Low;
        fuseable = true;
        price = 5;

        fuseMap = new Dictionary<Effect, Item>()
        {
            { Effect.Dicex2,        new Shroom_reverse_power() },
            { Effect.Dicex3,        new Shroom_reverse_power() },
            { Effect.DicePoison,    new Dubious() },
            { Effect.DiceReverse,   new Shroom_reverse_power() },
            { Effect.Key,           new Dubious() },
            { Effect.DuelingGlove,  new Dubious() },
            { Effect.SwitchBlock,   new Dubious() }
        };
    }
}
