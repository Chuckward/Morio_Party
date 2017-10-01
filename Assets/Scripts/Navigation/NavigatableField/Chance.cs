using UnityEngine;
using UnityEngine.SceneManagement;

public class Chance : NavigatableField {

    public override void ApplyEffect(Player player)
    {
        player.statistics.AddToField(Waypoint.FieldType.Chance);
        soundToPlay = Resources.Load<AudioClip>("Audio/Fields/special");
        fieldSound.clip = soundToPlay;
        fieldSound.Play();
        player.color = Player.Color.Green;
        SceneManager.LoadSceneAsync("chancetime");
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
