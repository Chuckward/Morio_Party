using System.Collections.Generic;
using UnityEngine;

public class Shroom_red : Item {

    public Shroom_red()
    {
        name = "Red Shroom";
        description = "Lets you roll two dices.";
        itemImage = Resources.Load<Sprite>("Items/shroom");
        itemEffect = Effect.Dicex2;
        fuseable = true;
        rating = Rating.Low;
        price = 5;

        fuseMap = new Dictionary<Effect, Item>()
        {
            { Effect.Dicex2,        new Shroom_gold() },
            { Effect.Dicex3,        new Dubious() },
            { Effect.DicePoison,    new Dubious() },
            { Effect.DiceReverse,   new Shroom_reverse_power() },
            { Effect.Key,           new Dubious() },
            { Effect.DuelingGlove,  new Dubious() }
        };
    }
}
