using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private float transition;
    public GameObject[] plates; // tODO: make private again
    public GameObject[] dish;
    public Food[] foods;

    private int wasabiCount;
    private System.Random rng;
    public AudioSource yeller;

    private void Awake()
    {
        yeller = GameObject.Find("Yeller").GetComponent<AudioSource>();
        foods = new Food[8];
        plates = new GameObject[8];
        dish = new GameObject[8];
        plates = GameObject.FindGameObjectsWithTag("Plate");
        rng = new System.Random();
        System.Threading.Thread.Sleep(200);
        for (int i = 0; i < foods.Length; i++)
        {
            foods[i] = PickRandomDish(); 
            dish[i] = Instantiate(foods[i].model);
            dish[i].transform.parent = plates[i].transform;     // set plate as parent
            dish[i].transform.position = plates[i].transform.position;
            System.Threading.Thread.Sleep(64);                  // give rng time to generate
        }
    }

    // Use this for initialization
    private void Start()
    {
        // fill table with random dishes
        InvokeRepeating("CheckTimeout", 3.0f, 1.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        rng.Next();
        transition = 360.0f * Time.deltaTime / 30;
        transform.Rotate(new Vector3(0, 0, transition));
    }

    private void CheckTimeout()
    {
        wasabiCount = 0;
        for (int i = 0; i < foods.Length; i++)
        {
            if (foods[i].timeout == 0)
            {
                foods[i] = PickRandomDish();
                dish[i] = Instantiate(foods[i].model);
                dish[i].transform.parent = plates[i].transform;     // set plate as parent
                dish[i].transform.position = plates[i].transform.position;
                foods[i].timeout = -1;
                if(foods[i].audio != null)
                {
                    yeller.clip = foods[i].audio;
                    yeller.Play();
                }
            }
            else if(foods[i].timeout > 0)
                foods[i].timeout--;
            if (foods[i].foodName == "Wasabi")
                wasabiCount++;
        }
    }

    public Food PickRandomDish()
    {
        int random = rng.Next(100);
        print(random);

        // TODO wow thats a lot of ifs -> maybe there is a better way?
        if (random < 25)
            return new MakiKappa();
        if (random < 45)
            return new MakiSake();
        if (random < 60)
            return new Tamago();
        if (random < 75)
            return new CocoBalls();
        /*if (random < 85)
            return new Tempura();
        if (random < 95 && wasabiCount < 3)
            return new Wasabi();
        else if (wasabiCount > 2)
            return PickRandomDish();       */ // Wasabi count too high, try again with another random; Possible lifelock if Random gen not really random!
        else
        {
            if (random < 99)
                return new TobikkoRe();
            else
                return new TobikkoOr();
        }
    }
}
