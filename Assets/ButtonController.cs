using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    public GameObject menu;
    GameObject[] countries;
    public int gameScene = 1;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        countries = GameObject.FindGameObjectsWithTag("Country");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMenu()
    {
        menu.SetActive(true);
        foreach(GameObject count in countries)
        {
            count.GetComponent<Button>().enabled = false;
        }
    }

    public void HideMenu()
    {
        menu.SetActive(false);
        foreach (GameObject count in countries)
        {
            count.GetComponent<Button>().enabled = true;
        }
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(gameScene);
    }

}
