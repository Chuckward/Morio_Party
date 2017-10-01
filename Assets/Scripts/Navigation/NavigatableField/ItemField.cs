using System;
using UnityEngine;

public class ItemField : NavigatableField {

    private System.Random rng;

    private void Awake()
    {
        rng = new System.Random();
    }

    private void Update()
    {
        rng.Next();
    }

    public override void ApplyEffect(Player player)
    {
        soundToPlay = Resources.Load<AudioClip>("Audio/Fields/special");
        fieldSound.clip = soundToPlay;
        fieldSound.Play();
        player.color = Player.Color.Green;
        player.statistics.AddToField(Waypoint.FieldType.Item);
        if(rng.Next(2) == 0)
        {
            // trigger question
        } else
        {
            // trigger itemgame
        }
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }

	
}
