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
        // if(info.IsName("goToAmbush")){
        //     //set destination
        //     target = ambushPoint;
        //     GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        //     if(Vector3.Distance(transform.position, target.transform.position)>3f){
        //         anim.SetTrigger("ThrowGrande");
        //     }

        // }
        // if(info.IsName("ThrowGrande")){

        // }

    }


    public void playerEnteredAmbushArea(){
        anim.SetTrigger("goToAmbush");
    }
}
