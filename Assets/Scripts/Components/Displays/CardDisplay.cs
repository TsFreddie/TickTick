using UnityEngine;
using System.Collections;

/// <summary>
/// 卡牌UI管理组件
/// </summary>
public class CardDisplay : MonoBehaviour {

    public UnityEngine.UI.Text PowerText;
    public UnityEngine.UI.Text HealthText;
    public UnityEngine.UI.Text AgilityText;
    public UnityEngine.UI.Text LossText;
    public UnityEngine.UI.Text CostText;
    public UnityEngine.UI.Text BoosterText;
    public UnityEngine.UI.Text TypeText;

    void Awake()
    {
        PowerText.text = "";
        HealthText.text = "";
        AgilityText.text = "";
        LossText.text = "";
        CostText.text = "";
        BoosterText.text = "";
        TypeText.text = "";
    }

    void Update()
    {

    }

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

    public void SetCost(int cost)
    {
        CostText.text = cost.ToString();
    }

    public void SetBooster(int booster)
    {
        BoosterText.text = booster.ToString();
    }

    public void SetType(CardData.CardType type)
    {
        switch (type)
        {
            case CardData.CardType.Magic:
                TypeText.text = "魔";
                break;
            case CardData.CardType.Melee:
                TypeText.text = "战";
                break;
            case CardData.CardType.Range:
                TypeText.text = "远";
                break;
            case CardData.CardType.Summon:
                TypeText.text = "召";
                break;
            case CardData.CardType.Wizard:
                TypeText.text = "法";
                break;
        }
    }
}
