using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Global
{
    public static Dictionary<ePrefabs, GameObject> prefabsDic;
    public static Dictionary<string, Sprite> spritesDic;

    public static Vector2 refrenceResolution;
    public static Image blackPannel;

    // 현재 게임상태
    public static eGameState gameState = eGameState.None;
    public static eSceneName sceneName = eSceneName.None;

    // 메인카메라
    public static Transform gMainCamTrm;
    public static Camera mainCam;
    public static Transform Map;
}

public enum ePrefabs
{
    None = -1,
    MainCamera,

    HEROS = 1000,
    HeroMan,
    HeroGirl,

    MANAGERS = 2000,
    GameManager,
    PoolManager,
    SpawnManager,

    UI = 3000,
    UIRoot,
    UIRootLoading,
    UIRootTitle,
    UIRootGame,
    UIRootSelect,
    UIRootMap,
    UIRootInGame,

    Object = 4000,
    EmptyObj,
    StageObj,
    EnemyObj1,
    EnemyObj2,
    Button,
    Map,
    Enemy,
    RedHitBox,
}

public enum eSceneName
{
    None = -1,
    Boss,
    Game,
    Loading,
    Main,
    Select,
    Title,
    Map,
    InGame,
}

public class GameMapClass
{
    public static Transform gMap;
}

public class GameSceneClass
{
    public static UIRoot gUiRoot;

    public static MGGame gMGGame;
    public static MGPool gMGPool;
}

public enum eGameState
{
    None,
    Playing,
    Paused,
}

public enum eStageState
{
    None,
    Event,
    Shop,
    Battle,
}
