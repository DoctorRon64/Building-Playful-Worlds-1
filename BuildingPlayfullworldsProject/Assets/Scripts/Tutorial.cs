using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public string[] TutorialText;
    public TMP_Text text;
    public PickUps[] PickUpPoint;
    public GameObject Door;
    private int tutorialIndex;
    private float verticalInput;
    public Vector2 mousedir;

    void Start()
    {
        text.text = TutorialText[0];
        tutorialIndex = 0;
    }
    private void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        mousedir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        TutorialSwitch();
    }

    private void TutorialSwitch()
    {
        switch (tutorialIndex)
        {
            case 0: if (mousedir.magnitude != 0) { tutorialIndex++; text.text = TutorialText[1]; } break;
            case 1: if (verticalInput > 0 && PickUpPoint[0].Collided) { tutorialIndex++; Door.SetActive(false); text.text = TutorialText[2]; } break;
            case 2: if (PickUpPoint[1].Collided) { tutorialIndex++; text.text = TutorialText[3]; } break;
            case 3: if (Input.GetKey(KeyCode.LeftControl)) { tutorialIndex++; text.text = TutorialText[4]; } break;
            case 4: if (PickUpPoint[2].Collided) { tutorialIndex++; text.text = TutorialText[5]; } break;
            case 5: if (PickUpPoint[3].Collided) { tutorialIndex++; text.text = TutorialText[6]; } break;
            case 6: if (PickUpPoint[4].Collided) { tutorialIndex++; text.text = TutorialText[7]; } break;
            case 7: if (PickUpPoint[5].Collided) { tutorialIndex++; text.text = TutorialText[8]; } break;
        }
    }
}