using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grande : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion;
    float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 3f)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
