using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pause_sc : MonoBehaviour
{
    public GameObject pausemenu;
    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (flag)
            {
                Resume();
            }
            else
            {
                pausemenu.SetActive(true);
                Time.timeScale = 0f;
                flag = true;
            }
        }
    }

    public void Resume()
    {
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        flag = false;
    }

    public void Pause()
    {
        //restart in the same scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void mainmenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
