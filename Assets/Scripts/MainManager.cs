using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public Color TeamColor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    [Serializable]
    class UserData
    {
        public Color TeamColor;
    }

    public void SaveData()
    {
        var userData = new UserData() { TeamColor = TeamColor };
        var jsonData = JsonUtility.ToJson(userData);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "userData.json"), jsonData);
    }

    public void LoadData()
    {
        var userDataFile = Path.Combine(Application.persistentDataPath, "userData.json");
        if (File.Exists(userDataFile))
        {
            var jsonData = File.ReadAllText(userDataFile);
            var userData = JsonUtility.FromJson<UserData>(jsonData);
            TeamColor = userData.TeamColor;
        }
    }
}
