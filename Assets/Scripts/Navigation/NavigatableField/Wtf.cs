
public class Wtf : NavigatableField {

    public override void ApplyEffect(Player player)
    {
        player.color = Player.Color.Green;
        player.statistics.AddToField(Waypoint.FieldType.Wtf);
        player.currentPlayerState = Player.PlayerState.Reset;
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
