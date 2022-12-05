using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int currentLevel;

    public static MainMenu instance { get; private set; }
    public void Awake()
    {
        instance = this;
    }
    public int GetCurrentLevel() {

        return currentLevel;
    }
    public void SetCurrrentLevel(int level) 
    {
        currentLevel = level;
    
    }

    public void Scene1()
    {
        SceneManager.LoadScene("GameLevel");
    }
    public void QuitGame() 
    {
        Application.Quit();
    }

    public void Level1() {

        currentLevel = 1;
    }
    public void Level2()
    {
        currentLevel = 2;

    }
    public void Level3()
    {
        currentLevel = 3;

    }
    public void Level4()
    {
        currentLevel = 4;

    }
    public void Level5()
    {
        currentLevel = 5;

    }
    public void Level6()
    {
        currentLevel = 6;

    }
    public void Level7()
    {
        currentLevel = 7;

    }
    public void Level8()
    {
        currentLevel = 8;

    }
    public void Level9()
    {
        currentLevel = 9;

    }
    public void Level10()
    {
        currentLevel = 10;

    }


}
