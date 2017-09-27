
public class ItemField : NavigatableField {

    public override void ApplyEffect(Player player)
    {

        player.statistics.AddToField(Waypoint.FieldType.Item);
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }

	
}
