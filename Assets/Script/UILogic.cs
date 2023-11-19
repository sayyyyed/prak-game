using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{
    public GameObject panelmainmen;

    public GameObject paneloptions;
    public void bukaOptions()
    {
        panelmainmen.SetActive(false);
        paneloptions.SetActive(true);
    }
    public void bukaSceneStart()
    {
        SceneManager.LoadScene("StartGame");
    }
}
