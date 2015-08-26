using UnityEngine;
using System.Collections;

public class GameDisplay : MonoBehaviour {
	public UnityEngine.UI.Text _gold;
	public UnityEngine.UI.Text _hostileGold;
	public UnityEngine.UI.Text _booster;
	public UnityEngine.UI.Text _hostileBooster;
	public UnityEngine.UI.Text _day;
	public UnityEngine.UI.Text _time;
	public UnityEngine.UI.Text _mining;
	public ParticleSystem _particle;
	
	public void UpdateDisplay(Rule rule)
	{
		if (rule.GetType() == typeof(DuelRule))
			UpdateDuelRuleDisplay((DuelRule)rule);
	}
	
	public void UpdateDuelRuleDisplay(DuelRule rule)
	{
		_gold.text = rule.Gold.ToString();
		_hostileGold.text = rule.HostileGold.ToString();
		_booster.text = rule.Booster.ToString();
		_hostileBooster.text = rule.HostileBooster.ToString();
		_time.text = (rule.Hour < 12) ? (rule.Hour == 0 ? "12" : rule.Hour.ToString()) + " AM" : ((rule.Hour - 12) == 0 ? "12" : (rule.Hour - 12).ToString()) + " PM";
		_day.text = (rule.Day == 0) ? "Eve" : "Day " + rule.Day;
		_mining.text = rule.Mining.ToString("F3");
	}

    /// <summary>
    /// 显示连接
    /// </summary>
    /// <param name="origin">起点</param>
    /// <param name="endpoint">重点</param>
	public void ShowConnection(Vector3 origin, Vector3 endpoint)
	{
        // TODO: 改用其他方式
        _particle.transform.position = new Vector3(origin.x, 0.8f, origin.z);
		_particle.transform.LookAt(new Vector3(endpoint.x, 0.8f, endpoint.z));
        _particle.startRotation = Mathf.Atan2(origin.z - endpoint.z, endpoint.x - origin.x);
        _particle.startLifetime = (new Vector3(endpoint.x, 0, endpoint.z) - new Vector3(origin.x, 0, origin.z)).magnitude / _particle.startSpeed;
        _particle.Simulate(0);
    }

    /// <summary>
    /// 隐藏连接
    /// </summary>
	public void HideConnection()
	{
        _particle.Clear();
    }
}
