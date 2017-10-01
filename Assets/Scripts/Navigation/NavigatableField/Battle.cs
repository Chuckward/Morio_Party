using UnityEngine;


public class Battle : NavigatableField {

    public override void ApplyEffect(Player player)
    {
        player.statistics.AddToField(Waypoint.FieldType.Battle);
        soundToPlay = Resources.Load<AudioClip>("Audio/Fields/special");
        fieldSound.clip = soundToPlay;
        fieldSound.Play();
        player.color = Player.Color.Green;

        // trigger battle minigame
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
