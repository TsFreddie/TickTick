using UnityEngine;
using System.Collections;

/// <summary>
/// 刻石UI管理组件
/// </summary>
public class CarvedDisplay : MonoBehaviour {

    public UnityEngine.UI.Text PowerText;
    public UnityEngine.UI.Text HealthText;
    public UnityEngine.UI.Text AgilityText;
    public UnityEngine.UI.Text LossText;

    public void SetPower(int power)
    {
        PowerText.text = power == -1 ? "" : power.ToString();
    }

    public void SetHealth(int health)
    {
        HealthText.text = health == -1 ? "" : health.ToString();
    }

    public void SetAgility(int agility)
    {
        AgilityText.text = agility == -1 ? "" : agility.ToString();
    }

    public void SetLoss(int loss)
    {
        LossText.text = loss == -1 ? "" : loss.ToString();
    }

    public void SetReady(bool ready)
    {
        //Highligher.ConstantOn(ReadyColor);
    }

    public void SetHighlight(bool highlighted)
    {
        //Highligher.On(ActiveColor);
    }
}
    