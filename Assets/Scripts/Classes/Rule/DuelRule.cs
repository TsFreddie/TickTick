using UnityEngine;
using System.Collections;

public class DuelRule : Rule
{
	private int[] sites;
	private int siteScore;
	public DuelRule(ulong gameID, ulong hostID, ulong guestID, int siteCount, int siteScore) : base(gameID, hostID, guestID)
	{
		sites = new int[siteCount];
		this.siteScore = siteScore;			
	}
	
	/// <summary>操作: Card - Standby, 拖入待命区</summary>
	public override void DoAction(CardObject card, StandbySlotsArranger standby)
	{
		standby.Place(card);	
	}
	/// <summary>操作: Card - Magic, 拖入魔法槽</summary>
	public override void DoAction(CardObject card, MagicSlotObject magic) 
	{
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
		
	}
}
