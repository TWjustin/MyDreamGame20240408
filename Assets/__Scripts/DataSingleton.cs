using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSingleton  // 不用SO & mono
{
    public InventorySizeData inventorySizeData { get; } = Resources.Load<InventorySizeData>("DataForm/InventorySizeForm");
    
    #region Singleton

    private static DataSingleton instance;
    public static DataSingleton Instance 
    {
        get 
        {
            if (instance == null)
            {
                instance = new DataSingleton();
            }
            return instance;
        }
    }

    #endregion
}
