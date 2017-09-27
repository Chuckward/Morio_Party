using UnityEngine;

public class Dubious : Item {

	public Dubious()
    {
        name = "Dubious Food";
        description = "It's too gross to even look at. A bizarre smell issues forth from this heap. Eating it won't hurt you, though... probably.";
        itemImage = Resources.Load<Sprite>("Items/dubious");
        itemEffect = Effect.Dubious;
        rating = Rating.Low;
        fuseable = false;
        price = 1;

        fuseMap = null;
    }
}
