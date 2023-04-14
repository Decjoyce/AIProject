using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera tdCam;
    public CinemachineFreeLook sheepCam;
    public CinemachineFreeLook dogCam;

    public GameObject[] sheep;
    public GameObject[] dogs;

    // Start is called before the first frame update
    void Start()
    {
        tdCam.Priority = 1;
        sheepCam.Priority = 0;
        dogCam.Priority = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            tdCam.Priority = 1;
            sheepCam.Priority = 0;
            dogCam.Priority = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            tdCam.Priority = 0;
            sheepCam.Priority = 1;
            dogCam.Priority = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            tdCam.Priority = 0;
            sheepCam.Priority = 0;
            dogCam.Priority = 1;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeCamera();
        }

        if(tdCam.Priority != 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dogCam.Priority == 1)
                    ChangeDog();

                if (sheepCam.Priority == 1)
                    ChangeSheep();
            }
        }


    }

    public void ChangeCamera()
    {
        if(tdCam.Priority == 1)
        {
            sheepCam.Priority = 1;
            tdCam.Priority = 0;
        }
        else if (sheepCam.Priority == 1)
        {
            dogCam.Priority = 1;
            sheepCam.Priority = 0;
        }
        else if (dogCam.Priority == 1)
        {
            tdCam.Priority = 1;
            dogCam.Priority = 0;
        }
    }

    public void ChangeSheep()
    {

    }    
    public void ChangeDog()
    {

    }

}
