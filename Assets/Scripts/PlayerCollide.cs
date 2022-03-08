using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollide : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name == "player"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
