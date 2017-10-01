using UnityEngine;
using UnityEngine.SceneManagement;


public class Bowser : NavigatableField {

    public override void ApplyEffect(Player player)
    {
        player.statistics.AddToField(Waypoint.FieldType.Bowser);
        soundToPlay = Resources.Load<AudioClip>("Audio/Fields/bowser");
        fieldSound.clip = soundToPlay;
        fieldSound.Play();
        player.color = Player.Color.Red;
        SceneManager.LoadSceneAsync("BowserSpace");
    }

    public override void PassBy(Player player)
    {
        // thankfully, nothing happens
    }
}
