using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image shieldBar;
    public Image[] lives;
    public GameObject gravityText;
    public Player player;
    public Spawner spawner;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;


    public int score;

    public static UIManager instance;

    // Start is called before the first frame update
    void Start()
    {
        player.OnShieldUse += OnShieldUse;
        player.OnShieldColour += ChangeShieldColor;
        spawner.OnPreLevelSwitch += TurnTextOn;
        spawner.OnLevelSwitch += TurnTextOff;
        spawner.OnLevelSwitch += UpdateLevelText;
        spawner.CometReference += OnCometReference;
        shieldBar.color = Color.green;
        gravityText.SetActive(false);
        score = 0;
    }

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnCometReference(Comet comet)
    {
        if(comet != null)
        {
            comet.CameraShake += OnCameraShake;
            comet.LeftCometReference += OnLeftCometReference;
            comet.RightCometReference += OnRightCometReference;
        }
       
    }

    private void OnCameraShake()
    {
        score += 20;
        scoreText.text = score.ToString();
    }

    private void OnRightCometReference(MiniComet comet)
    {
        if(comet != null)
        {
            comet.miniCometCameraShake += OnRightMiniCometCameraShake;
        }
       
        
    }

    private void OnRightMiniCometCameraShake()
    {
        score += 20;
        scoreText.text = score.ToString();
    }

    private void OnLeftCometReference(MiniComet comet)
    {
        if(comet != null)
        {
            comet.miniCometCameraShake += OnLeftMiniCometCameraShake;
        }
        
    }

    private void OnLeftMiniCometCameraShake()
    {
        score += 20;
        scoreText.text = score.ToString();
    }

    private void UpdateLevelText(int level)
    {
        if(level == 5)
        {
            levelText.text = "You win!";
        }
        else
        {
            level = level + 1;
            levelText.text = "Level " + level.ToString();
           
        }
    }

    private void TurnTextOff(int level)
    {
        gravityText.SetActive(false);
    }

    private void TurnTextOn()
    {
        gravityText.SetActive(true);
        int scoreMultiplyer = 1;
        //How many lives are left?
        foreach (var life in lives)
        {
            if (life.fillCenter)
            {
                scoreMultiplyer++;
            }
        }
        score *= scoreMultiplyer;
        scoreText.text = score.ToString();

        //Multiply score by lives left.
    }

    public void OnShieldUse(float shieldAmount)
    {
        if(player != null)
        {
            //Convert to a percent
            float shieldAsPercent = shieldAmount / player.GetPlayerShieldMax() * 100;
            //Apply to the actual bar
            shieldBar.fillAmount = shieldAsPercent / 100;
        }
        
    }

    public void ChangeShieldColor(bool canShield)
    {
        shieldBar.color = canShield ? Color.blue : Color.red;
    }

    public void LoseLife(Vector2 playerPos)
    {
        bool playerAlive = lives[lives.Length - 1].fillCenter;
        //Last life will get bonked if second life is bonked.
        //lives[2].gameObject.SetActive(lives[1].gameObject.activeSelf);
        lives[2].fillCenter = lives[1].fillCenter;
        //Second life will get bonked if first life is already bonked
        //lives[1].gameObject.SetActive(lives[0].gameObject.activeSelf);
        lives[1].fillCenter = lives[0].fillCenter ;
        //First life will always get bonked.
        //lives[0].gameObject.SetActive(false);
        lives[0].fillCenter = false;

        switch (playerAlive)
        {
            case true:
                break;

            case false:
                SceneManager.LoadScene("GameOver");
                break;
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.OnShieldUse -= OnShieldUse;
            player.OnShieldColour -= ChangeShieldColor;
        }
       if(spawner != null)
        {
            spawner.OnPreLevelSwitch -= TurnTextOn;
            spawner.OnLevelSwitch -= TurnTextOff;
            spawner.OnLevelSwitch -= UpdateLevelText;
            spawner.CometReference -= OnCometReference;
        }
        
    }
}
