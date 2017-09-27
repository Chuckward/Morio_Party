using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TobikkoRe : Food {

    public TobikkoRe()
    {
        // probability 5%
        foodName = "Tobikko Red";
        price = 800;
        timeout = -1;
        model = Resources.Load<GameObject>("Models/Minigames/SushiGoRound/TobikkoRed");
        audio = Resources.Load<AudioClip>("Audio/Minigames/SushiGoRound/RobikoRed");
    }
}
