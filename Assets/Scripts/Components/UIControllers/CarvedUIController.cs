using UnityEngine;
using System.Collections;

/// <summary>
/// 刻石UI管理组件
/// </summary>
public class CarvedUIController : MonoBehaviour {

    public UnityEngine.UI.Text PowerText;
    public UnityEngine.UI.Text HealthText;
    public UnityEngine.UI.Text AgilityText;
    public UnityEngine.UI.Text HarmText;

    public void SetPower(int power) { PowerText.text = power.ToString(); }
    public void SetHealth(int health) { HealthText.text = health.ToString(); }
    public void SetAgility(int agility) { AgilityText.text = agility.ToString(); }
    public void SetHarm(int harm) { HarmText.text = harm.ToString(); }

}
