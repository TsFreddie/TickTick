namespace TickTick
{
	public class Deck {

		int[] cardArr;
		int cardCount;

		/// <summary>
		/// 临时生成的全卡组 TODO: Fuck this
		/// </summary>
		public Deck()
		{
			cardArr = new int[100];
			for (int i = 0; i < 100; i++)
			{
				cardArr[i] = i;
			}
			cardCount = 0;
		}

		/// <summary>
		/// 洗牌函数
		/// </summary>
		public void Shuffle()
		{
			// TODO: 不洗牌的黑箱
		}

		/// <summary>
		/// 抽牌函数 TODO: Fuck this
		/// </summary>
		public int Draw()
		{
			cardCount += 1;
			return cardArr[cardCount - 1];
		}
	}
}

