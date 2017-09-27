using UnityEngine;

public class Key_boss : Item {

	public Key_boss()
    {
        name = "Bosskey";
        description = "Grants you 100% chance of a bossfight at the end of the game.";
        itemEffect = Effect.BossKey;
        itemImage = Resources.Load<Sprite>("Items/bosskey");
        rating = Rating.High;
        fuseable = false;
        price = 20;

        fuseMap = null;
    }
}
