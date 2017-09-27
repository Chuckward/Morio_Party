using UnityEngine;

public class StickyFingers : Item {

	public StickyFingers()
    {
        name = "Sticky Fingers";
        description = "What's so hard to understand? Grab a pocket of a passing player and get an item. EOL.";
        itemEffect = Effect.StickyFingers;
        itemImage = Resources.Load<Sprite>("Items/sticky");
        rating = Rating.Medium;
        fuseable = false;
        price = 20;

        fuseMap = null;
    }
}
