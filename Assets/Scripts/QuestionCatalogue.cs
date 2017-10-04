using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class QuestionCatalogue : MonoBehaviour {

    
    private System.Random rng;

    private void Awake()
    {
        rng = new System.Random();
    }

    private void Update()
    {
        rng.Next();
    }
    
    public Question getRandomQuestion()
    {
        int index = rng.Next(catalogue.Length);

        return catalogue[index];
    }

    private Question[] catalogue = {
        new Question("What is the distance between earth and the sun in Astronomical Units (AU)?", new string[] {"1 AU", "30684.85 AU", "1.561854 10^9 AU"}),
        new Question("", new string[] {"", "", "" })
    };
}
