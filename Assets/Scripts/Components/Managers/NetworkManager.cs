using UnityEngine;
using TickTick.Events;

public class NetworkManager : MonoBehaviour
{
	public bool _offline = false;
	private event StatusUpdateEventHandler statusUpdateHandler;
	private event InitializeEventHandler initializeHandler;

    // Singleton
    private static NetworkManager _instance;
    public static NetworkManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<NetworkManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
    	if (_offline)
    		PhotonNetwork.offlineMode = true;
        // Singleton
        if (_instance == null)
        {
            _instance = this;
            PhotonNetwork.OnEventCall += EventCall;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(gameObject);
        }
    }

    public void RaiseEvent(IEvent e)
    {
        SendMsg(e.ToByte(), (uint)e.ToByte().Length);
    }

	public void SendMsg(byte[] pubData, uint cubData)
	{
		var data = new object[1];
		//data[0] = cubData;
		data[0] = pubData;
		PhotonNetwork.RaiseEvent(0, data, true, new RaiseEventOptions());
		
	}

	private void EventCall(byte eventcode, object content, int senderid)
	{
		if (eventcode != 0)
			return;

        var data = (object[])content;
		//uint cubData = (uint)data[0];
		var pubData = (byte[])data[0];
        HandleMsg(pubData, 0);
	}

	private void HandleMsg(byte[] pubData, uint cubData)
	{
        if (EventsGroup.GetEventType(pubData) == NetEventType.StatusUpdate)
        {
            Debug.Log("Got StatusUpdateEvent:" + StatusUpdateEvent.ToEvent(pubData).GetStatus());
            if (statusUpdateHandler != null)
            {
                statusUpdateHandler(StatusUpdateEvent.ToEvent(pubData).GetStatus());          
            }
        }

        if (EventsGroup.GetEventType(pubData) == NetEventType.Initialize)
        {
            Debug.Log("Got InitializeEvent:" + InitializeEvent.ToEvent(pubData).GetCardIDList().Length);
            if (initializeHandler != null)
            {
                initializeHandler(InitializeEvent.ToEvent(pubData).GetCardIDList());          
            }
        }

	}

	public void RegisterStatusUpdate(StatusUpdateEventHandler method) { statusUpdateHandler += method; }
	public void RegisterInitialize(InitializeEventHandler method) { initializeHandler += method; }
}
