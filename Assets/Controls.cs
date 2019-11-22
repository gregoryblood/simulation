using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//r restarts scene
//space pauses/starts
public class Controls : MonoBehaviour
{
    private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        //Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
            SceneManager.LoadScene(0);
        if (Input.GetKeyDown("space") && !paused)
        {
            paused = !paused;
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown("space") && paused)
        {
            paused = !paused;
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown("["))
            Time.timeScale = Time.timeScale/2;
        else if (Input.GetKeyDown("]"))
            Time.timeScale = Time.timeScale * 2;
    }
}
