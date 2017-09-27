
public class Evnt : NavigatableField {

    public override void ApplyEffect(Player player)
    {
        player.statistics.AddToField(Waypoint.FieldType.Evnt);
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
