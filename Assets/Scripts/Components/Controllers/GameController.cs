using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏控制器组件,负责分配输入指令
/// </summary>
public class GameController : MonoBehaviour
{
    public delegate void MousePosDelegate(Vector3 mousePosition);
    public delegate void MouseRayDelegate(Ray worldRay);
    public delegate void MouseDelegate();
    public event MousePosDelegate MouseDown;           // 左键事件
    public event MousePosDelegate MouseDownCancel;     // 左键后右键事件
    public event MouseRayDelegate MouseRayDown;        // 左键碰撞事件
    public event MouseRayDelegate MouseRayDownCancel;  // 左键后右键碰撞事件
    public event MouseDelegate MouseUp;                // 右键抬起事件

    public event MousePosDelegate MouseMove;           // 鼠标移动事件
    public event MouseRayDelegate MouseRayMove;        // 鼠标移动碰撞事件

    // Singleton 定义
    private static GameController _instance;
    public static GameController instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameController>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        // Singleton 定义
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    void Start()
    {
        MouseDown = null;
        MouseDownCancel = null;
        MouseRayDown = null;
        MouseRayDownCancel = null;
        MouseUp = null;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (MouseDownCancel != null)
                    MouseDownCancel(Input.mousePosition);
                if (MouseRayDownCancel != null)
                    MouseRayDownCancel(Camera.main.ScreenPointToRay(Input.mousePosition));
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (MouseDown != null)
                    MouseDown(Input.mousePosition);
                if (MouseRayDown != null)
                {
                    MouseRayDown(Camera.main.ScreenPointToRay(Input.mousePosition));
                }          
            }
            if (MouseMove != null)
                MouseMove(Input.mousePosition);
            if (MouseRayMove != null)
                MouseRayMove(Camera.main.ScreenPointToRay(Input.mousePosition));
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (MouseUp != null)
            {
                MouseUp();
            }
        }
    }
}
