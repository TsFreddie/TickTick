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


