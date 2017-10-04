
public class Boo : NavigatableField {

    public override void ApplyEffect(Player player)
    {
       // one cannot land directly on boos field
    }

    public override void PassBy(Player player)
    {
        player.statistics.AddToField(Waypoint.FieldType.Boo);
        player.currentPlayerState = Player.PlayerState.Move;
    }
}
