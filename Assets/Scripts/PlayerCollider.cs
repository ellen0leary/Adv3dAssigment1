using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="health"){
            GetComponent<NPCHealth>().setHealth(100);
            Destroy(other.gameObject);
            GameObject.Find("HPs").GetComponent<HealthController>().removePack();
        } else if (other.gameObject.tag == "coll"){
            //get score controller
            //score up by one
        } else if(other.gameObject.tag== "ammo"){
            GetComponent<AmmoController>().setAmmo(100);
            Destroy(other.gameObject);
            GameObject.Find("APs").GetComponent<AmmoManager>().removePack();
        }
    }
}
