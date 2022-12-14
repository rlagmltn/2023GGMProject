using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance
    {
        get
        {
            Init();
            return s_instance;
        }
    }
    #region CORE

    // 변수
    private UIManager _uiManager = new UIManager();
    private InputManager _inputManager = new InputManager();
    private SoundManager _soundManager = new SoundManager();

    // 프로퍼티 
    public static UIManager UIManager { get { return Instance._uiManager; } }
    public static InputManager InputManager { get { return Instance._inputManager; } }
    public static SoundManager SoundManager { get { return Instance._soundManager; } }
    #endregion

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        //_inputManager.OnUpdate();
    }
    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._soundManager.Init();
        }
    }
}
