using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] NPCs;
    public GameObject[] SPs;
    float maxTimer= 20f;
    float timer = 0;
    float multiplyer= 1;
    // Start is called before the first frame update
    void Start()
    {
        SPs = new GameObject[] {
            GameObject.Find("SP1"),
            GameObject.Find("SP2"),
            GameObject.Find("SP3"),
            GameObject.Find("SP4"),
            GameObject.Find("SP5"),
            GameObject.Find("SP6")
        };
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime* multiplyer;
        if(timer>maxTimer){
            int i = Random.Range(0,NPCs.Length);
            int j = Random.Range(0, SPs.Length);

            Instantiate(NPCs[i],SPs[j].transform.position,Quaternion.identity);
            multiplyer *= 1.5f;
            timer = 0;
            
        }
    }
}
