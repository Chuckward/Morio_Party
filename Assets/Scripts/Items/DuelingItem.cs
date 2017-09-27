using UnityEngine;

public class DuelingItem : Item
{
    public DuelingItem()
    {
        name = "Sticky Glove";
        description = "Works like a dueling glove, but this time, you get to battle for your items!";
        itemImage = Resources.Load<Sprite>("Items/duel_item");
        itemEffect = Effect.ItemDuel;
        rating = Rating.Medium;
        fuseable = false;
        price = 20;

        fuseMap = null;
    }
}
