using UnityEngine;

public class Shroom_ultra : Item {

	public Shroom_ultra()
    {
        name = "Ultra Shroom";
        description = "Dice 5 times, get Schwifty.";
        itemImage = Resources.Load<Sprite>("Items/shroom_ultra");
        rating = Rating.Ultra;
        fuseable = false;
        price = 50;
    }
}
