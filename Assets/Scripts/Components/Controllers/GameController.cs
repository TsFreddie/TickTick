using UnityEngine;

/// <summary>
/// 游戏控制器组件,负责分配输入指令
/// </summary>
public class GameController : MonoBehaviour
{
    public delegate void MousePosDelegate(Vector3 mousePosition);
    public delegate void MouseRayDelegate(Ray worldRay);
    public delegate void MouseNamDelegate();

    private event MousePosDelegate mousePosDown;
    private event MousePosDelegate mousePosDownCancel;
    private event MouseRayDelegate mouseRayDown;
    private event MouseRayDelegate mouseRayDownCancel;
    private event MousePosDelegate mousePosUp;
    private event MouseRayDelegate mouseRayUp;
    private event MousePosDelegate mousePosMove;
    private event MouseRayDelegate mouseRayMove;
    private event MouseNamDelegate mouseUp;
    private event MouseNamDelegate mouseDown;
    private event MouseNamDelegate mouseDownCancel;

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
        mouseDown = null;
        mouseDownCancel = null;
        mouseRayDown = null;
        mouseRayDownCancel = null;
        mouseUp = null;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (mouseDownCancel != null)
                    mouseDownCancel();
                if (mouseRayDownCancel != null)
                    mouseRayDownCancel(Camera.main.ScreenPointToRay(Input.mousePosition));
                if (mousePosDownCancel != null)
                    mousePosDownCancel(Input.mousePosition);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (mouseDown != null)
                    mouseDown();
                if (mousePosDown != null)
                    mousePosDown(Input.mousePosition);
                if (mouseRayDown != null)
                    mouseRayDown(Camera.main.ScreenPointToRay(Input.mousePosition));       
            }
            if (mousePosMove != null)
                mousePosMove(Input.mousePosition);
            if (mouseRayMove != null)
                mouseRayMove(Camera.main.ScreenPointToRay(Input.mousePosition));
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (mouseUp != null)
                mouseUp();
            if (mouseRayUp != null)
                mouseRayUp(Camera.main.ScreenPointToRay(Input.mousePosition));
            if (mousePosUp != null)
                mousePosUp(Input.mousePosition);
        }
    }

    #region 事件注册
    public void RegisterMouseDown(MousePosDelegate method)         { mousePosDown += method; }
    public void UnregisterMouseDown(MousePosDelegate method)       { mousePosDown -= method; }
    public void RegisterMouseDown(MouseRayDelegate method)         { mouseRayDown += method; }
    public void UnregisterMouseDown(MouseRayDelegate method)       { mouseRayDown -= method; }
    public void RegisterMouseDown(MouseNamDelegate method)         { mouseDown += method; }
    public void UnregisterMouseDown(MouseNamDelegate method)       { mouseDown -= method; }
    public void RegisterMouseDownCancel(MousePosDelegate method)   { mousePosDownCancel += method; }
    public void UnregisterMouseDownCancel(MousePosDelegate method) { mousePosDownCancel -= method; }
    public void RegisterMouseDownCancel(MouseRayDelegate method)   { mouseRayDownCancel += method; }
    public void UnregisterMouseDownCancel(MouseRayDelegate method) { mouseRayDownCancel -= method; }
    public void RegisterMouseDownCancel(MouseNamDelegate method)   { mouseDownCancel += method; }
    public void UnregisterMouseDownCancel(MouseNamDelegate method) { mouseDownCancel -= method; }
    public void RegisterMouseUp(MousePosDelegate method)           { mousePosUp += method; }
    public void UnregisterMouseUp(MousePosDelegate method)         { mousePosUp -= method; }
    public void RegisterMouseUp(MouseRayDelegate method)           { mouseRayUp += method; }
    public void UnregisterMouseUp(MouseRayDelegate method)         { mouseRayUp -= method; }
    public void RegisterMouseUp(MouseNamDelegate method)           { mouseUp += method; }
    public void UnregisterMouseUp(MouseNamDelegate method)         { mouseUp -= method; }
    public void RegisterMouseMove(MousePosDelegate method)         { mousePosMove += method; }
    public void UnregisterMouseMove(MousePosDelegate method)       { mousePosMove -= method; }
    public void RegisterMouseMove(MouseRayDelegate method)         { mouseRayMove += method; }
    public void UnregisterMouseMove(MouseRayDelegate method)       { mouseRayMove -= method; }
    #endregion
}
