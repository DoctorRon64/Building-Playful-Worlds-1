using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator CamAnim;


    public void OnStartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnOptionsClick()
    {
        CamAnim.SetBool("State", true);

    }

    public void OnOptionsBack()
    {
        CamAnim.SetBool("State", false);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
