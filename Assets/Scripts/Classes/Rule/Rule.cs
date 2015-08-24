using UnityEngine;

/// <summary>
/// 规则基类，为不同模式提供通用实现
/// 新规则类即代表一盘游戏
/// </summary>
public class Rule
{
	private ulong gameID;
	private ulong hostID;
	private ulong guestID;
	
	protected Rule(ulong gameID, ulong hostID, ulong guestID)
	{
		this.gameID = gameID;
		this.hostID = hostID;
		this.guestID = guestID;
	}
	
	/// <summary>操作: Card - Standby, 拖入待命区</summary>
	public virtual void DoAction(CardObject card, StandbySlotsArranger standby) {}
	/// <summary>操作: Card - Magic, 拖入魔法槽</summary>
	public virtual void DoAction(CardObject card, MagicSlotObject magic) {}
	/// <summary>操作: Card - Carved, 法师攻击或充能</summary>
	public virtual void DoAction(CardObject card, CarvedObject carved) {}
	/// <summary>操作: Carved - Carved, 近战或远程攻击</summary>
	public virtual void DoAction(CarvedObject attacker, CarvedObject victim) {}
	/// <summary>操作: Carved - Site, 近战上前线或应战</summary>
	public virtual void DoAction(CarvedObject carved, SiteSlotsArranger site) {}
	
	public virtual void Start() {}
	
	/// <summary>
	/// 双方交换的网络消息日志，用于录制和保存demo
	/// </summary>
	protected void Record(byte[] msg)
	{
		
	}
}
