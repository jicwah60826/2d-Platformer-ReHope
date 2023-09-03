using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowContinue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string dataPath = Application.persistentDataPath;

        if (System.IO.File.Exists(Application.persistentDataPath + "/Dash.save") == false)
        {
            gameObject.SetActive(false);
        }
    }

}
