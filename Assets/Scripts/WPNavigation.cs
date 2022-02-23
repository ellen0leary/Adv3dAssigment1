using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WPNavigation : MonoBehaviour
{
    public GameObject target;
    GameObject[] WPs;
    int WPIndex;
    string thisTag;
    Animator anim;
    AnimatorStateInfo info;
    public bool ifSet = false;
    GameObject player;
    int numWPReached =0;
    float patrolTimer =0;
    float shootTimer = 0;
    float shootingTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject WP1 = GameObject.Find("WP1");
        GameObject WP2 = GameObject.Find("WP2");
        GameObject WP3 = GameObject.Find("WP3");
        GameObject WP4 = GameObject.Find("WP4");
        WPs = new GameObject[] { WP1, WP2, WP3,WP4};
        WPIndex = 0;
        thisTag =this.gameObject.tag;
        anim = GetComponent<Animator>();
        target = WPs[WPIndex];
        player = GameObject.Find("player");
    }

    void MoveToNextWP()
     {
         WPIndex++;
         if (WPIndex > WPs.Length - 1) WPIndex = 0;
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
        smell();
        look();
        listen();

        if (info.IsName("Idle"))
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer > 1)
            {
                patrolTimer = 0;
                anim.SetBool("IsPatroling",true);
            }
        }
        if(info.IsName("Patrol")){
            if ((Vector3.Distance(transform.position, target.transform.position) < 2.0f) && ifSet)
            {
            MoveToNextWP();
            target = WPs[WPIndex];
            numWPReached++;
            if (numWPReached >= 4)
            {
                numWPReached = 0;
                anim.SetTrigger("startBackToStart");
            }
            }
            else if( (Vector3.Distance(transform.position, target.transform.position) < 2.0f) && !ifSet )
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
            shootingTimer +=Time.deltaTime;
            GetComponent<NavMeshAgent>().isStopped = true;
            if(shootingTimer > 1){
            anim.SetBool("startToShoot",false);
            GetComponent<NavMeshAgent>().isStopped = true;
            }
        }
        if(info.IsName("FollowPlayer")){
            shootTimer += Time.deltaTime;
            
            if(shootTimer>3){
                if(anim.GetBool("canSeePlayer") && GetComponent<AmmoController>().getAmmo()>0) {
                    anim.SetBool("startToShoot",true);
                    print("shooting");
                    shootTimer =0;
                    GetComponent<AmmoController>().decreaseAmmo();
                }
            }
            target = player;
            GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        }
        
    }


    void look(){
        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = transform.position + Vector3.up * 0.7f;
        string objInSight = "";
        float castingDistance = 20;
        ray.direction = transform.forward * castingDistance;
        Debug.DrawRay(ray.origin, ray.direction * castingDistance, Color.red);
        if(Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance)){
            objInSight = hit.collider.gameObject.name;
            if(objInSight=="player") anim.SetBool("canSeePlayer", true);
            else anim.SetBool("canSeePlayer", false);
        }
    }
    void listen(){
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < 3f) anim.SetBool("canHearPlayer", true);
        else anim.SetBool("canHearPlayer", false);
    }

    void smell() {
        GameObject[] allBCs = GameObject.FindGameObjectsWithTag("BC");
        float minDistance = 2;
        bool dectectBC = false;
        for( int i=0; i<allBCs.Length; i++){
            if(Vector3.Distance(transform.position, allBCs[i].transform.position)<minDistance){
                dectectBC =true;
                break;
            }
        }
        if(dectectBC)anim.SetBool("canSmellPlayer", true);
        else anim.SetBool("canSmell", false);
    }
}
