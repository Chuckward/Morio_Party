using System.Collections.Generic;
using UnityEngine;

public class DuelingGlove : Item {

	public DuelingGlove()
    {
        name = "Dueling Glove";
        description = "A duel is an arranged engagement in combat between two people, with matched weapons, in accordance with agreed-upon rules." +
            "It's based on honor. Fight well!";
        itemImage = Resources.Load<Sprite>("Items/duel_glove");
        itemEffect = Effect.DuelingGlove;
        rating = Rating.Medium;
        fuseable = true;
        price = 15;

        fuseMap = new Dictionary<Effect, Item>()
        {
            { Effect.Dicex2,        new Dubious() },
            { Effect.Dicex3,        new Dubious() },
            { Effect.DicePoison,    new Dubious() },
            { Effect.DiceReverse,   new Dubious() },
            { Effect.Key,           new Dubious() },
            { Effect.DuelingGlove,  new Dubious() },
            { Effect.StickyFingers, new DuelingItem() }
        };
    }
}
