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
    public UnityEngine.UI.Image ProgressBar;
    
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
    /// <summary>设置读条进度</summary>
    /// <param name="progress">进度, 0 - 1</param>
    public void SetProgress(CarvedObject.Stage stage, float progress)
    {
        if (stage == CarvedObject.Stage.Preparing)
        {
            ProgressBar.color = new Color(0,1,0,0.8f);
            ProgressBar.fillAmount = progress;
        }
        if (stage == CarvedObject.Stage.Ready)
        {
            ProgressBar.color = new Color(0,0,1,0.8f);
            ProgressBar.fillAmount = 1f;
        }
        if (stage == CarvedObject.Stage.Processing)
        {
            ProgressBar.color = new Color(1,0,0,0.8f);
            ProgressBar.fillAmount = 1 - progress;
        }
        if (stage == CarvedObject.Stage.Recovering)
        {
            ProgressBar.color = new Color(1,1,0,0.8f);
            ProgressBar.fillAmount = progress;
        } 
        
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
    