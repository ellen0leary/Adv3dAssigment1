using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Type4 : MonoBehaviour
{
    public GameObject target;
    GameObject player;
    Animator anim;
    public Vector3 newPos;
    AnimatorStateInfo info;
    GameObject ambushPoint;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("player");
        ambushPoint = GameObject.Find("AmbushPoint");
    }

    // Update is called once per frame
    void Update()
    {
        // float minimuDistanceBetweenThem = 2.5f;
        // Vector3 dir = (-transform.position + player.transform.position).normalized;
        // newPos = transform.position - minimuDistanceBetweenThem * dir;
        // GetComponent<NavMeshAgent>().SetDestination(newPos);
        info = anim.GetCurrentAnimatorStateInfo(0);
        if(info.IsName("Patrol")){
            float minimuDistanceBetweenThem = 2.5f;
            Vector3 dir = (-transform.position + player.transform.position).normalized;
            newPos = transform.position -minimuDistanceBetweenThem*  dir;
            GetComponent<NavMeshAgent>().SetDestination(newPos);
        }
        if(info.IsName("GoToAmbush")){
            //set destination
            target = ambushPoint;
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
            if(Vector3.Distance(transform.position, target.transform.position)<3f){
                anim.SetTrigger("ThrowGrande");
            }
        }
        if (info.IsName("LookForHealth"))
        {
            target = LookForTag("health");
            if (target == null)
            {
                //no health packs
                if (GetComponent<AmmoController>().getAmmo() > 80)
                {
                    anim.SetBool("ifHealthAbv", false);
                }
            }
            print("looking for health");
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) < 2)
            {
                GetComponent<NPCHealth>().setHealth(100);
            }
        }

    }


    public void playerEnteredAmbushArea(){
        anim.SetTrigger("goToAmbush");
    }

    GameObject LookForTag(string tag)
    {
        GameObject[] gs = GameObject.FindGameObjectsWithTag(tag);
        if (gs.Length == 1)
        {
            return gs[0];
        }
        else if (gs.Length > 0)
        {
            GameObject returnValue = gs[0];
            float distance = Vector3.Distance(transform.position, gs[0].transform.position);
            foreach (GameObject j in gs)
            {
                if (Vector3.Distance(transform.position, j.transform.position) < distance)
                {
                    returnValue = j;
                    distance = Vector3.Distance(transform.position, j.transform.position);
                }
            }
            return returnValue;
        }
        else
        {
            return null;
        }
    }
}
