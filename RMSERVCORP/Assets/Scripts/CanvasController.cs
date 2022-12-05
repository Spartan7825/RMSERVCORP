using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField] 
    public Animator levelCompletedAnimController;
    public Canvas can;
    public GameObject fire;
    public GameObject enemy;
    public Transformation trans;

    public GameObject singlePlayerRobot;
    public GameObject singlePlayerTruck;

    public bool skipped;
    public GameObject levels;

    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log(MainMenu.instance.GetCurrentLevel());

        can = GetComponent<Canvas>();
        //can.transform.GetChild(0).gameObject.SetActive(false); //Camera Touchpad
        can.transform.GetChild(1).gameObject.SetActive(false); //CinematicBars
        can.transform.GetChild(5).gameObject.SetActive(false); //Minimap
        can.transform.GetChild(6).gameObject.SetActive(false);
        can.transform.GetChild(7).gameObject.SetActive(false);
        can.transform.GetChild(8).gameObject.SetActive(false);
        can.transform.GetChild(9).gameObject.SetActive(false);
        can.transform.GetChild(11).gameObject.SetActive(false);
        can.transform.GetChild(12).gameObject.SetActive(false);
        can.transform.GetChild(13).gameObject.SetActive(false);
        can.transform.GetChild(14).gameObject.SetActive(false);
        can.transform.GetChild(15).gameObject.SetActive(false);
        can.transform.GetChild(16).gameObject.SetActive(false);

        StartCoroutine(IntroDelay(96));

  
        foreach (Transform child in levels.transform) 
        {
            for (int i = 0; i < child.transform.childCount; i++)
            {
                if (i == 0)
                {
                    child.transform.GetChild(i).gameObject.SetActive(true);
                }
                else { child.transform.GetChild(i).gameObject.SetActive(false); }
                
            }

        }
        if (MainMenu.instance.GetCurrentLevel() == 999)
        {
            singlePlayerRobot.SetActive(false);
            singlePlayerTruck.SetActive(false);
            can.transform.GetChild(0).gameObject.SetActive(true);
            can.transform.GetChild(1).gameObject.SetActive(false);
            can.transform.GetChild(2).gameObject.SetActive(false);
            can.transform.GetChild(3).gameObject.SetActive(false);
            can.transform.GetChild(6).gameObject.SetActive(true);
            can.transform.GetChild(5).gameObject.SetActive(true);
            can.transform.GetChild(7).gameObject.SetActive(false);
            can.transform.GetChild(8).gameObject.SetActive(true);
            can.transform.GetChild(9).gameObject.SetActive(true);
            can.transform.GetChild(11).gameObject.SetActive(true);
            can.transform.GetChild(12).gameObject.SetActive(true);
            can.transform.GetChild(13).gameObject.SetActive(true);
            can.transform.GetChild(14).gameObject.SetActive(true);
            can.transform.GetChild(15).gameObject.SetActive(true);
            can.transform.GetChild(16).gameObject.SetActive(true);
            can.transform.GetChild(8).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            can.transform.GetChild(8).gameObject.transform.GetChild(1).gameObject.SetActive(false);
            can.transform.GetChild(8).gameObject.transform.GetChild(2).gameObject.SetActive(false);
            can.transform.GetChild(8).gameObject.transform.GetChild(3).gameObject.SetActive(false);
            can.transform.GetChild(8).gameObject.transform.GetChild(4).gameObject.SetActive(false);
            can.transform.GetChild(8).gameObject.transform.GetChild(5).gameObject.SetActive(true);
            can.transform.GetChild(8).gameObject.transform.GetChild(6).gameObject.SetActive(true);
            Camera.main.GetComponent<Animation>().Stop("IntroCinematic");

            //can.transform.GetChild(8).gameObject.transform.GetChild(6).gameObject.SetActive(false);


        }
        else
        {
            can.transform.GetChild(8).gameObject.transform.GetChild(6).gameObject.SetActive(false);

            foreach (Transform child in levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1))
            {
                child.gameObject.SetActive(true);

            }
            levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1).transform.GetChild(3).gameObject.GetComponent<EnemyRobotColorController>().changeMaterial();

            levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1).transform.GetChild(3).gameObject.GetComponent<EnemyController>().damage = 20 + (MainMenu.instance.GetCurrentLevel() * 2);

            levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1).transform.GetChild(3).gameObject.GetComponent<Damage>().npcHealth = 30 + (MainMenu.instance.GetCurrentLevel() * 5);
            levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1).transform.GetChild(3).gameObject.GetComponent<Damage>().npcMaxHealth = 30 + (MainMenu.instance.GetCurrentLevel() * 5);

            levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1).transform.GetChild(3).gameObject.GetComponent<EnemyController>().TargetForplayer = levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1).transform.GetChild(3).gameObject.transform.GetChild(2).gameObject;
            levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1).transform.GetChild(3).gameObject.GetComponent<EnemyController>().TargetFireForPlayer = levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1).transform.GetChild(1).gameObject;

            levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1).transform.GetChild(3).gameObject.GetComponent<EnemyController>().rsForHM = levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1).transform.GetChild(0).GetComponent<RebuildingScript>();


            enemy = levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1).transform.GetChild(3).gameObject;
            fire = levels.transform.GetChild(MainMenu.instance.GetCurrentLevel() - 1).transform.GetChild(1).gameObject;
        }
            
       
                   


        if (MainMenu.instance.GetCurrentLevel() > 1 && MainMenu.instance.GetCurrentLevel() < 990) 
        {
            Camera.main.GetComponent<Animation>().Stop("IntroCinematic");
            can.transform.GetChild(1).gameObject.SetActive(false);
            can.transform.GetChild(2).gameObject.SetActive(false);
            can.transform.GetChild(5).gameObject.SetActive(true);
            //can.transform.GetChild(5).gameObject.SetActive(true);
            can.transform.GetChild(7).gameObject.SetActive(true);
            can.transform.GetChild(8).gameObject.SetActive(true);
            skipped = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MainMenu.instance.GetCurrentLevel() < 990) 
            {
                if (can.transform.GetChild(17).gameObject.activeSelf)
                {
                    can.transform.GetChild(17).gameObject.SetActive(false);
                }
                else
                {
                    can.transform.GetChild(17).gameObject.SetActive(true);

                }
                //single player code here :)
            } 
            else 
            {
                //this fixed
                if (can.transform.GetChild(10).gameObject.activeSelf)
                {
                    can.transform.GetChild(10).gameObject.SetActive(false);
                }
                else
                {
                    can.transform.GetChild(10).gameObject.SetActive(true);

                }
            }

           
        }
        

