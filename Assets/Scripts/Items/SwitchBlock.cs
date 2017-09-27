using System.Collections.Generic;
using UnityEngine;

public class SwitchBlock : Item {
    
    public SwitchBlock()
    {
        name = "Switch Block";
        description = "You can change places with a random player.";
        itemImage = Resources.Load<Sprite>("Items/switchblock");
        itemEffect = Effect.SwitchBlock;
        rating = Rating.Medium;
        fuseable = true;
        price = 10;

        fuseMap = new Dictionary<Effect, Item>()
        {
            { Effect.Dicex2,        new Dubious() },
            { Effect.Dicex3,        new Dubious() },
            { Effect.DicePoison,    new Dubious() },
            { Effect.DiceReverse,   new Dubious() },
            { Effect.Key,           new Dubious() },
            { Effect.DuelingGlove,  new Dubious() },
            { Effect.ItemDuel,      new Dubious() },
            { Effect.SwitchBlock,   new PortalGun() }
        };
    }
}
