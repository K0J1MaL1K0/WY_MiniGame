using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour,ISaveable
{
    private int gameWeek;
    private Dictionary<string, bool> miniGameStateDict = new Dictionary<string, bool>();



    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.gameWeek = this.gameWeek;
        return saveData;
    }

    public void RestoreGameDate(GameSaveData saveDate)
    {
        this.gameWeek = saveDate.gameWeek;
    }

    public void SaveableRegister()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        ISaveable saveble = this;
        saveble.SaveableRegister();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
