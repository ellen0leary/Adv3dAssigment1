using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Type3 : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent nav;
    Animator anim;
    GameObject player;
    float shootingTimer;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("player");
        anim = GetComponent<Animator>();
        shootingTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(player.transform.position);
        anim.SetBool("isPatroling", true);
        bool ifSees = look();
        if(ifSees && shootingTimer> 10f){
            print("shooting");
            GetComponent<AmmoController>().decreaseAmmo();
            anim.SetTrigger("Shoot");
            shootingTimer =0f;
            // GetComponent<AmmoController>().decreaseAmmo();
        } 
        shootingTimer += Time.deltaTime;
    }


    bool look()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = transform.position + Vector3.up * 0.7f;
        string objInSight = "";
        float castingDistance = 20;
        ray.direction = transform.forward * castingDistance;
        Debug.DrawRay(ray.origin, ray.direction * castingDistance, Color.red);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance))
        {
            objInSight = hit.collider.gameObject.name;
            if (objInSight == "player") return true;
            else return false;
        }
        return false;
    }
}
