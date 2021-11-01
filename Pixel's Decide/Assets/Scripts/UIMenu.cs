using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    public InputField nameInput;

    public void StartGame()
    {
        if(nameInput.text == ""){
            nameInput.image.color = Color.red;
        }
        else
        {
            PlayerPrefs.SetString("PlayerName",nameInput.text);
            SceneManager.LoadScene("LoadingScreenServer");
        }
    }
}
