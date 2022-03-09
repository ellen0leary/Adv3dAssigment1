using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AmmoController : MonoBehaviour
{
    int ammo = 100;
    Animator anim;
    // Start is called before the first frame update

    private void Start() {
        anim = GetComponent<Animator>();
        anim.SetInteger("Ammo", ammo);
        if (this.gameObject.name == "player")
        {
            GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>().SetText("Ammo " + ammo.ToString());
        }
    }
    public void decreaseAmmo(){
        ammo -= 40;
        print(ammo.ToString());
        anim.SetInteger("Ammo", ammo);
        if (this.gameObject.name == "player")
        {
            GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>().SetText("Ammo " + ammo.ToString());
        }
        if(ammo <=0) ammo = 0;
    }

    public void setAmmo(int newAmmo){
        ammo = newAmmo;
        if(this.gameObject.name=="player"){
            GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>().SetText("Ammo "+ ammo.ToString());
        }
    }

    public void decreaseAmmo(int value)
    {
        ammo -= value;
        print(ammo.ToString());
        anim.SetInteger("Ammo", ammo);
        if (this.gameObject.name == "player")
        {
            GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>().SetText("Ammo " + ammo.ToString());
        }
        if (ammo <= 0) ammo = 0;
    }
    public int getAmmo(){
        return ammo;
    }
}
