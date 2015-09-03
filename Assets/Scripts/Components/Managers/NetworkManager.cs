using UnityEngine;
using TickTick.Events;

public class NetworkManager : MonoBehaviour
{
	public event StatusUpdateEventHandler StatusUpdateHandler;

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
		byte[] pubData = (byte[])data[0];
        HandleMsg(pubData, 0);
	}

	private void HandleMsg(byte[] pubData, uint cubData)
	{
        if (EventsGroup.GetEventType(pubData) == NetEventType.StatusUpdate)
        {
            Debug.Log("Got StatusUpdateEvent:" + StatusUpdateEvent.ToEvent(pubData).GetStatus());
            if (StatusUpdateHandler != null)
            {
                StatusUpdateHandler(StatusUpdateEvent.ToEvent(pubData).GetStatus());
                
            }
                
        }
	}
}
