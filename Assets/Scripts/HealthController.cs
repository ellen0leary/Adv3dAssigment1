using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public GameObject healthPack;
    GameObject[] locations;
    int maxPacks = 3;
    int currentPacks;
    // Start is called before the first frame update
    void Start()
    {
        currentPacks = 0;
        locations = new GameObject[] { GameObject.Find("HP1"), GameObject.Find("HP2"), GameObject.Find("HP3"), GameObject.Find("HP4") };
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPacks <= maxPacks){
            int index = Random.Range(0, locations.Length);
            Instantiate(healthPack, locations[index].transform.position, Quaternion.identity);
            currentPacks++;
        }
    }

    public void removePack(){
        currentPacks--;
    }
}
