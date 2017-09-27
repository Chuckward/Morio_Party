
public class Battle : NavigatableField {

    public override void ApplyEffect(Player player)
    {

        player.statistics.AddToField(Waypoint.FieldType.Battle);
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
