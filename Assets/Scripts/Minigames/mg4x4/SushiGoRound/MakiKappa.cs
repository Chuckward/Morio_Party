using UnityEngine;

public class MakiKappa : Food {

    public MakiKappa()
    {
        // probability 25%
        foodName = "Kappa Maki";
        price = 100;
        timeout = -1;
        model = Resources.Load<GameObject>("Models/Minigames/SushiGoRound/MakiKappa");
        audio = null;
    }
}
