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

    private MousePosDelegate MousePosDown;
    private MousePosDelegate MousePosDownCancel;
    private MouseRayDelegate MouseRayDown;
    private MouseRayDelegate MouseRayDownCancel;
    private MousePosDelegate MousePosUp;
    private MouseRayDelegate MouseRayUp;
    private MousePosDelegate MousePosMove;
    private MouseRayDelegate MouseRayMove;
    private MouseNamDelegate MouseUp;
    private MouseNamDelegate MouseDown;
    private MouseNamDelegate MouseDownCancel;

    // Singleton
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameController>();
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
                Destroy(gameObject);
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

    #region 事件注册
    public void RegisterMouseDown(MousePosDelegate method)         { MousePosDown += method; }
    public void UnregisterMouseDown(MousePosDelegate method)       { MousePosDown -= method; }
    public void RegisterMouseDown(MouseRayDelegate method)         { MouseRayDown += method; }
    public void UnregisterMouseDown(MouseRayDelegate method)       { MouseRayDown -= method; }
    public void RegisterMouseDown(MouseNamDelegate method)         { MouseDown += method; }
    public void UnregisterMouseDown(MouseNamDelegate method)       { MouseDown -= method; }
    public void RegisterMouseDownCancel(MousePosDelegate method)   { MousePosDownCancel += method; }
    public void UnregisterMouseDownCancel(MousePosDelegate method) { MousePosDownCancel -= method; }
    public void RegisterMouseDownCancel(MouseRayDelegate method)   { MouseRayDownCancel += method; }
    public void UnregisterMouseDownCancel(MouseRayDelegate method) { MouseRayDownCancel -= method; }
    public void RegisterMouseDownCancel(MouseNamDelegate method)   { MouseDownCancel += method; }
    public void UnregisterMouseDownCancel(MouseNamDelegate method) { MouseDownCancel -= method; }
    public void RegisterMouseUp(MousePosDelegate method)           { MousePosUp += method; }
    public void UnregisterMouseUp(MousePosDelegate method)         { MousePosUp -= method; }
    public void RegisterMouseUp(MouseRayDelegate method)           { MouseRayUp += method; }
    public void UnregisterMouseUp(MouseRayDelegate method)         { MouseRayUp -= method; }
    public void RegisterMouseUp(MouseNamDelegate method)           { MouseUp += method; }
    public void UnregisterMouseUp(MouseNamDelegate method)         { MouseUp -= method; }
    public void RegisterMouseMove(MousePosDelegate method)         { MousePosMove += method; }
    public void UnregisterMouseMove(MousePosDelegate method)       { MousePosMove -= method; }
    public void RegisterMouseMove(MouseRayDelegate method)         { MouseRayMove += method; }
    public void UnregisterMouseMove(MouseRayDelegate method)       { MouseRayMove -= method; }
    #endregion
}
