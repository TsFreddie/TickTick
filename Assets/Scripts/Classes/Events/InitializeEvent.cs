using System;

namespace TickTick.Events
{
    public delegate void InitializeEventHandler(int[] cardIDList);

    public struct InitializeEvent : IEvent
    {
        const int FIXED_BUFFER_SIZE = 2;

        private int[] cardIDList;

        /// <summary>
        /// 初始化事件，交换卡组
        /// </summary>
        /// <param name="cardIDList">卡牌id数组</param>
        public InitializeEvent(int[] cardIDList)
        {
            this.cardIDList = cardIDList;
        }

        public byte[] ToByte()
        {
        	// 卡组数大于255你逗我?
        	if (cardIDList.Length > 255)
        		throw new OverflowException();

        	byte arrLen = (byte)cardIDList.Length;
            var buffer = new byte[FIXED_BUFFER_SIZE + arrLen * 4];
            buffer[0] = (byte)NetEventType.Initialize;
            buffer[1] = arrLen;
            for (byte i = 0; i < arrLen; i++)
            	BitConverter.GetBytes(cardIDList[i]).CopyTo(buffer, FIXED_BUFFER_SIZE + i * 4);
            return buffer;
        }

        public int[] GetCardIDList()
        {
            return cardIDList;
        }

        public static InitializeEvent ToEvent(byte[] buffer)
        {
            if (buffer[0] != (byte)NetEventType.Initialize || buffer.Length < FIXED_BUFFER_SIZE)
                return new InitializeEvent(null);

            byte arrLen = buffer[1];
            var cardIDList = new int[arrLen];
            for (byte i = 0; i < arrLen; i++)
            {
            	cardIDList[i] = BitConverter.ToInt32(buffer, FIXED_BUFFER_SIZE + i * 4);
            }
            return new InitializeEvent(cardIDList);
        }
    }
}