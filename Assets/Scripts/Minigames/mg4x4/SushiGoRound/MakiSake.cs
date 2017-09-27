using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakiSake : Food {

    public MakiSake()
    {
        // probability 20%
        foodName = "Sake Maki";
        price = 200;
        timeout = -1;
        model = Resources.Load<GameObject>("Models/Minigames/SushiGoRound/MakiSake");
        audio = null;
    }
}
