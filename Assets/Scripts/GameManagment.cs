using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagment : MonoBehaviour
{
    #region Singleton

    public static GameManagment instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Instance found");
            return;
        }
        instance = this;
    }
    #endregion

    public bool paused = true;
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        paused = true;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        paused = false;
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
