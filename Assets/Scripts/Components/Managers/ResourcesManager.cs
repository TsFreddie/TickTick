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
    public Deck PlayerDeck { get; private set; }
    public bool IsHostileLoaded { get; private set; }
    public Rule CurrentRule { get; private set; }

    /// <summary>卡牌容器</summary>
    private Dictionary<int, CardData> cardData;

    private bool cardResLoaded;

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
        IsHostileLoaded = false;
        cardResLoaded = false;
        cardData = new Dictionary<int, CardData>();
        var dataArr = Resources.LoadAll("CardData", typeof(TextAsset));
        foreach (TextAsset data in dataArr)
        {
            CardData newCardData = RawData.RawToCard(data.bytes);
            cardData.Add(newCardData.ID, newCardData);
        }
		CardPrefab = Resources.Load("Prefabs/Onboard/Card") as GameObject;
		CarvedPrefab = Resources.Load("Prefabs/Onboard/CarvedStone") as GameObject;
        PlayerDeck = new Deck();
	}

    public CardData GetCard(int index)
    {
        return cardData[index];
    }

    // TODO: 开始准备卡图的时候完成这步
    public InitializeEventHandler GetInitializeHandler()
    {
        return InitializeHandler;
    }

    public StatusUpdateEventHandler GetStatusUpdateHandler()
    {
        return StatusUpdateHandler;
    }

    /// <summary>
    /// 初始化事件接受器
    /// </summary>
    /// <param name="cardArr"></param>
    public void InitializeHandler(int[] cardArr)
    {
        cardResLoaded = true;
        LoadGameScene();
    }

    /// <summary>
    /// 状态更新事件接收器
    /// </summary>
    /// <param name="status"></param>
    public void StatusUpdateHandler(byte status)
    {
        if (status == 2)
        {
            IsHostileLoaded = true;
        }
    }

    public void LoadGameScene()
    {
        CurrentRule = new DuelRule(0, 0, 0, 40, PlayerDeck, 3, 100);
        Application.LoadLevel("GameTesNew");
    }

}
