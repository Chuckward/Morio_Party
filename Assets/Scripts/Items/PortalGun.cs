using UnityEngine;

public class PortalGun : Item {
    
    public PortalGun()
    {
        name = "Portal Gun";
        description = "Shoot it, go through it and discover any dimension of the infinite universes. You can also use this to switch with any player you want.";
        itemImage = Resources.Load<Sprite>("Items/portalgun");
        itemEffect = Effect.PortalGun;
        rating = Rating.High;
        fuseable = false;
        price = 30;

        fuseMap = null;
    }
}
