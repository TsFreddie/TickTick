using UnityEngine;
using System.Collections;
using TickTick;

/// <summary>
/// 刻石UI管理组件
/// </summary>
public class CarvedDisplay : MonoBehaviour {
    public UnityEngine.UI.Text _powerText;
    public UnityEngine.UI.Text _healthText;
    public UnityEngine.UI.Text _agilityText;
    public UnityEngine.UI.Text _lossText;
    public UnityEngine.UI.Image _progressBar;
    
    public void SetPower(int power)
    {
        _powerText.text = power == -1 ? "" : power.ToString();
    }

    public void SetHealth(int health)
    {
        _healthText.text = health == -1 ? "" : health.ToString();
    }

    public void SetAgility(int agility)
    {
        _agilityText.text = agility == -1 ? "" : agility.ToString();
    }
     
    public void SetLoss(int loss)
    {
        _lossText.text = loss == -1 ? "" : loss.ToString();
    }
    /// <summary>设置读条进度</summary>
    /// <param name="stage">阶段</param>
    /// <param name="progress">进度, 0 - 1</param>
    public void SetProgress(CarvedObject.Stage stage, float progress)
    {
        if (stage == CarvedObject.Stage.Preparing)
        {
            _progressBar.color = new Color(0,1,0,0.8f);
            _progressBar.fillAmount = progress;
        }
        if (stage == CarvedObject.Stage.Ready)
        {
            _progressBar.color = new Color(0,0,1,0.8f);
            _progressBar.fillAmount = 1f;
        }
        if (stage == CarvedObject.Stage.Processing)
        {
            _progressBar.color = new Color(1,0,0,0.8f);
            _progressBar.fillAmount = 1 - progress;
        }
        if (stage == CarvedObject.Stage.Recovering)
        {
            _progressBar.color = new Color(1,1,0,0.8f);
            _progressBar.fillAmount = progress;
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
    