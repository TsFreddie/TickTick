using System;

namespace TickTick.Events
{
    public delegate void DoActionEventHandler(byte cardAction,int card,int cardObject);


    public struct DoActionEvent : IEvent
    {
        const int BUFFER_SIZE = 10;

        private byte cardAction;
        private int card;
        private int cardObject;
        

        /// <summary>
        /// 卡牌动作事件
        /// </summary>
        /// <param name ="card">卡牌id</param>
        /// <param name ="cardObject">被操作的卡牌组件</param>
        /// <param name ="cardAction">卡牌动作类型,0 - undefined, 1 - Card>Standby, 2 - Card>Magic, 3 - Card>Carved, 4 - Carved>Carved, 5 - Carved>Site </param>
        public DoActionEvent(byte cardAction,int card, int cardObject)
        {
            this.cardAction = cardAction;
            this.card = card;
            this.cardObject = cardObject;
            
        }

        public byte[] ToByte()
        {
            var buffer = new byte[BUFFER_SIZE];
            buffer[0] = (byte)NetEventType.DoAction;
            buffer[1] = cardAction;
            BitConverter.GetBytes(card).CopyTo(buffer, 2);
            BitConverter.GetBytes(cardObject).CopyTo(buffer, 6);
            return buffer;
        }

        public byte GetCardAction()
        {
            return this.cardAction;
        }

        public int GetCard()
        {
            return this.card;
        }

        public int GetCardObject()
        {
            return this.cardObject;
        }


        public static DoActionEvent ToEvent(byte[] buffer)
        {
            if(buffer[0] != (byte)NetEventType.DoAction || buffer.Length <BUFFER_SIZE)
                return new DoActionEvent(0, 0, 0);
            var cardAction = buffer[1];
            int card = BitConverter.ToInt32(buffer,2);
            int cardObject = BitConverter.ToInt32(buffer,6);
            
            return new DoActionEvent(cardAction,card,cardObject);
        }
    }
}
