using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    int ammo = 100;
    Animator anim;
    // Start is called before the first frame update

    private void Start() {
        anim = GetComponent<Animator>();
        anim.SetInteger("Ammo", ammo);
    }
    public void decreaseAmmo(){
        ammo -= 40;
        print(ammo.ToString());
        anim.SetInteger("Ammo", ammo);
        if(ammo <=0) ammo = 0;
    }

    public void setAmmo(int newAmmo){
        ammo = newAmmo;
    }

    public int getAmmo(){
        return ammo;
    }
}
