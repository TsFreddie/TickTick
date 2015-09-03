using UnityEngine;
using System.Collections.Generic;

using TickTick;
using TickTick.Utils;
using TickTick.Events;

/// <summary>
/// 资源管理组件,顺便负责切换场景,Singleton
/// </summary>
public class ResourcesManager : MonoBehaviour {
    // Singleton
    private static ResourcesManager _instance;
    public static ResourcesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ResourcesManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
	/// <summary>卡牌Prefab</summary>
	public GameObject CardPrefab { get; private set; }
	/// <summary>刻石Prefab</summary>
	public GameObject CarvedPrefab { get; private set; }
    /// <summary>卡牌容器</summary>
    private Dictionary<int, CardData> cardData;

    private Deck myDeck;

    private bool cardResLoaded;
    private bool hostileLoaded;
	
    void Awake()
    {
        // Singleton
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(gameObject);
        }
    }
	
	void Start () {
        hostileLoaded = false;
        cardResLoaded = false;
        cardData = new Dictionary<int, CardData>();
        for (int i = 0; i < 100; i++)
        {
            var data = (TextAsset)Resources.Load("CardData/"+i);
            CardData newCardData = RawData.RawToCard(data.bytes);
            cardData.Add(newCardData.ID, newCardData);
        }
		CardPrefab = Resources.Load("Prefabs/Onboard/Card") as GameObject;
		CarvedPrefab = Resources.Load("Prefabs/Onboard/CarvedStone") as GameObject;
        myDeck = new Deck(); //TODO: 卡组管理器
	}

    public CardData GetCard(int index)
    {
        return cardData[index];
    }

    // TODO: 开始准备卡图的时候完成这步
    public InitializeEventHandler GetInitializeHandler()
    {
        return GetHostileDeck;
    }

    public StatusUpdateEventHandler GetStatusUpdateHandler()
    {
        return GetLoaded;
    }
    public void GetHostileDeck(int[] cardArr)
    {
        cardResLoaded = true;
        LoadGameScene();
    }

    public void LoadGameScene()
    {
        Application.LoadLevel("GameTesNew");
    }

    public void GetLoaded(byte status)
    {
        if (status == 2)
        {
            hostileLoaded = true;
        }
    }

    public bool IsHostileLoaded()
    {
        return hostileLoaded;
    }

}
