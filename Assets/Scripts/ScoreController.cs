using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    float timer=180f;
    float score=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //set timer on cavas

        if(score>=10){
            //end game
            print("game won");
        }else if(timer<0){
            //end game
            print("out of time");
        }
    }


    public void updateScore(){
        score++;
        print(score);
        //add to canvas
    }
}
