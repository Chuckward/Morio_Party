
public class Evnt : NavigatableField {

    public override void ApplyEffect(Player player)
    {
        player.color = Player.Color.Green;
        player.statistics.AddToField(Waypoint.FieldType.Evnt);
        player.currentPlayerState = Player.PlayerState.Reset;
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
