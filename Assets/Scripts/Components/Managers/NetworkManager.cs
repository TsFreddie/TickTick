using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{

	void Awake()
	{
		PhotonNetwork.OnEventCall += EventCall;
	}

	public static void SendMsg(byte[] pubData, uint cubData)
	{
		var data = new object[2];
		data[0] = cubData;
		data[1] = pubData;
		PhotonNetwork.RaiseEvent(0, data, true, new RaiseEventOptions());
		
	}

	private void EventCall(byte eventcode, object content, int senderid)
	{
		if (eventcode != 0)
			return;
		var data = (object[])content;
		uint cubData = (uint)data[0];
		byte[] pubData = (byte[])data[1];
		HandleMsg(pubData, cubData);
	}

	private void HandleMsg(byte[] pubData, uint cubData)
	{

	}

}
