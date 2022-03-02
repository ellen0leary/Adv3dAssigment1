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
    AnimatorStateInfo info;
    float shootingTimer;
    GameObject target;
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
        if(ifSees && shootingTimer> 5f){
            print("shooting");
            GetComponent<AmmoController>().decreaseAmmo();
            anim.SetTrigger("Shoot");
            shootingTimer =0f;
            // GetComponent<AmmoController>().decreaseAmmo();
        } 
        shootingTimer += Time.deltaTime;


        info = anim.GetCurrentAnimatorStateInfo(0);
        if(info.IsName("LookForHealth")){
            target = LookForTag("health");
            if (target == null)
            {
                //no health packs
                if (Vector3.Distance(transform.position, player.transform.position) > 5f)
                {
                    Vector3 dir = transform.position - player.transform.position;
                    Vector3 newPos = transform.position + dir;
                    GetComponent<NavMeshAgent>().SetDestination(newPos);
                }
            }
            print("looking for health");
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) < 2)
            {
                GetComponent<NPCHealth>().setHealth(100);
            }
        }
        if (info.IsName("LookForAmmo"))
        {
            target = LookForTag("ammo");
            if (target == null)
            {
                return;
            }
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) < 2)
            {
                GetComponent<AmmoController>().setAmmo(100);
            }
        }
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


    GameObject LookForTag(string tag)
    {
        GameObject[] gs = GameObject.FindGameObjectsWithTag(tag);
        if (gs.Length == 1)
        {
            return gs[0];
        }
        else if (gs.Length > 0)
        {
            GameObject returnValue= gs[0];
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
