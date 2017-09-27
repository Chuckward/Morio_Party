
public class Star : NavigatableField {


    public override void ApplyEffect(Player player)
    {
        // one cannot land directly on star!
    }

    public override void PassBy(Player player)
    {
        // buy star!
        player.statistics.AddToField(Waypoint.FieldType.Star);
    }
}
