using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    GameObject startMenuPanel;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        startMenuPanel = GameObject.Find("StartMenu");
    }

    public void StartGame()
    {
        startMenuPanel.SetActive(false);
        gameManager.StartGame();
    }

}
