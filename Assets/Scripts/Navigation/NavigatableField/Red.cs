
public class Red : NavigatableField {

    private void Awake()
    {
        //visibleField = (GameObject)Instantiate(Resources.Load("Fields/Field_red"), gameObject.transform.parent, false);
    }

    public override void ApplyEffect(Player player)
    {
        player.ChangeCoins(-3);
        player.statistics.AddToField(Waypoint.FieldType.Red);
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
