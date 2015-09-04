using TickTick;
using System;
using UnityEngine;
namespace TickTick.Utils
{
	/// <summary>
	/// 文件数据格式化和解析静态方法类
	/// </summary>
	public static class RawData
	{
		public static CardData RawToCard(byte[] rawData)
		{
			if (rawData == null)
				return null;
			if (rawData.Length <= 0)
				return null;
			switch ((CardType)rawData[0])
			{
				case CardType.Melee:
					return new MeleeCardData(Bit(rawData, 0), Bit(rawData, 1), Bit(rawData, 2), (ElementType)Bit(rawData, 3), Bit(rawData, 4), Bit(rawData, 5), Bit(rawData, 6));
				case CardType.Range:
					return new RangeCardData(Bit(rawData, 0), Bit(rawData, 1), Bit(rawData, 2), (ElementType)Bit(rawData, 3), Bit(rawData, 4), Bit(rawData, 5), Bit(rawData, 6), Bit(rawData, 7));
				case CardType.Wizard:
					return new WizardCardData(Bit(rawData, 0), Bit(rawData, 1), Bit(rawData, 2), (ElementType)Bit(rawData, 3), Bit(rawData, 4));
				case CardType.Summon:
					return new SummonCardData(Bit(rawData, 0), Bit(rawData, 1), Bit(rawData, 2), (ElementType)Bit(rawData, 3), Bit(rawData, 4), Bit(rawData, 5), Bit(rawData, 6));
				case CardType.Magic:
					break;
			}
			return null;
		}

		// TODO: Fuck this
		private static int Bit(Array rawData, int index)
		{
			byte[] intByte = new Byte[4];
			Array.Copy(rawData, 1+4*index, intByte, 0, 4);
			Array.Reverse(intByte);
			return BitConverter.ToInt32(intByte, 0);
		}
	}
}
