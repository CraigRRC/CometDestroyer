using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image shieldBar;
    public Image[] lives;
    public GameObject gravityText;
    public Player player;
    public Spawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        player.OnShieldUse += OnShieldUse;
        player.OnShieldColour += ChangeShieldColor;
        spawner.OnPreLevelSwitch += TurnTextOn;
        spawner.OnLevelSwitch += TurnTextOff;
        shieldBar.color = Color.green;
        gravityText.SetActive(false);
    }

    private void TurnTextOff(int level)
    {
        gravityText.SetActive(false);
    }

    private void TurnTextOn()
    {
        gravityText.SetActive(true);
    }

    public void OnShieldUse(float shieldAmount)
    {
        //Convert to a percent
        float shieldAsPercent = shieldAmount / player.GetPlayerShieldMax() * 100;
        //Apply to the actual bar
        shieldBar.fillAmount = shieldAsPercent / 100;
    }

    public void ChangeShieldColor(bool canShield)
    {
        shieldBar.color = canShield ? Color.green : Color.red;
    }

    public void LoseLife(Vector2 playerPos)
    {
        bool playerAlive = lives[lives.Length - 1].gameObject.activeSelf;
        //Last life will get bonked if second life is bonked.
        lives[2].gameObject.SetActive(lives[1].gameObject.activeSelf);
        //Second life will get bonked if first life is already bonked
        lives[1].gameObject.SetActive(lives[0].gameObject.activeSelf);
        //First life will always get bonked.
        lives[0].gameObject.SetActive(false);

        switch (playerAlive)
        {
            case true:
                break;

            case false:
                Debug.Log("dead");
                break;
        }
    }

    private void OnDisable()
    {
        player.OnShieldUse -= OnShieldUse;
        player.OnShieldColour -= ChangeShieldColor;
        spawner.OnPreLevelSwitch -= TurnTextOn;
        spawner.OnLevelSwitch -= TurnTextOff;
    }
}
