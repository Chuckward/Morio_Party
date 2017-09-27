using UnityEngine;

public class Discount : Item {

    public Discount()
    {
        name = "Discount 50%";
        description = "Gives you a discount of 50% for the next item shopping tour. Only works in item shops.";
        itemImage = Resources.Load<Sprite>("Items/discount");
        itemEffect = Effect.Discount;
        rating = Rating.High;
        fuseable = false;
        price = 30;

        fuseMap = null;
    }
}
