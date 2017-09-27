using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tempura : Food {

    public Tempura()
    {
        // probability 10%
        foodName = "Tempura";
        price = 600;
        timeout = -1;
        model = Resources.Load<GameObject>("Models/Minigames/SushiGoRound/Tempura");
        audio = Resources.Load<AudioClip>("Audio/Minigames/SushiGoRound/Tempura");
    }

}
