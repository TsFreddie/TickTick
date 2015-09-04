using UnityEngine;
using TickTick;
using TickTick.Events;

// 此Manager为临时用，匹配系统需重写

public class ConnectTest : Photon.MonoBehaviour {

	public UnityEngine.UI.Text _networkStatus;
	public UnityEngine.UI.Text _roomList;
	public UnityEngine.UI.InputField _playerName;
	public UnityEngine.UI.Text _warningText;
	public UnityEngine.UI.Image _selected;
	public UnityEngine.UI.Button _joinButton;
	public UnityEngine.UI.Text _player1;
	public UnityEngine.UI.Text _player2;
	public GameObject _battlePanel;
	public GameObject _readyButton;

	private Deck myDeck;

	private RoomInfo[] rooms;
	private int selectedItem = -1;

	private bool selfReady;
	private bool hostileReady;

    void Start()
    {
    	Connect();
    	NetworkManager.RegisterInitialize(ResourcesManager.Instance.GetInitializeHandler());
    	NetworkManager.RegisterStatusUpdate(ResourcesManager.Instance.GetStatusUpdateHandler());
    	NetworkManager.RegisterStatusUpdate(ReadyUpdate);
    	selfReady = false;
		hostileReady = false;
		myDeck = new Deck();
    }

    void Update()
    {
    	if (_networkStatus != null)
    		_networkStatus.text = PhotonNetwork.connectionState+ " " + PhotonNetwork.GetPing();
    	CheckReady();
    }

	// Use this for initialization
	void Connect ()
	{
		NetworkManager.InitailizeConnection();
	}

	void OnReceivedRoomListUpdate()
	{
		RefreshRoomList();
	}

	void OnJoinedRoom()
	{
		_battlePanel.SetActive(true);
		_player1.text = PhotonNetwork.playerName;
		PhotonPlayer[] players = PhotonNetwork.otherPlayers;
		if (players.Length > 0)
		{
			_player2.text = players[0].name;
			_readyButton.SetActive(true);
		}
		else
		{
			selfReady = false;
			hostileReady = false;
			_player2.text = "Wating...";
			_readyButton.SetActive(false);
		}
	}

	void OnPhotonPlayerConnected()
	{
		_player1.text = PhotonNetwork.playerName;
		PhotonPlayer[] players = PhotonNetwork.otherPlayers;
		if (players.Length > 0)
		{
			_player2.text = players[0].name;
			_readyButton.SetActive(true);
		}
		else
		{
			selfReady = false;
			hostileReady = false;
			_player2.text = "Wating...";
			_readyButton.SetActive(false);
		}
	}

	void OnPhotonPlayerDisconnected()
	{
		_player1.text = PhotonNetwork.playerName;
		PhotonPlayer[] players = PhotonNetwork.otherPlayers;
		if (players.Length > 0)
		{
			_player2.text = players[0].name;
			_readyButton.SetActive(true);
		}
		else
		{
			selfReady = false;
			hostileReady = false;
			_player2.text = "Wating...";
			_readyButton.SetActive(false);
		}
	}

	void OnLeftRoom()
	{
		selfReady = false;
		hostileReady = false;
		_readyButton.SetActive(false);
		_battlePanel.SetActive(false);
	}

	private void CheckReady()
	{
		if (selfReady && hostileReady)
		{
            NetworkManager.RaiseEvent(new InitializeEvent(myDeck.GetCardIDArray()));
            selfReady = false;
        }
	}
	public void RefreshRoomList()
	{
		selectedItem = -1;
		_joinButton.interactable = false;
		_selected.gameObject.SetActive(false);
		_roomList.text = "";
		rooms = PhotonNetwork.GetRoomList();
		for (int i = 0; i < rooms.Length; i++)
		{
			_roomList.text += rooms[i].name + " | " + (rooms[i].playerCount > 1 ? "Full" : "Waiting") + System.Environment.NewLine;
		}
		if (rooms.Length == 0)
		{
			_roomList.text = "No Game Found.";
		}
	}

	public void Select(UnityEngine.EventSystems.BaseEventData e)
	{
		if (e.GetType() == typeof(UnityEngine.EventSystems.PointerEventData))
		{
			RectTransform trans = _roomList.GetComponent<RectTransform>();
			Vector2 clickedPos = ((UnityEngine.EventSystems.PointerEventData)e).position;
			float clickedY = clickedPos.y - trans.position.y;
			int item = (int)(clickedY / (_roomList.fontSize + _roomList.lineSpacing));
			if (item < 0 || item >= rooms.Length)
				return;
			_selected.gameObject.SetActive(true);
			_joinButton.interactable = true;
			_selected.GetComponent<RectTransform>().anchoredPosition = new Vector3(_selected.GetComponent<RectTransform>().localPosition.x, -item * (_roomList.fontSize + _roomList.lineSpacing), _selected.GetComponent<RectTransform>().localPosition.z);
            selectedItem = item;
		}

	}

	public void JoinRoom()
	{
		if (_playerName.text == "")
		{
			_playerName.text = "LazyDog";
		}
		PhotonNetwork.playerName = _playerName.text;
		PhotonNetwork.JoinRoom(rooms[selectedItem].name);
	}

	public void CreateRoom()
	{

		if (_playerName.text == "")
		{
			_warningText.text = "Please put your name first!";
			return;
		}
		PhotonNetwork.playerName = _playerName.text;
		var options = new RoomOptions();
		options.isOpen = true;
		options.isVisible = true;
		options.maxPlayers = 2;
		PhotonNetwork.CreateRoom(_playerName.text, options, PhotonNetwork.lobby);
	}

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void Ready()
    {
    	selfReady = true;
    	_player1.text = "-" + _player1.text + "-";
    	_readyButton.SetActive(false);
        NetworkManager.RaiseEvent(new StatusUpdateEvent(1));
    }

    private void ReadyUpdate(byte status)
    {
    	if (status == 1)
    	{
    		hostileReady = true;
    		_player2.text = "-" + _player2.text + "-";
    	}
    }


}
