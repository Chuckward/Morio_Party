
public class Bowser : NavigatableField {

    public override void ApplyEffect(Player player)
    {
        player.statistics.AddToField(Waypoint.FieldType.Bowser);
    }

    public override void PassBy(Player player)
    {
        // thankfully, nothing happens
    }
}
