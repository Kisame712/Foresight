using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if(nextSceneIndex == SceneManager.sceneCount)
            {
                Debug.Log("Winner!");
            }
            else
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
    }
}
