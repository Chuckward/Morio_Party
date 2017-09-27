using System.Collections.Generic;
using UnityEngine;

public class Shroom_gold : Item {

    public Shroom_gold()
    {
        name = "Golden Shroom";
        description = "Lets you roll three dices. It has a nice crown, hasn't it?";
        itemImage = Resources.Load<Sprite>("Items/shroom_golden");
        itemEffect = Effect.Dicex3;
        rating = Rating.Medium;
        fuseable = true;
        price = 15;

        fuseMap = new Dictionary<Effect, Item>()
        {
            { Effect.Dicex2,        new Dubious() },
            { Effect.Dicex3,        new Dubious() },
            { Effect.DicePoison,    new Dubious() },
            { Effect.DiceReverse,   new Shroom_reverse_power() },
            { Effect.Key,           new Dubious() },
            { Effect.DuelingGlove,  new Dubious() }
        };
    }  
}
