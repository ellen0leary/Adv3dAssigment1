using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeamMember : MonoBehaviour
{
    GameObject leader;
    Animator anim;
    AnimatorStateInfo info;
    float distanceToLeader;
    public GameObject target;
    float distanceToTarget;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (gameObject.tag == "teamMember") leader = GameObject.Find("player");
        else leader = GameObject.Find("TeamLeader");
    }

    // Update is called once per frame
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);
        distanceToLeader = Vector3.Distance(leader.transform.position, transform.position);
        if (distanceToLeader < 3.0f)
        {
            print("close to leader");
            anim.SetBool("closeToLeader", true);
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
        else
        {
            anim.SetBool("closeToLeader", false);
            print("not close to leader");
        }

        if (info.IsName("idle"))
        {
            GetComponent<NavMeshAgent>().isStopped = true;
        }
        if (info.IsName("MoveTowardsLeader"))
        {
            GetComponent<NavMeshAgent>().SetDestination(leader.transform.position);
            GetComponent<NavMeshAgent>().isStopped = false;
        }
        if (info.IsName("GoToTarget"))
        {
            if (target != null)
            {
                GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
                GetComponent<NavMeshAgent>().isStopped = false;
                distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

                if (distanceToTarget < 1.5f)
                {
                    anim.SetBool("closeToTarget", true);
                }
                else
                {
                    anim.SetBool("closeToTarget", false);
                }
            }
            else anim.SetBool("targetDestroyed", true);
        }

        if (info.IsName("AttackTarget"))
        {
            if (target != null)
            {
                GetComponent<NavMeshAgent>().isStopped = true;
                gameObject.transform.LookAt(target.transform);
                if (info.normalizedTime % 1.0 >= .98)
                {
                    int damage;
                    if (gameObject.tag == "team2") damage = 10;
                    else damage = 20;
                    target.GetComponent<NPC>().hitByOpponet(gameObject, damage);
                    if (target != null) target.GetComponent<NPC>().changeHealth(-20);
                    else anim.SetBool("targetDestroyed", true);
                }
            }
            else anim.SetBool("targetDestroyed", true);
        }
    }

    public void attack(GameObject t)
    {
        target = t;
        anim.SetTrigger("respondToAttack");
    }

    public void retreat()
    {
        anim.SetTrigger("retreat");
    }
}
