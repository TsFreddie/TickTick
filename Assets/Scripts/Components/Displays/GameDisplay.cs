using UnityEngine;
using System.Collections;

public class GameDisplay : MonoBehaviour {
	public UnityEngine.UI.Text Gold;
	public UnityEngine.UI.Text HostileGold;
	public UnityEngine.UI.Text Booster;
	public UnityEngine.UI.Text HostileBooster;
	public UnityEngine.UI.Text Day;
	public UnityEngine.UI.Text Time;
	public UnityEngine.UI.Text Mining;
	
	public void UpdateDisplay(Rule rule)
	{
		if (rule.GetType() == typeof(DuelRule))
			UpdateDuelRuleDisplay((DuelRule)rule);
	}
	
	public void UpdateDuelRuleDisplay(DuelRule rule)
	{
		Gold.text = rule.Gold.ToString();
		HostileGold.text = rule.HostileGold.ToString();
		Booster.text = rule.Booster.ToString();
		HostileBooster.text = rule.HostileBooster.ToString();
		Time.text = (rule.Hour < 12) ? (rule.Hour == 0 ? "12" : rule.Hour.ToString()) + " AM" : ((rule.Hour - 12) == 0 ? "12" : (rule.Hour - 12).ToString()) + " PM";
		Day.text = (rule.Day == 0) ? "Eve" : "Day " + rule.Day.ToString();
		Mining.text = rule.Mining.ToString("F3");
	}
}
