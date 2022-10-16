using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable 
{
    void SaveableRegister();

    GameSaveData GenerateSaveData();

    void RestoreGameDate(GameSaveData saveDate);

}




