using UnityEngine;
using UnityEngine.UI;

public abstract class NavigatableField : MonoBehaviour {

    public AudioClip soundToPlay;
    public AudioSource fieldSound;

    public Image HUD_Info;
    public Text HUD_Info_Text;
    public Image HUD_Info_Avatar;
    private MapLogic map;

    public void Start()
    {
        map = GameObject.FindWithTag("Map").GetComponent<MapLogic>();
        HUD_Info = map.HUD_Info;
        HUD_Info_Text = map.HUD_Info_Text;
        HUD_Info_Avatar = map.HUD_Info_Avatar;

        fieldSound = GameObject.Find("Map/FieldSound").GetComponent<AudioSource>();
    }

    public abstract void ApplyEffect(Player player);

    public abstract void PassBy(Player player);

}
