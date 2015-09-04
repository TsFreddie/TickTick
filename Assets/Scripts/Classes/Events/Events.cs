namespace TickTick.Events
{
    public interface IEvent
    {
        byte[] ToByte();
    }

    public enum NetEventType
    {
        Empty = 0,
        StatusUpdate,
        Initialize, // 此事件交换卡组，因为现在没有卡图，并不需要载入数据，所以此步被跳过。 TODO: Fuck this.
        InfomationUpdate,
        DoAction,
        Undefined,
    }

    public static class EventsGroup
    {

        public static NetEventType GetEventType(byte[] e)
        {
            if (e == null)
                return NetEventType.Undefined;

            if (e.Length <= 0)
                return NetEventType.Undefined;

            return (NetEventType)e[0];
        }
    }


}


