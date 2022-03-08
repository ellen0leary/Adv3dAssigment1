using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NPCHealth : MonoBehaviour
{
    Animator anim;
    float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        setHealth(health - Time.deltaTime * 0.10f);
        // anim.SetFloat("Health", health);
        // print(health);
        if (health < 0) {
            GetComponent<NavMeshAgent>().isStopped = true;
            Destroy(gameObject, 3);
        }
    }

    public float getHealth()
    {
        return health;
    }
    public void setHealth(float newHealth)
    {
        health = newHealth;
        // anim.SetFloat("Health", health);
    }
}
