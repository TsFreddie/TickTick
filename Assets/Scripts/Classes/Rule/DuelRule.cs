using UnityEngine;
using System.Collections;

public class DuelRule : Rule
{
	public int Gold { get; private set; }	
	public int Booster { get; private set; }
	public int HostileGold { get; private set; }
	public int HostileBooster { get; private set; }
	public double Time { get; private set; }
	public float Mining { get; private set; }
	/// <summary>当前时间的小时数</summary>
	public int Hour
	{
		get
		{
			int startedHours = (int)(24 * (Time - DayScale * (int)(Time / DayScale)) / DayScale);
			return (startedHours + 18) % 24;
		}	
	}
	/// <summary>当前时间的天数</summary>
	public int Day
	{
		get
		{
			return (int)((Time / DayScale) + 0.5f);
		}
	}
	private int[] sites;
	private int siteScore;
	private int lastDay;
	/// <summary>开始的系统时间</summary>
	private System.DateTime startTime;
	/// <summary>对战模式规则</summary>
	public DuelRule(ulong gameID, ulong hostID, ulong guestID, float dayScale, int siteCount, int siteScore) : base(gameID, hostID, guestID, dayScale)
	{
		sites = new int[siteCount];
		this.siteScore = siteScore;	
	}
	
	/// <summary>操作: Card - Standby, 拖入待命区</summary>
	public override void DoAction(CardObject card, StandbySlotsArranger standby)
	{
		CardData data = card.CardData;
		
		if (data.Cost > Gold)
			return;
			
		if (data.GetType() == typeof(MagicCardData) || data.GetType() == typeof(WizardCardData))
			return;
		
		if (standby.CarvedCount >= standby._slotsCount)
			return;
		
		Gold -= data.Cost;
		standby.Place(card);	
	}
	/// <summary>操作: Card - Magic, 拖入魔法槽</summary>
	public override void DoAction(CardObject card, MagicSlotObject magic) 
	{
		CardData data = card.CardData;
		
		if (data.Cost > Gold)
			return;

		if (data.GetType() != typeof(MagicCardData))
			return;
			
		Gold -= data.Cost;
		magic.Place(card);
	}
	/// <summary>操作: Card - Carved, 法师攻击或充能</summary>
	public override void DoAction(CardObject card, CarvedObject carved) 
	{
		
	}
	/// <summary>操作: Carved - Carved, 近战或远程攻击</summary>
	public override void DoAction(CarvedObject attacker, CarvedObject victim) 
	{
		
	}
	/// <summary>操作: Carved - Site, 近战上前线或应战</summary>
	public override void DoAction(CarvedObject carved, SiteSlotsArranger site) 
	{
		
	}
	/// <summary>游戏开始时的操作</summary>
	public override void Start() 
	{
		Gold = 100;
		Booster = 2;
		HostileGold = 1;
		HostileBooster = 2;
		Mining = 0;
		
		lastDay = 0;
		startTime = System.DateTime.Now;
	}
	/// <summary>规则Tick</summary>
	public override void Tick()
	{
		// TODO: Change booster modify into a method.
		double lastTime = Time;
		Time = (System.DateTime.Now - startTime).TotalSeconds;
		float deltaTime = (float)(Time - lastTime);
		Mining += deltaTime * (Booster / DayScale);
		if (Mining >= 1f)
		{
			Gold += 1;
			Mining = Mining - 1f;
		}
		if (lastDay < Day)
		{
			lastDay += 1;
			Booster += 1;
			HostileBooster += 1;
		}
	}
	/// <summary>获取前线分数</summary>
	/// <param name="index">前线编号</param>
	public int GetSiteScore(int index)
	{
		if (index < 0 || index >= sites.Length)
		{
			return sites[index];	
		}
		return -1;
	}

}
