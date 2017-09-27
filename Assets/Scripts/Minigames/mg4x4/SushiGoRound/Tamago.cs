using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tamago : Food {

    public Tamago()
    {
        // probability 15%
        foodName = "Tamago";
        price = 250;
        timeout = -1;
        model = Resources.Load<GameObject>("Models/Minigames/SushiGoRound/Tamago");
        audio = null;
    }
}
