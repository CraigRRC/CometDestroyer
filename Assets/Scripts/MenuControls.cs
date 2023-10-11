using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Awake()
    {
        if(UIManager.instance != null)
        {
            scoreText.text = UIManager.instance.score.ToString();
        }
        
    }
}
