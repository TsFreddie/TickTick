using System;
using System.Collections.Generic;
namespace TickTick
{
	public class Hand
	{
		public delegate void HandCallBack(bool add, int handID, int cardID);

		// Tuple<HandID, CardID>
		private Dictionary<int,int> cardDataDict;
		private HandCallBack callback;
		private int handID;

		public Hand()
		{
			cardDataDict = new Dictionary<int, int>();
			handID = 0;
		}

		public void AddCard(int cardID)
		{
			cardDataDict.Add(handID, cardID);
			if (callback != null)
				callback(true, handID, cardID);
			handID++;
		}

		public void RemoveCard(int handID, int cardID)
		{
			cardDataDict.Remove(handID);
			if (callback != null)
				callback(false, handID, cardID);
		}

		public void RegisterHandCallBack(HandCallBack method) { callback += method; }
	}
}