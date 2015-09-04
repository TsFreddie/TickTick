using UnityEngine;
using TickTick.Events;

public static class NetworkManager
{

	private static event StatusUpdateEventHandler statusUpdateHandler;
	private static event InitializeEventHandler initializeHandler;
    private static event DoActionEventHandler doActionHandler;

    public static void InitailizeConnection()
    {
        PhotonNetwork.ConnectUsingSettings("prototype A");
        PhotonNetwork.OnEventCall += EventCall;
        statusUpdateHandler = null;
        initializeHandler = null;
    }

    public static void RaiseEvent(IEvent e)
    {
        SendMsg(e.ToByte(), (uint)e.ToByte().Length);
    }

	public static void SendMsg(byte[] pubData, uint cubData)
	{
		var data = new object[1];
		//data[0] = cubData;
		data[0] = pubData;
		PhotonNetwork.RaiseEvent(0, data, true, new RaiseEventOptions());
		
	}

	private static void EventCall(byte eventcode, object content, int senderid)
	{
		if (eventcode != 0)
			return;

        var data = (object[])content;
		//uint cubData = (uint)data[0];
		var pubData = (byte[])data[0];
        HandleMsg(pubData, 0);
	}

	private static void HandleMsg(byte[] pubData, uint cubData)
	{
        if (EventsGroup.GetEventType(pubData) == NetEventType.StatusUpdate)
        {
            Debug.Log("Got StatusUpdateEvent => Status:" + StatusUpdateEvent.ToEvent(pubData).GetStatus());
            if (statusUpdateHandler != null)
            {
                statusUpdateHandler(StatusUpdateEvent.ToEvent(pubData).GetStatus());          
            }
        }

        if (EventsGroup.GetEventType(pubData) == NetEventType.Initialize)
        {
            Debug.Log("Got InitializeEvent => CardList:" + InitializeEvent.ToEvent(pubData).GetCardIDList().Length);
            if (initializeHandler != null)
            {
                initializeHandler(InitializeEvent.ToEvent(pubData).GetCardIDList());          
            }
        }
        if (EventsGroup.GetEventType(pubData) == NetEventType.DoAction)
        {
            var doActionEvent = DoActionEvent.ToEvent(pubData);
            Debug.Log("Got DoActionEvent => ActionType:" + doActionEvent.GetActionType());
            Debug.Log("Got DoActionEvent => Sender:" + doActionEvent.GetSender());
            Debug.Log("Got DoActionEvent => Receiver:" + doActionEvent.GetReceiver());
            if(doActionHandler != null)
            {
                doActionHandler(doActionEvent.GetActionType(),doActionEvent.GetSender(),doActionEvent.GetReceiver());
            }
        }

	}

	public static void RegisterStatusUpdate(StatusUpdateEventHandler method) { statusUpdateHandler += method; }
	public static void RegisterInitialize(InitializeEventHandler method) { initializeHandler += method; }
    public static void RegisterDoAction(DoActionEventHandler method) { doActionHandler += method; }
}
