using UnityEngine;

public class Grave : Item {

    public Grave()
    {
        name = "Grave";
        description = "A grave contains a dead body. It's soul shall rest in piece. But maybe it still roams around? Try to call it!";
        itemImage = Resources.Load<Sprite>("Items/grave");
        itemEffect = Effect.Grave;
        rating = Rating.High;
        fuseable = false;
        price = 15;

        fuseMap = null;
    }
}
