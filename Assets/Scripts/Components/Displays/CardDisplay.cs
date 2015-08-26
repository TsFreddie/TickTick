using UnityEngine;
using System.Collections;

/// <summary>
/// 卡牌UI管理组件
/// </summary>
public class CardDisplay : MonoBehaviour {

    public UnityEngine.UI.Text _powerText;
    public UnityEngine.UI.Text _healthText;
    public UnityEngine.UI.Text _agilityText;
    public UnityEngine.UI.Text _lossText;
    public UnityEngine.UI.Text _costText;
    public UnityEngine.UI.Text _boosterText;
    public UnityEngine.UI.Text _typeText;

    void Awake()
    {
        _powerText.text = "";
        _healthText.text = "";
        _agilityText.text = "";
        _lossText.text = "";
        _costText.text = "";
        _boosterText.text = "";
        _typeText.text = "";
    }

    void Update()
    {

    }

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

    public void SetCost(int cost)
    {
        _costText.text = cost.ToString();
    }

    public void SetBooster(int booster)
    {
        _boosterText.text = booster.ToString();
    }

    public void SetType(CardData.CardType type)
    {
        switch (type)
        {
            case CardData.CardType.Magic:
                _typeText.text = "魔";
                break;
            case CardData.CardType.Melee:
                _typeText.text = "战";
                break;
            case CardData.CardType.Range:
                _typeText.text = "远";
                break;
            case CardData.CardType.Summon:
                _typeText.text = "召";
                break;
            case CardData.CardType.Wizard:
                _typeText.text = "法";
                break;
        }
    }
}
