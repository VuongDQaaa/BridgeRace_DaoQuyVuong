using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private string levelKey = "currentLevel";

    void Update()
    {
        int currentLevel = GetCurrentLevel();
        if (currentLevel > 3)
        {
            SaveCurrentLevel(1);
        }
    }

    public void SaveCurrentLevel(int level)
    {
        PlayerPrefs.SetInt(levelKey, level);
        PlayerPrefs.Save();
        Debug.Log("level saved:" + GetCurrentLevel());
    }

    public int GetCurrentLevel()
    {
        if (PlayerPrefs.HasKey(levelKey))
        {
            return PlayerPrefs.GetInt(levelKey);
        }
        else
        {
            return 1;
        }
    }
}
