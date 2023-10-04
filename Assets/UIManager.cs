using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image shieldBar;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player.OnShieldUse += OnShieldUse;
        player.OnShieldColour += ChangeShieldColor;
        shieldBar.color = Color.green; 

    }

   
    void Update()
    {
        
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

    private void OnDisable()
    {
        player.OnShieldUse -= OnShieldUse;
        player.OnShieldColour -= ChangeShieldColor;
    }
}
