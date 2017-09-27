using System.Collections.Generic;
using UnityEngine;

public class Shroom_poison : Item {

	public Shroom_poison()
    {
        name = "Poison Shroom";
        description = "You eat it. You feel sick. You throw up. And you can only move up to 3 spaces.";
        itemImage = Resources.Load<Sprite>("Items/shroom_poison");
        itemEffect = Effect.DicePoison;
        rating = Rating.Low;
        price = 5;

        fuseMap = new Dictionary<Effect, Item>()
        {
            { Effect.Dicex2,        new Dubious() },
            { Effect.Dicex3,        new Dubious() },
            { Effect.DicePoison,    new Dubious() },
            { Effect.DiceReverse,   new Dubious() },
            { Effect.Key,           new Dubious() },
            { Effect.DuelingGlove,  new Dubious() },
            { Effect.ItemDuel,      new Dubious() },
            { Effect.SwitchBlock,   new Dubious() }
        };
    }
}
