using System;
using UnityEngine;

public class Blue : NavigatableField  {

    System.Random rng;

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
        // 1% chance to find a hidden block
        if(rng.Next(100) == 50)
        {
            soundToPlay = Resources.Load<AudioClip>("Audio/Fields/mysteryBlock");
            fieldSound.clip = soundToPlay;
            fieldSound.Play();
            print("1% hit");
            // block appears, information screen, after confirmation roll what kind of thing the player gets + information screen about thing
            // possible: coins (5 (30%), 10(25%), 20(20%)), items(20%) (only low rating) or star (5%)
        }
        soundToPlay = Resources.Load<AudioClip>("Audio/Fields/blue");
        fieldSound.clip = soundToPlay;
        fieldSound.Play();
        player.color = Player.Color.Blue;
        player.ChangeCoins(3);
        player.statistics.AddToField(Waypoint.FieldType.Blue);
        // TODO change player box to respective color
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }

}
