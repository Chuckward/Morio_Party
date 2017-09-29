using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle : NavigatableField {

    public override void ApplyEffect(Player player)
    {
        player.statistics.AddToField(Waypoint.FieldType.Battle);
        soundToPlay = Resources.Load<AudioClip>("Audio/Fields/special");
        fieldSound.clip = soundToPlay;
        fieldSound.Play();


        //SceneManager.LoadSceneAsync();
    }

    public override void PassBy(Player player)
    {
        // nothing happens
    }
}
