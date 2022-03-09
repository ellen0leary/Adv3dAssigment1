using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreController : MonoBehaviour
{
    float timer=10f;
    int score=0;
    GameObject timerOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().SetText("Score " + score.ToString());
        GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>().SetText("Time : " + ((int)timer).ToString());
        timerOverPanel = GameObject.Find("TimerOverPanel");
        timerOverPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //set timer on cavas
        GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>().SetText("Time : " + ((int)timer).ToString());

        if(score>=10){
            //end game
            if(SceneManager.GetActiveScene().name=="Level1"){
                SceneManager.LoadScene("Level2");
            }else {
                //get win panel
            }
            print("game won");
        }else if(timer<0){
            //get end panel
            print("out of time");
            Time.timeScale =0;
            timerOverPanel.SetActive(true);
        }
    }


    public void updateScore(){
        score++;
        print(score);
        GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>().SetText("Score " + score.ToString());
        //add to canvas
    }
}
