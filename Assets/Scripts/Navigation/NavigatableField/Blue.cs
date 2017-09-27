using System;

public class Blue : NavigatableField  {

    public override void ApplyEffect(Player player)
    {
        // 1% chance to find a hidden block
        if(new System.Random().Next(100) == 50)
        {
            print("1% hit");
            // block appears, information screen, after confirmation roll what kind of thing the player gets + information screen about thing
            // possible: coins (5 (30%), 10(25%), 20(20%)), items(20%) (only low rating) or star (5%)
        }
        player.ChangeCoins(3);
        player.statistics.AddToField(Waypoint.FieldType.Blue);
        // TODO change player box to respective color
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
