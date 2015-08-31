using UnityEngine;
using System.Collections.Generic;

using TickTick;
using TickTick.Utils;

/// <summary>
/// 资源管理组件,Singleton
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
        cardData = new Dictionary<int, CardData>();
        for (int i = 0; i < 100; i++)
        {
            var data = (TextAsset)Resources.Load("CardData/"+i);
            CardData newCardData = RawData.RawToCard(data.bytes);
            cardData.Add(newCardData.ID, newCardData);
        }
		CardPrefab = Resources.Load("Prefabs/Onboard/Card") as GameObject;
		CarvedPrefab = Resources.Load("Prefabs/Onboard/CarvedStone") as GameObject;
	}

    public CardData GetCard(int index)
    {
        return cardData[index];
    }

}
