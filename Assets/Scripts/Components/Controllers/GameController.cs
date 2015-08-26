using UnityEngine;
using System.Collections;

/// <summary>
/// 游戏控制器组件,负责分配输入指令
/// </summary>
public class GameController : MonoBehaviour
{
    public delegate void MousePosDelegate(Vector3 mousePosition);
    public delegate void MouseRayDelegate(Ray worldRay);
    public delegate void MouseNamDelegate();

    public event MousePosDelegate MousePosDown;
    public event MousePosDelegate MousePosDownCancel;
    public event MouseRayDelegate MouseRayDown;
    public event MouseRayDelegate MouseRayDownCancel;
    public event MousePosDelegate MousePosUp;
    public event MouseRayDelegate MouseRayUp;
    public event MousePosDelegate MousePosMove;
    public event MouseRayDelegate MouseRayMove;
    public event MouseNamDelegate MouseUp;
    public event MouseNamDelegate MouseDown;
    public event MouseNamDelegate MouseDownCancel;

    // Singleton
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
        // Singleton
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
                    MouseDownCancel();
                if (MouseRayDownCancel != null)
                    MouseRayDownCancel(Camera.main.ScreenPointToRay(Input.mousePosition));
                if (MousePosDownCancel != null)
                    MousePosDownCancel(Input.mousePosition);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (MouseDown != null)
                    MouseDown();
                if (MousePosDown != null)
                    MousePosDown(Input.mousePosition);
                if (MouseRayDown != null)
                    MouseRayDown(Camera.main.ScreenPointToRay(Input.mousePosition));       
            }
            if (MousePosMove != null)
                MousePosMove(Input.mousePosition);
            if (MouseRayMove != null)
                MouseRayMove(Camera.main.ScreenPointToRay(Input.mousePosition));
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (MouseUp != null)
                MouseUp();
            if (MouseRayUp != null)
                MouseRayUp(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (MousePosUp != null)
                MousePosUp(Input.mousePosition);
        }
    }
}
