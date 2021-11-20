using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviourPunCallbacks
{
    public InputField nameInput;
    public Button playButton;
    public Text buttonText;

    public void ConnectToServer()
    {
        if(nameInput.text == ""){
            nameInput.image.color = new Color32(240,23,0,94);
        }
        else
        {
            PhotonNetwork.NickName = nameInput.text;
            playButton.image.color = new Color(91,87,250,98);
            buttonText.text = "Connecting...";
            playButton.interactable = false;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Lobby");
    }
}
