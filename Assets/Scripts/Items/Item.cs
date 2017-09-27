using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{ 
    public string name;
    public string description;
    public Sprite itemImage;
    public Effect itemEffect;
    public Rating rating;
    public bool fuseable;
    public int price;

    public Dictionary<Effect, Item> fuseMap;

    public enum Effect
    {
        None,
        Dubious,
        Dicex2,
        Dicex3,
        Dicex5,
        DiceReverse,
        DicePowerReverse,
        DicePoison,
        Key,
        BossKey,
        PortalGun,
        DuelingGlove,
        StickyFingers,
        SwitchBlock,
        ItemDuel,
        Grave,
        Flashlight,
        Discount,
        ItemFuser,
    }

    public enum Rating
    {
        None,
        Low,
        Medium,
        High,
        Ultra
    }

    public Item FuseItems(Item itemToFuse)
    {
        Item finishedItem = new None();
        if (itemToFuse.fuseable)        
        {
            fuseMap.TryGetValue(itemToFuse.itemEffect, out finishedItem);
        }
        return finishedItem;
    }
}
