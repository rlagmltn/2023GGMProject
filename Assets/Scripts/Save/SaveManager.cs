using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    [SerializeField] private PlayerData playerData;
    public PlayerData PlayerData
    {
        get
        {
            return playerData;
        }
        set
        {
            playerData = value;
        }
    }

    [SerializeField] private GameData gameData;
    public GameData GameData
    { get 
        {
            return gameData;
        } 
        set 
        { 
            gameData = value;
        } 
    }

    [SerializeField] private TutoData tutoData;
    public TutoData TutoData
    {
        get
        {
            return tutoData;
        }
        set
        {
            tutoData = value;
        }
    }

    private string SAVE_PATH = "";
    private string PLAYER_SAVE_FILENAME = "/PlayerSave.txt";
    private string GAME_SAVE_FILENAME = "/GameSave.txt";
    private string TUTO_SAVE_FILENAME = "/TutoSave.txt";

    private void Awake()
    {
        //SAVE_PATH = Application.dataPath + "/Save";
        SAVE_PATH = Application.persistentDataPath + "/Save";

        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        PlayerDataLoad();
        GameDataLoad();
        TutoDataLoad();
        DontDestroyOnLoad(this);

        ArInventoryManager.Instance.Init();
    }

    private void PlayerDataLoad()
    {
        if (File.Exists(SAVE_PATH + PLAYER_SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_PATH + PLAYER_SAVE_FILENAME);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
    }

    private void GameDataLoad()
    {
        if (File.Exists(SAVE_PATH + GAME_SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_PATH + GAME_SAVE_FILENAME);
            gameData = JsonUtility.FromJson<GameData>(json);
        }
    }

    private void TutoDataLoad()
    {
        if (File.Exists(SAVE_PATH + TUTO_SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_PATH + TUTO_SAVE_FILENAME);
            tutoData = JsonUtility.FromJson<TutoData>(json);
        }
    }

    public void PlayerDataSave()
    {
        string jsonPlayer = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(SAVE_PATH + PLAYER_SAVE_FILENAME, jsonPlayer, System.Text.Encoding.UTF8);
        Debug.Log("¿˙¿Â...");
    }

    public void GameDataSave()
    {
        string jsonGame = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(SAVE_PATH + GAME_SAVE_FILENAME, jsonGame, System.Text.Encoding.UTF8);
    }

    public void TutoDataSave()
    {
        string jsonTuto = JsonUtility.ToJson(tutoData, true);
        File.WriteAllText(SAVE_PATH + TUTO_SAVE_FILENAME, jsonTuto, System.Text.Encoding.UTF8);
    }

    public void AllDataClear()
    {
        playerData = new PlayerData();
        gameData = new GameData();
        tutoData = new TutoData();

        PlayerDataSave();
        GameDataSave();
        TutoDataSave();
    }

    private void OnApplicationQuit()
    {
        PlayerDataSave();
        GameDataSave();
        TutoDataSave();
    }
}
