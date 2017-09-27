using UnityEngine;

public class None : Item {
    
    public None()
    {
        name = "";
        description = "";
        itemImage = Resources.Load<Sprite>("Items/no_item");
        itemEffect = Effect.None;
        rating = Rating.Low;
        fuseable = false;
        price = 0;

        fuseMap = null;
    }
}
