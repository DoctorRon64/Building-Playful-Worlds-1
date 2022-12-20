using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Start()
    {
        text.text = TutorialText[0];
        tutorialIndex = 0;
    }

	private void Update()
	{
        verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 mousedir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (mousedir.magnitude != 0 && tutorialIndex == 0)
		{
            tutorialIndex++;
            text.text = TutorialText[1];
        }
        
        if (verticalInput > 0 && tutorialIndex == 1 && PickUpPoint[0].Collided)
		{
            tutorialIndex++;
            Door.SetActive(false);
            text.text = TutorialText[2];
		}

        if (tutorialIndex == 2 && PickUpPoint[1].Collided)
		{
            tutorialIndex++;
            text.text = TutorialText[3];
		}

        if (Input.GetKey(KeyCode.LeftControl) && tutorialIndex == 3)
		{
            tutorialIndex++;
            text.text = TutorialText[4];
		}

        if (tutorialIndex == 4 && PickUpPoint[2].Collided)
        {
            tutorialIndex++;
            text.text = TutorialText[5];
        }

		if (tutorialIndex == 5 && PickUpPoint[3].Collided)
		{
			tutorialIndex++;
			text.text = TutorialText[6];
		}

        if (tutorialIndex == 6 && PickUpPoint[4].Collided)
        {
            tutorialIndex++;
            text.text = TutorialText[7];
        }

        if (tutorialIndex == 7 && PickUpPoint[5].Collided)
        {
            tutorialIndex++;
            text.text = TutorialText[8];
        }
    }



}
