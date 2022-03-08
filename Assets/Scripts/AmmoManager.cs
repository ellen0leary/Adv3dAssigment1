using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public GameObject ammoPack;
    GameObject[] locations;
    int maxPacks = 3;
    int currentPacks;
    // Start is called before the first frame update
    void Start()
    {
        currentPacks = 0;
        locations = new GameObject[] { GameObject.Find("AP1"), GameObject.Find("AP2"), GameObject.Find("AP3"), GameObject.Find("AP4") };
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPacks <= maxPacks){
            int index = Random.Range(0, locations.Length);
            Instantiate(ammoPack, locations[index].transform.position, Quaternion.identity);
            currentPacks++;
        }
    }

    public void removePack()
    {
        currentPacks--;
    }
}
