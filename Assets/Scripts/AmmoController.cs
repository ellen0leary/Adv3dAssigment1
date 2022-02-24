using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    int ammo = 100;
    // Start is called before the first frame update
    public void decreaseAmmo(){
        ammo -= 40;
        print(ammo.ToString());
        if(ammo <=0) ammo = 0;
    }

    public void setAmmo(int newAmmo){
        ammo = newAmmo;
    }

    public int getAmmo(){
        return ammo;
    }
}
