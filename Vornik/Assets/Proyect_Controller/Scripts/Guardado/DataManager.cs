using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static readonly string SLOT = "slot";

    public static void AutoSaveData( MonoBehaviour component)
    {
        string jsonFormat = JsonUtility.ToJson(component);
        PlayerPrefs.SetString(SLOT+1 ,jsonFormat);
        
    }

    public static void LoadData(MonoBehaviour component)
    {
        try
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(SLOT + 1), component);
        }
        catch
        {
            Debug.Log("No hay datos");
        }
        
    }
}
