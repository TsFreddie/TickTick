using UnityEngine;
using System.Collections;
using Steamworks;

public class TestScript : MonoBehaviour {
    public UnityEngine.UI.Text text;
    public UnityEngine.UI.Text friend;
    public UnityEngine.UI.Text lobby;
    public UnityEngine.UI.Text chatMsg;
    public UnityEngine.UI.Text member;
    public GameObject selection;


    private CallResult<LobbyCreated_t> LobbyCreated;
    private CallResult<LobbyEnter_t> LobbyEnter;
    private CallResult<LobbyMatchList_t> RequestCall;
    private Callback<LobbyChatMsg_t> LobbyChatMsg;
    private Callback<LobbyChatUpdate_t> LobbyChatUpdate;
    private Callback<GameLobbyJoinRequested_t> GameLobbyJoinRequested;

    private CSteamID steamLobbyID;

    private System.Collections.Generic.List<CSteamID> LobbyList;
    private int selectedLobbyID = -1;

    void OnEnable()
    {
        LobbyList = new System.Collections.Generic.List<CSteamID>();
        if (SteamManager.Initialized)
        {
            LobbyCreated = CallResult<LobbyCreated_t>.Create(OnLobbyCreated);
            LobbyEnter = CallResult<LobbyEnter_t>.Create(OnLobbyEnter);
            RequestCall = CallResult<LobbyMatchList_t>.Create(OnRequestList);
            LobbyChatMsg = Callback<LobbyChatMsg_t>.Create(OnLobbyChatMsg);
            LobbyChatUpdate = Callback<LobbyChatUpdate_t>.Create(OnLobbyChatUpdate);
            GameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);

        }
    }
    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t pCallback)
    {
        SteamAPICall_t handle = SteamMatchmaking.JoinLobby(pCallback.m_steamIDLobby);
        RequestCall.Set(handle);
        Debug.Log("Called JoinLobby()");

        friend.text = "Joining Lobby.";
    }

    private void OnLobbyChatUpdate(LobbyChatUpdate_t pCallback)
    {
        int memberCount = SteamMatchmaking.GetNumLobbyMembers(steamLobbyID);
        member.text = "";
        for (int i = 0; i < memberCount; i++)
        {
            member.text += GetFriendPersonaName(SteamMatchmaking.GetLobbyMemberByIndex(steamLobbyID, i)) + "|";
        }
        Debug.Log("LobbyChatUpdate_t Callback");
    }
    private void OnLobbyChatMsg(LobbyChatMsg_t pCallback)
    {
        byte[] buffer = new byte[256];
        EChatEntryType type;
        CSteamID SteamIDUser;
        int getChatMsg = SteamMatchmaking.GetLobbyChatEntry(steamLobbyID, (int)pCallback.m_iChatID, out SteamIDUser, buffer, buffer.Length+1, out type);
        chatMsg.text = chatMsg.text + System.Environment.NewLine + GetFriendPersonaName(SteamIDUser) + ":" + System.Text.Encoding.UTF8.GetString(buffer, 0, getChatMsg);
        Debug.Log("LobbyChatMsg_t Callback");
    }

    private void OnLobbyCreated(LobbyCreated_t pCallback, bool bIOFailure)
    {
        if (bIOFailure)
        {
            Debug.Log("LobbyCreated_t Failed");
            return;
        }
        friend.text = pCallback.m_eResult + System.Environment.NewLine + pCallback.m_ulSteamIDLobby;
        steamLobbyID = (CSteamID)pCallback.m_ulSteamIDLobby;
        SteamMatchmaking.SetLobbyData(steamLobbyID, "2T", "1");

        int memberCount = SteamMatchmaking.GetNumLobbyMembers(steamLobbyID);
        member.text = "";
        for (int i = 0; i < memberCount; i++)
        {
            member.text += GetFriendPersonaName(SteamMatchmaking.GetLobbyMemberByIndex(steamLobbyID, i)) + "|";
        }
        Debug.Log("LobbyCreated_t");
    }

    private void OnLobbyEnter(LobbyEnter_t pCallback, bool bIOFailure)
    {
        if (bIOFailure)
        {
            Debug.Log("LobbyEnter_t Failed");
            return;
        }
        friend.text = "ID:" + pCallback.m_ulSteamIDLobby;
        steamLobbyID.m_SteamID = pCallback.m_ulSteamIDLobby;
        int memberCount = SteamMatchmaking.GetNumLobbyMembers(steamLobbyID);
        member.text = "";
        for (int i = 0; i < memberCount; i++)
        {
            member.text += GetFriendPersonaName(SteamMatchmaking.GetLobbyMemberByIndex(steamLobbyID, i)) + "|";
        }
        Debug.Log("LobbyEnter_t");
    }

    private void OnRequestList(LobbyMatchList_t pCallback, bool bIOFailure)
    {
        if (bIOFailure)
        {
            Debug.Log("SteamAPICall_t Failed");
            return;
        }
        selection.SetActive(false);
        selectedLobbyID = -1;
        LobbyList.Clear();
        lobby.text = "";
        uint lobbiesCount = pCallback.m_nLobbiesMatching;
        for (int i = 0; i < lobbiesCount; i++)
        {
            LobbyList.Add((CSteamID)SteamMatchmaking.GetLobbyByIndex(i));
            lobby.text += SteamMatchmaking.GetLobbyByIndex(i).ToString() + ":" + GetFriendPersonaName(SteamMatchmaking.GetLobbyOwner(SteamMatchmaking.GetLobbyByIndex(i))) + System.Environment.NewLine;
        }
        
    }

    public void UpdateName()
    {
        text.text = GetPersonaName();
    }

    public void UpdateFriends()
    {
        friend.text = GetFriendList();
    }

    public string GetFriendList()
    {
        if (!SteamManager.Initialized)
            return "";
        string listRes = "";
        int friendsCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagAll);
        for (int i = 0; i < friendsCount; i++)
        {
            CSteamID steamIDFriend = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagAll);
            listRes += SteamFriends.GetFriendPersonaName(steamIDFriend) + System.Environment.NewLine;
        }
        return listRes;
    }

    public string GetPersonaName()
    {
        if (!SteamManager.Initialized)
            return "";
        return SteamFriends.GetPersonaName();
    }

    public string GetFriendPersonaName(CSteamID steamID)
    {
        if (!SteamManager.Initialized)
            return "";
        return SteamFriends.GetFriendPersonaName(steamID);
    }

    public void CreateLobby()
    {
        if (!SteamManager.Initialized)
            return;
        if (steamLobbyID.IsValid() && steamLobbyID.IsLobby())
        {
            SteamMatchmaking.LeaveLobby(steamLobbyID);
        }
        SteamAPICall_t handle = SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, 20);
        LobbyCreated.Set(handle);
        Debug.Log("Called CreateLobby()");
    }

    public void LeaveLobby()
    {
        if (!SteamManager.Initialized)
            return;
        if (!steamLobbyID.IsValid() || !steamLobbyID.IsLobby())
        {
            friend.text = "No lobby joined.";
            Debug.Log("No lobby joined.");
        }
        SteamMatchmaking.LeaveLobby(steamLobbyID);
        friend.text = "Lobby left.";
        Debug.Log("Lobby left.");
    }

    public void SearchLobby()
    {
        if (!SteamManager.Initialized)
            return;
        SteamMatchmaking.AddRequestLobbyListStringFilter("2T", "1", ELobbyComparison.k_ELobbyComparisonEqual);
        SteamAPICall_t handle = SteamMatchmaking.RequestLobbyList();
        RequestCall.Set(handle);
        Debug.Log("Called RequestLobbyList()");
    }

    public void SendChat(string str)
    {
        if (!SteamManager.Initialized)
            return;
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
        SteamMatchmaking.SendLobbyChatMsg(steamLobbyID, buffer, buffer.Length);
    }

    public void SelectLobby(UnityEngine.EventSystems.BaseEventData e)
    {
        Vector2 diff = ((UnityEngine.EventSystems.PointerEventData)e).position - (Vector2)lobby.gameObject.gameObject.transform.position;
        int index = (int)(-diff.y / 15);
        if (index < LobbyList.Count)
        {
            selection.SetActive(true);
            selection.gameObject.transform.localPosition = new Vector3(selection.gameObject.transform.localPosition.x, -7.5f - 15 * index, selection.gameObject.transform.localPosition.z);
        }
        selectedLobbyID = index;
    }

    public void JoinLobby()
    {
        if (!SteamManager.Initialized)
            return;
        if (selectedLobbyID == -1)
        {
            friend.text = "No lobby selected.";
            Debug.Log("No lobby selected.");
        }
        SteamAPICall_t handle = SteamMatchmaking.JoinLobby(LobbyList[selectedLobbyID]);
        LobbyEnter.Set(handle);
        Debug.Log("Called JoinLobby()");

        friend.text = "Joining Lobby.";
    }
}
