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
    public bool ifSet = false;
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
            if ((Vector3.Distance(transform.position, target.transform.position) < 2.0f) && ifSet)
            {
            MoveToNextWP();
            target = WPs[WPIndex];
            }
            else if( (Vector3.Distance(transform.position, target.transform.position) < 2.0f) && !ifSet )
            {
            MoveToRandomWP();
            target = WPs[WPIndex];
          }
        GetComponent<NavMeshAgent>().SetDestination(WPs[WPIndex].transform.position);
        
    }
}
