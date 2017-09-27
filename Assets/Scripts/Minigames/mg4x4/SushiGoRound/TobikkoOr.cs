using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TobikkoOr : Food {

    public TobikkoOr()
    {
        // probability 1%
        foodName = "Tobikko";
        price = 1000;
        timeout = -1;
        model = Resources.Load<GameObject>("Models/Minigames/SushiGoRound/TobikkoOrange");
        audio = Resources.Load<AudioClip>("Audio/Minigames/SushiGoRound/RobikoOrange");
    }
}
