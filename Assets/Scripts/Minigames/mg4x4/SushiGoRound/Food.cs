using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food {

    public string foodName;
    public int price;
    public int timeout;
    public GameObject model;
    public AudioClip audio;

	public void eat()
    {
        timeout = 5;
        model = Resources.Load<GameObject>("Models/Minigames/SushiGoRound/Empty");
    }
}
