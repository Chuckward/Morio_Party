
public class Chance : NavigatableField {

    public override void ApplyEffect(Player player)
    {
        player.statistics.AddToField(Waypoint.FieldType.Chance);
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
