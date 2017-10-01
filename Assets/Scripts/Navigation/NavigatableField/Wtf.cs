
public class Wtf : NavigatableField {

    public override void ApplyEffect(Player player)
    {
        player.color = Player.Color.Green;
        player.statistics.AddToField(Waypoint.FieldType.Wtf);
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
