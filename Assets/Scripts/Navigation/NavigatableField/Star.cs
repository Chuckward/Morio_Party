using UnityEngine;

public class Star : NavigatableField {


    public override void ApplyEffect(Player player)
    {
        // one cannot land directly on star!
    }

    public override void PassBy(Player player)
    {
        // buy star!
        soundToPlay = Resources.Load<AudioClip>("Audio/Fields/starField");
        fieldSound.clip = soundToPlay;
        fieldSound.Play();
        player.statistics.AddToField(Waypoint.FieldType.Star);
    }
}
