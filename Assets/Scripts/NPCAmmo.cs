using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAmmo : MonoBehaviour
{
    Animator anim;
    int ammo =100;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void decreaseAmmo(){
        ammo-=40;
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
