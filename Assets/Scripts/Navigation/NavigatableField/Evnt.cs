
public class Evnt : NavigatableField {

    public override void ApplyEffect(Player player)
    {
        player.color = Player.Color.Green;
        player.statistics.AddToField(Waypoint.FieldType.Evnt);
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
