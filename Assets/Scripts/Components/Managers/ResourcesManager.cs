using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		CardPrefab = Resources.Load("Prefabs/Onboard/Card") as GameObject;
		CarvedPrefab = Resources.Load("Prefabs/Onboard/CarvedStone") as GameObject;
	}

}
