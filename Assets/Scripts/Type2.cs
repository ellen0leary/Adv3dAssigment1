using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Type2 : MonoBehaviour
{
    public GameObject target;
    GameObject[] WPs;
    int WPIndex;
    string thisTag;
    Animator anim;
    AnimatorStateInfo info;
    public bool ifSet = false;
    GameObject player;
    int numWPReached = 0;
    float patrolTimer = 0;
    float shootTimer = 0;
    float shootingTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject WP1 = GameObject.Find("WP1");
        GameObject WP2 = GameObject.Find("WP2");
        GameObject WP3 = GameObject.Find("WP3");
        GameObject WP4 = GameObject.Find("WP4");
        WPs = new GameObject[] { WP1, WP2, WP3, WP4 };
        WPIndex = 0;
        thisTag = this.gameObject.tag;
        anim = GetComponent<Animator>();
        target = WPs[WPIndex];
        player = GameObject.Find("player");
    }

    void MoveToRandomWP()
    {
        int previous = WPIndex;
        int random = 0;
        do
        {
            random = Random.Range(0, WPs.Length);
        } while (random == previous);
        WPIndex = random;
    }

    // Update is called once per frame
    void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Dies"))
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            Destroy(gameObject, 3);
            return;
        }
        int ammos = GetComponent<AmmoController>().getAmmo();
        anim.SetInteger("Ammo", ammos);
        smell();
        look();
        listen();

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 1.5)
        {
            print("game over");
            return;
        }
        if (info.IsName("Idle"))
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer > 1)
            {
                patrolTimer = 0;
                anim.SetBool("IsPatroling", true);
            }
        }
        if (info.IsName("Patrol"))
        {
            if ((Vector3.Distance(transform.position, target.transform.position) < 2.0f) && !ifSet)
            {
                MoveToRandomWP();
                target = WPs[WPIndex];
                numWPReached++;
                if (numWPReached >= 4)
                {
                    numWPReached = 0;
                    anim.SetTrigger("startBackToStart");
                }
            }

            GetComponent<NavMeshAgent>().SetDestination(WPs[WPIndex].transform.position);
        }
        if (info.IsName("Shoots"))
        {
            shootingTimer += Time.deltaTime;
            // GetComponent<NavMeshAgent>().isStopped = true;
            print("shooting now");
            GetComponent<AmmoController>().decreaseAmmo();

        }
        if (info.IsName("FollowPlayer"))
        {
            shootTimer += Time.deltaTime;

            if (shootTimer > 3)
            {
                if (/*anim.GetBool("canSeePlayer") &&*/ GetComponent<AmmoController>().getAmmo() > 0)
                {
                    anim.SetTrigger("startToShoot");
                    print("shooting");
                    shootTimer = 0;
                }
            }
            target = player;
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        }
        if (info.IsName("LookForHealth"))
        {
            target = LookForHealth();
            if(target == null){
                //no health packs
                if(Vector3.Distance(transform.position, player.transform.position)>5f){
                    Vector3 dir = transform.position - player.transform.position;
                    Vector3 newPos = transform.position+dir;
                    GetComponent<NavMeshAgent>().SetDestination(newPos);
                }
            }
            print("looking for health");
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) < 2)
            {
                GetComponent<NPCHealth>().setHealth(100);
                Destroy(target);
                GameObject.Find("WPs").GetComponent<HealthController>().removePack();
            }
        }

        if (info.IsName("LookForAmmo"))
        {
            target = LookForAmmo();
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


    void look()
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
            if (objInSight == "player") anim.SetBool("canSeePlayer", true);
            else anim.SetBool("canSeePlayer", false);
        }
    }
    void listen()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < 3f) anim.SetBool("canHearPlayer", true);
        else anim.SetBool("canHearPlayer", false);
    }

    void smell()
    {
        GameObject[] allBCs = GameObject.FindGameObjectsWithTag("BC");
        float minDistance = 2;
        bool dectectBC = false;
        for (int i = 0; i < allBCs.Length; i++)
        {
            if (Vector3.Distance(transform.position, allBCs[i].transform.position) < minDistance)
            {
                dectectBC = true;
                break;
            }
        }
        if (dectectBC) anim.SetBool("canSmellPlayer", true);
        else anim.SetBool("canSmell", false);
    }

    GameObject LookForAmmo(){
        GameObject[] gs = GameObject.FindGameObjectsWithTag("ammo");
        if(gs.Length==1){
            return gs[0];
        }else if(gs.Length>0){
            GameObject returnValue = new GameObject();
            float distance = Vector3.Distance(transform.position,gs[0].transform.position);
            foreach(GameObject j in gs){
                if(Vector3.Distance(transform.position, j.transform.position)< distance){
                    returnValue = j;
                    distance = Vector3.Distance(transform.position, j.transform.position);
                }
            }
            return returnValue;
        } else{
            return null;
        }
    }

    GameObject LookForHealth()
    {
        GameObject[] gs = GameObject.FindGameObjectsWithTag("health");
        if (gs.Length == 1)
        {
            return gs[0];
        }
        else if (gs.Length > 0)
        {
            GameObject returnValue = new GameObject();
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
