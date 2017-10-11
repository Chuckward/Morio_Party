using UnityEngine;


public class Battle : NavigatableField {

    public override void ApplyEffect(Player player)
    {
        player.statistics.AddToField(Waypoint.FieldType.Battle);
        fieldSound.clip = Resources.Load<AudioClip>("Audio/Fields/special");
        fieldSound.Play();
        player.color = Player.Color.Green;
        player.currentPlayerState = Player.PlayerState.Reset;

        // trigger battle minigame
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
