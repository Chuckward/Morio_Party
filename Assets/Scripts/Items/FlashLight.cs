using UnityEngine;

public class FlashLight : Item {
    
    public FlashLight()
    {
        name = "Flashlight 07/15";
        description = "Ghosts are scared of light. Pretty obvious. Battery has charge for one go. Protect your valueables!";
        itemEffect = Effect.Flashlight;
        itemImage = Resources.Load<Sprite>("Items/flashlight");
        rating = Rating.Medium;
        fuseable = false;
        price = 10;

        fuseMap = null;
    }
}
