using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasabi : Food {

    public Wasabi()
    {
        // probability 10% but max 3
        foodName = "Wasabi";
        price = 50;
        timeout = -1;
        model = Resources.Load<GameObject>("Models/Minigames/SushiGoRound/Wasabi");
    }
}
