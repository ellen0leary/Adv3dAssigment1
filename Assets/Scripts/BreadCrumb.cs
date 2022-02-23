using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadCrumb : MonoBehaviour
{
    Vector3 prevPos; 
    float counter =0;
    public GameObject BC;
    public float distance;

    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = transform.position;
        distance = Vector3.Distance(prevPos, currentPosition);
        if (distance > 1.5f)
        {
            GameObject g = Instantiate(BC, prevPos, transform.rotation);
            prevPos = currentPosition;
            g.name = "BC" + counter;
            counter++;
        }
    }
}
