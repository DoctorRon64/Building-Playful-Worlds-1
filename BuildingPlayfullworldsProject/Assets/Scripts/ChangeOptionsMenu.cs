using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOptionsMenu : MonoBehaviour
{
    public GameObject Mainmenu;
    public GameObject OptionsMenu;
    public int ChangeMenu;

    private void Update()
    {
        if (ChangeMenu == 0)
        {
            MainMenuOn();
        }

        if (ChangeMenu == 1)
        {
            OptionsMenuOn();
        }   
    }

    void OptionsMenuOn()
    {
        Mainmenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    void MainMenuOn()
    {
        Mainmenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }
}
