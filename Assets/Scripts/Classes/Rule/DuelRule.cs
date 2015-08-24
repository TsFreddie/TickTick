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
}
