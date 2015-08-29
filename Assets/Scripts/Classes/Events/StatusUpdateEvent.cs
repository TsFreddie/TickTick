namespace TickTick.Events
{
    public delegate void StatusUpdateEventHandler(byte status);

    public struct StatusUpdateEvent : IEvent
    {
        const int BUFFER_SIZE = 2;

        private byte status;

        /// <summary>
        /// 状态更新事件, 0 - undefined, 1 - ready, 2 - loaded
        /// </summary>
        /// <param name="status"></param>
        public StatusUpdateEvent(byte status)
        {
            this.status = status;
        }

        public byte[] ToByte()
        {
            byte[] buffer = new byte[BUFFER_SIZE];
            buffer[0] = (byte)NetEventType.StatusUpdate;
            buffer[1] = status;
            return buffer;
        }

        public byte GetStatus()
        {
            return status;
        }

        public static StatusUpdateEvent ToEvent(byte[] buffer)
        {
            if (buffer[0] != (byte)NetEventType.StatusUpdate || buffer.Length < BUFFER_SIZE)
                return new StatusUpdateEvent(0);

            return new StatusUpdateEvent(buffer[1]);
        }
    }
}