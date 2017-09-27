using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour {

    Image image;
    public Sprite backgroundImage;

	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}
	
	public void SetBackground()
    {
        image.sprite = backgroundImage;
    }
}
