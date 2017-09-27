using System.Collections.Generic;
using UnityEngine;

public abstract class Waypoint : MonoBehaviour {

    public enum FieldType
    {
        Blue,
        Red,
        Evnt,
        Bank,
        Bowser,
        Chance,
        Wtf,
        Battle,
        Star,
        Item,
        Boo,
        Shop,
        Special,
        None
    }

    public FieldType field;

    public Dictionary<FieldType, System.Type> fieldMap = new Dictionary<FieldType, System.Type>()
    {
        { FieldType.Blue, typeof(Blue) },
        { FieldType.Red, typeof(Red) },
        { FieldType.Bank, typeof(Bank) },
        { FieldType.Battle, typeof(Battle) },
        { FieldType.Boo, typeof(Boo) },
        { FieldType.Bowser, typeof(Bowser) },
        { FieldType.Chance, typeof(Chance) },
        { FieldType.Evnt, typeof(Evnt) },
        { FieldType.Item, typeof(ItemField) },
        { FieldType.Shop, typeof(Shop) },
        { FieldType.Star, typeof(Star) }
    };

    public Dictionary<FieldType, string> fieldPath = new Dictionary<FieldType, string>()
    {
        { FieldType.Blue, "Fields/Field_blue" },
        { FieldType.Red, "Fields/Field_red" },
        { FieldType.Bank, "Fields/Field_bank" },
        { FieldType.Battle, "Fields/Field_battle" },
        { FieldType.Boo, "" },
        { FieldType.Bowser, "Fields/Field_bowser" },
        { FieldType.Chance, "Fields/Field_chance" },
        { FieldType.Evnt, "Fields/Field_evnt" },
        { FieldType.Item, "Fields/Field_item" },
        { FieldType.Shop, "Fields/Field_arrow" },
        { FieldType.Star, "Fields/Field_star" }
    };

    public abstract Vector3 LinearPosition(Waypoint previous, float ratio, Waypoint next);

    public abstract Quaternion Orientation(Waypoint previous, float ratio, Waypoint next);
}
