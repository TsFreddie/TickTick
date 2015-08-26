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
	public ParticleSystem particle;
	
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

    /// <summary>
    /// 显示连接
    /// </summary>
    /// <param name="origin">起点</param>
    /// <param name="endpoint">重点</param>
	public void ShowConnection(Vector3 origin, Vector3 endpoint)
	{
        // TODO: 改用其他方式
        particle.transform.position = new Vector3(origin.x, 0.8f, origin.z);
		particle.transform.LookAt(new Vector3(endpoint.x, 0.8f, endpoint.z));
        particle.startRotation = Mathf.Atan2(origin.z - endpoint.z, endpoint.x - origin.x);
        particle.startLifetime = (new Vector3(endpoint.x, 0, endpoint.z) - new Vector3(origin.x, 0, origin.z)).magnitude / particle.startSpeed;
        particle.Simulate(0);
    }

    /// <summary>
    /// 隐藏连接
    /// </summary>
	public void HideConnection()
	{
        particle.Clear();
    }
}
