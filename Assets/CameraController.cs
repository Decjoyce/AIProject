using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    #region Singleton

    public static CameraController instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one Instance found");
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject dawg;
    public GameObject sheepy;

    public GameObject dogHolder;
    public GameObject sheepHolder;

    public CinemachineVirtualCamera tdCam;
    public CinemachineFreeLook sheepCam;
    public CinemachineFreeLook dogCam;

    public List<GameObject> sheep = new List<GameObject>();
    public List<GameObject> dogs = new List<GameObject>();

    int sheepNum;
    int dogNum;

    // Start is called before the first frame update
    void Start()
    {
        tdCam.Priority = 1;
        sheepCam.Priority = 0;
        dogCam.Priority = 0;
        sheepCam.LookAt = sheep[0].transform;
        sheepCam.Follow = sheep[0].transform;
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeCamera();
        }

        if(tdCam.Priority == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dogCam.Priority == 1)
                    ChangeDog();
                else if (sheepCam.Priority == 1)
                    ChangeSheep();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
                Instantiate(dawg, hit.point, new Quaternion(0, 0, 0, 0), dogHolder.transform);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
                Instantiate(sheepy, hit.point, new Quaternion(0, 0, 0, 0), sheepHolder.transform);
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
        sheepNum++;
        if (sheepNum >= sheep.Count)
            sheepNum = 0;
        sheepCam.LookAt = sheep[sheepNum].transform;
        sheepCam.Follow = sheep[sheepNum].transform;
    }    
    public void ChangeDog()
    {
        dogNum++;
        if (dogNum >= dogs.Count)
            dogNum = 0;
        dogCam.LookAt = dogs[dogNum].transform;
        dogCam.Follow = dogs[dogNum].transform;
    }
    
}
