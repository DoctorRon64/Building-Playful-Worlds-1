using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public GameObject[] Door;
    public PickUps[] PickUpPoint;

    void Update()
    {
        if (PickUpPoint[0].Collided && PickUpPoint[1].Collided)
		{
            Door[0].SetActive(false);
		}

        if (PickUpPoint[2].Collided)
		{
            Door[1].SetActive(false);
		}
    }
}