/*            if (Input.GetKeyDown("a"))
        {
            if (!skipped && !(MainMenu.instance.GetCurrentLevel() == 999))
            {
               
                can.transform.GetChild(1).gameObject.SetActive(false);
                can.transform.GetChild(2).gameObject.SetActive(false);
                can.transform.GetChild(5).gameObject.SetActive(true);
                //can.transform.GetChild(5).gameObject.SetActive(true);
                can.transform.GetChild(7).gameObject.SetActive(true);
                can.transform.GetChild(8).gameObject.SetActive(true);
                skipped = true;
            }

        }
        if (Input.touchCount > 0 )
        {
            if (!skipped && !(MainMenu.instance.GetCurrentLevel() == 999))
            {
                can.transform.GetChild(1).gameObject.SetActive(false);
                can.transform.GetChild(2).gameObject.SetActive(false);
                can.transform.GetChild(5).gameObject.SetActive(true);
                //can.transform.GetChild(5).gameObject.SetActive(true);
                can.transform.GetChild(7).gameObject.SetActive(true);
                can.transform.GetChild(8).gameObject.SetActive(true);
                skipped = true;
            }
        }*/

        if (!(fire||enemy)) 
        {
            levelCompletedAnimController.SetBool("Trigger", true);
            can.transform.GetChild(4).gameObject.SetActive(true);

            can.transform.GetChild(0).gameObject.SetActive(false);
            can.transform.GetChild(1).gameObject.SetActive(false);
            can.transform.GetChild(2).gameObject.SetActive(false);
            can.transform.GetChild(3).gameObject.SetActive(false);
            can.transform.GetChild(5).gameObject.SetActive(false);
            can.transform.GetChild(6).gameObject.SetActive(false);
            can.transform.GetChild(7).gameObject.SetActive(false);
            can.transform.GetChild(8).gameObject.SetActive(false);
            StartCoroutine(DelayBeforeSwitch(13));
        }

    }
    public void DeathDelay() 
    {
        StartCoroutine(DelayBeforeDeath(8));
    }

    public IEnumerator DelayBeforeDeath(int time)
    {
        //transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("MainMenu");
        trans.RestartValues();

    }
    public IEnumerator DelayBeforeSwitch(int time)
    {
        yield return new WaitForSeconds(time);
        if (MainMenu.instance.GetCurrentLevel() < 10) 
        {
            SaveManager.instance.levelsUnlocked[MainMenu.instance.GetCurrentLevel()] = true;
            SaveManager.instance.Save();
        }
        SaveManager.instance.money += 100;
        SaveManager.instance.Save();
        SceneManager.LoadScene("MainMenu");
        trans.RestartValues();

    }

    public IEnumerator IntroDelay(int time) 
    {
        yield return new WaitForSeconds(time);
        if (!skipped && !(MainMenu.instance.GetCurrentLevel() == 999))
        {
            skipped = true;
            can.transform.GetChild(1).gameObject.SetActive(false);
            can.transform.GetChild(2).gameObject.SetActive(false);
            can.transform.GetChild(5).gameObject.SetActive(true);
            //can.transform.GetChild(5).gameObject.SetActive(true);
            can.transform.GetChild(7).gameObject.SetActive(true);
            can.transform.GetChild(8).gameObject.SetActive(true);
        }

    }
}
