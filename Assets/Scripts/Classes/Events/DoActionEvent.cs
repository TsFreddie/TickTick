using System;

namespace TickTick.Events
{
    public delegate void DoActionEventHandler(byte actionType, int sender,int receiver);


    public struct DoActionEvent : IEvent
    {
        const int BUFFER_SIZE = 10;

        private byte actionType;
        private int sender;
        private int receiver;


        /// <summary>
        /// 卡牌动作事件
        /// </summary>
        /// <param name ="sender">执行操作的组件</param>
        /// <param name ="receiver">被操作的组件</param>
        /// <param name ="actionType">组件动作类型,0 - undefined, 1 - Card>Standby, 2 - Card>Magic, 3 - Card>Carved, 4 - Carved>Carved, 5 - Carved>Site </param>
        public DoActionEvent(byte actionType, int sender, int receiver)
        {
            this.actionType = actionType;
            this.sender = sender;
            this.receiver = receiver;
            
        }

        public byte[] ToByte()
        {
            var buffer = new byte[BUFFER_SIZE];
            buffer[0] = (byte)NetEventType.DoAction;
            buffer[1] = actionType;
            BitConverter.GetBytes(sender).CopyTo(buffer, 2);
            BitConverter.GetBytes(receiver).CopyTo(buffer, 6);
            return buffer;
        }

        public byte GetActionType()
        {
            return this.actionType;
        }

        public int GetSender()
        {
            return this.sender;
        }

        public int GetReceiver()
        {
            return this.receiver;
        }


        public static DoActionEvent ToEvent(byte[] buffer)
        {
            if(buffer[0] != (byte)NetEventType.DoAction || buffer.Length <BUFFER_SIZE)
                return new DoActionEvent(0, 0, 0);
            var actionType = buffer[1];
            int sender = BitConverter.ToInt32(buffer,2);
            int receiver = BitConverter.ToInt32(buffer,6);
            
            return new DoActionEvent(actionType, sender,receiver);
        }
    }
}
