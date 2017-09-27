using System.Collections.Generic;
using UnityEngine;

public class Key : Item {

	public Key()
    {
        name = "Key";
        description = "It's a key. Much better! It's a drawing of a key. Gentlemen, what do keys do? Keys... unlock... things? " +
            "And whatever this key unlocks, inside there's something valueable... Soo we're setting out to find whatever this key unlocks. " +
            "No. If we don't have the key, we can't open whatever it is we don't have that it unlocks. So what purpose would be served " +
            "in finding whatever need be unlocked... which we don't have... without first having found the key what unlocks it?";
        itemImage = Resources.Load<Sprite>("Items/key");
        itemEffect = Effect.Key;
        rating = Rating.Low;
        fuseable = true;
        price = 5;

        fuseMap = new Dictionary<Effect, Item>()
        {
            { Effect.Dicex2,        new Dubious() },
            { Effect.Dicex3,        new Dubious() },
            { Effect.DicePoison,    new Dubious() },
            { Effect.DiceReverse,   new Dubious() },
            { Effect.Key,           new Key_boss() },
            { Effect.DuelingGlove,  new Dubious() }
        };
    }
}
