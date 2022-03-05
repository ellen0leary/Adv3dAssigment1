using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    public int getHealth()
    {
        return health;
    }

    public void changeHealth(int amount)
    {
        health += amount;
        print(health);
        if (health <= 0)
        {
            destroyNPC();
        }
    }

    void destroyNPC()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void hitByOpponet(GameObject g, int amount)
    {
        changeHealth(-amount);
        gameObject.GetComponent<TeamMember>().attack(g);
    }
}