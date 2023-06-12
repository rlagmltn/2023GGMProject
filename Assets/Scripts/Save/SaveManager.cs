using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameData gameData;

    private string SAVE_PATH = "";
    private string PLAYER_SAVE_FILENAME = "/PlayerSave.txt";
    private string GAME_SAVE_FILENAME = "/GameSave.txt";

    private void Awake()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        // Application.dataPath
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        PlayerDataLoad();
        GameDataLoad();
    }

    private void PlayerDataLoad()
    {
        if (File.Exists(SAVE_PATH + PLAYER_SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_PATH + PLAYER_SAVE_FILENAME);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
    }

    private void PlayerDataSave()
    {
        string jsonPlayer = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(SAVE_PATH + PLAYER_SAVE_FILENAME, jsonPlayer, System.Text.Encoding.UTF8);
    }

    private void GameDataLoad()
    {
        if (File.Exists(SAVE_PATH + GAME_SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_PATH + GAME_SAVE_FILENAME);
            gameData = JsonUtility.FromJson<GameData>(json);
        }
    }

    private void GameDataSave()
    {
        string jsonGame = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(SAVE_PATH + GAME_SAVE_FILENAME, jsonGame, System.Text.Encoding.UTF8);
    }

    private void OnApplicationQuit()
    {
        PlayerDataSave();
        GameDataSave();
    }
}
