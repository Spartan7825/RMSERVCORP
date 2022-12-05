using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnPlayers : MonoBehaviour
{
    
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minz;
    public float maxz;
    public Text txt;
    public Text txtt;
    public Text kull;
    

    public Camera c;

    public Joystick joystick;

    public FixedTouchField touchField;

    public bool startFire = false;

    [SerializeField] public Animator DefeatAnimController;
    public Image healthBar;
    public Canvas can;
    public Transformation trans;

    public GameObject spawnedPlayer;


    public GameObject shield;


    public Image progressBar;
    public Button shieldbutton;


    void Start()
    {

        if (MainMenu.instance.GetCurrentLevel() > 990)
        {

            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0.7f, Random.Range(minz, maxz));
            spawnedPlayer = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

            if (MainMenu.instance.GetCurrentLevel() == 999)
            {
                c.GetComponent<CameraScript>().target = spawnedPlayer.transform;
                spawnedPlayer.GetComponent<PlayerController>().joystick = joystick;
                spawnedPlayer.GetComponent<TouchLook>().joystick = joystick;
                spawnedPlayer.GetComponent<TouchLook>().touchField = touchField;

                spawnedPlayer.GetComponent<Damage>().DefeatAnimController = DefeatAnimController;
                spawnedPlayer.GetComponent<Damage>().healthBar = healthBar;
                spawnedPlayer.GetComponent<Damage>().can = can;
                spawnedPlayer.GetComponent<Damage>().trans = trans;
                spawnedPlayer.GetComponent<Damage>().txtt = txtt;
                spawnedPlayer.GetComponent<Shooting>().txt = txt;
                spawnedPlayer.GetComponent<Damage>().sp = this;
                spawnedPlayer.GetComponent<Damage>().klls = kull;
                spawnedPlayer.GetComponent<Damage>().shield = shield;
                spawnedPlayer.GetComponent<Damage>().progressBar = progressBar;
                spawnedPlayer.GetComponent<Damage>().shieldbutton = shieldbutton;


            }
        }
    }

    public void SpshieldM()
    {
        Damage d = spawnedPlayer.GetComponent<Damage>();
        d.GetComponent<PhotonView>().RPC("shieldM", RpcTarget.All);
    }


    public void SelectstartFire()
    {
        startFire = true;
        spawnedPlayer.GetComponent<Shooting>().SelectstartFire();
    }
    public void DeselectstartFire()
    {
        startFire = false;
        spawnedPlayer.GetComponent<Shooting>().DeselectstartFire();
    }




    public void respawnPlayer(GameObject player)
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 0.7f, Random.Range(minz, maxz));
        player.transform.position = randomPosition;
    }
    public void disconnectfromgame() 
    {
        StartCoroutine(DisconnectAndLoad());
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
            yield return null;
        //Debug.Log("asdfasdf");
        SceneManager.LoadScene("MainMenu");




    }

}

