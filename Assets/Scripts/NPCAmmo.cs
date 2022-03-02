using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAmmo : MonoBehaviour
{
    Animator anim;
    int ammo =100;
    // Start is called before the first frame update
    // Start is called before the first frame update

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("Ammo", ammo);
    }

    public void decreaseAmmo(){
        ammo-=40;
        anim.SetInteger("Ammo", ammo);
        if(ammo <= 0) ammo = 0;
        print(ammo);
    }

    public void setAmmo(int newAmmo){
        ammo = newAmmo;
    }

    public int getAmmo(){
        return ammo;
    }
}
