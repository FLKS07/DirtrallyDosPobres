using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public void ButtonStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ButtonStartForMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    public void Level1()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Nivel2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Nivel3");
    }
    public void Level4()
    {
        SceneManager.LoadScene("Nivel4");
    }
    public void ExitgGme()
    {
        Application.Quit();
    }
}

