using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UImanager : MonoBehaviour
{
    public GameObject panelMainMenu;
    public GameObject panelOption;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void bukamenu()
    {
        panelMainMenu.SetActive(true);
        panelOption.SetActive(false);
    }

    public void bukaoption()
    {
        panelMainMenu.SetActive(false);
        panelOption.SetActive(true);
    }

    public void startGame()
    {
        SceneManager.LoadScene("ingame");
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Ini beruang");
    }
}
