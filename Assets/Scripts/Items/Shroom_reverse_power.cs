using UnityEngine;

public class Shroom_reverse_power : Item {

	public Shroom_reverse_power()
    {
        name = "Power Reverse Shroom";
        description = "Be like the french. Crank in the full reverse and retreat!! Roll twice, move further reverse," +
            " isn't that handy! Use it on yourself or others - but why sharing?";
        itemImage = Resources.Load<Sprite>("Items/shroom_dual");
        rating = Rating.Medium;
        fuseable = false;
        price = 15;
    }
}
