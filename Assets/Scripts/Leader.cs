using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Leader : MonoBehaviour
{
    public Vector3 target;
    GameObject[] teamMembers;
    GameObject[] allTargets;
    int nbTeamMembers, nbTargets;
    Animator anim;
    AnimatorStateInfo info;
    float distanceToTarget;
    float patrolTimer;
    int WPIndex; 
    GameObject[] WPs;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        WPIndex = 0;
        WPs = new GameObject[] {GameObject.Find("WP1"),GameObject.Find("WP2") ,GameObject.Find("WP3") ,GameObject.Find("WP4")};
        anim = GetComponent<Animator>();
        if (gameObject.name == "player") teamMembers = GameObject.FindGameObjectsWithTag("teamMember");
        else teamMembers = GameObject.FindGameObjectsWithTag("team2");
        nbTeamMembers = teamMembers.Length;
        // allTargets = GameObject.FindGameObjectsWithTag("target");
        // nbTargets = allTargets.Length;
        player = GameObject.Find("player");
    }

    public void attack()
    {
        if (gameObject.name == "TeamLeader") allTargets = GameObject.FindGameObjectsWithTag("teamMember");
        else allTargets = GameObject.FindGameObjectsWithTag("team2");
        print("gotten " + allTargets.Length.ToString()+ " to targets");
        nbTargets = allTargets.Length;
        for (int i = 0; i < nbTargets; i++)
        {
            print("attacking " + i.ToString() + " Index");
            teamMembers[i].GetComponent<TeamMember>().attack(allTargets[i]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "player")
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                print("attacking");
                attack();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                print("retreating");
                retreat();
            }
        }
        else
        {
            patrolTimer += Time.deltaTime;
            info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.IsName("Idle"))
            {
                if (patrolTimer >= 5)
                {
                    patrolTimer = 0.0f;
                    anim.SetTrigger("startPatrol");
                }
            }
            if (info.IsName("Patrol"))
            {
                detectEmemies();
                if (Vector3.Distance(transform.position, WPs[WPIndex].transform.position) < 1.0f)
                {
                    WPIndex++;
                    if (WPIndex > 3) WPIndex = 0;
                }
                target = WPs[WPIndex].transform.position;
                GetComponent<NavMeshAgent>().SetDestination(WPs[WPIndex].transform.position);
                GetComponent<NavMeshAgent>().isStopped = false;
            }
        }
    }

    void retreat()
    {
        for (int i = 0; i < nbTeamMembers; i++)
        {
            teamMembers[i].GetComponent<TeamMember>().retreat();
        }
    }

    void detectEmemies()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <10f){ 
            anim.SetTrigger("closeToEmeny");
        }
    }
}
