using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int gemCount;
    public int deathCount;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SaveData theSave = SaveSystem.instance.activeSave;

        gemCount = theSave.gemCount;
        deathCount = theSave.deathCount;
    }

    public void UpdateGemCount()
    {
        
    }

    public void UpdateDeathCount()
    {
        deathCount++;
    }
}
