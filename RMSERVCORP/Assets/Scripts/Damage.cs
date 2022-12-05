using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class Damage : MonoBehaviourPunCallbacks
{
    public Text txtt;
    [SerializeField] public Animator DefeatAnimController;
    public  static float currentHealth;
    public static float maxHealth;
    public Image healthBar;
    public Canvas can;
    public Transformation trans;
    public int kills;
    public SpawnPlayers sp;
    public Text klls;

    public GameObject shield;


    public float npcHealth;
    public float npcMaxHealth;

    public float timeRemaining = 0;
    public Image progressBar;
    public Button shieldbutton;

    public bool quit = false;


    public bool shieldActive = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = 100f;
        maxHealth = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainMenu.instance.GetCurrentLevel() == 999)
        {
            if (photonView.IsMine)
            {


                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;

                    progressBar.fillAmount = timeRemaining / 20f;


                }
                else
                {

                    shieldbutton.enabled = true;
                }

            }
        }

    }


    public void npcDamage(float damage)
    {
        if (npcHealth < 1)
        {

            Destroy(gameObject);

            //  Debug.Log(photonView.ViewID);
        }
        else
        {
            npcHealth -= damage;
            healthBar.fillAmount = npcHealth / npcMaxHealth;

        }

    }



    public void singlePlayerDamage(float damage)
    {
        if (!ShieldEffectController.instance.followRobot)
        {
            if (currentHealth < 1)
            {

                DefeatAnimController.SetBool("Trigger", true);
                can.transform.GetChild(3).gameObject.SetActive(true);

                can.transform.GetChild(0).gameObject.SetActive(false);
                can.transform.GetChild(1).gameObject.SetActive(false);
                can.transform.GetChild(2).gameObject.SetActive(false);
                can.transform.GetChild(4).gameObject.SetActive(false);
                can.transform.GetChild(5).gameObject.SetActive(false);
                can.transform.GetChild(6).gameObject.SetActive(false);
                can.transform.GetChild(7).gameObject.SetActive(false);
                can.transform.GetChild(8).gameObject.SetActive(false);
                can.GetComponent<CanvasController>().DeathDelay();
                transform.gameObject.SetActive(false);

                //  Debug.Log(photonView.ViewID);
            }
            else
            {
                currentHealth -= damage;
                healthBar.fillAmount = currentHealth / 100;

            }
        }

    }


    IEnumerator delay(float time)
    {

        yield return new WaitForSeconds(time);

        GetComponent<PhotonView>().RPC("DisableShield", RpcTarget.All);



    }


    [PunRPC]
    public void DisableShield()
    {
        if (photonView.IsMine)
        {
            shieldActive = false;
            transform.GetChild(32).gameObject.SetActive(false);
        }
        else
        {
            shieldActive = false;
            transform.GetChild(32).gameObject.SetActive(false);

        }
    
    }

    public void cooldown()
    {
        timeRemaining = 20;
        if (photonView.IsMine)
        { 
        shieldbutton.enabled = false;
        }
    }

    [PunRPC]
    public void shieldM()
    {

        
        //GameObject shieldGO = PhotonNetwork.Instantiate(shield.name, transform.position + new Vector3(0, 1.3f, 0), transform.rotation);
        if (photonView.IsMine)
        {
            shieldActive = true;
            StartCoroutine(delay(10f));
            cooldown();
            transform.GetChild(32).gameObject.SetActive(true);
            /*
            Debug.Log(photonView.ViewID);
            shieldGO.transform.localScale = new Vector3(300f, 300f, 300f);
            shieldGO.transform.parent = transform;
            
            Destroy(shieldGO, 20f);
            */
        }

        else
        {
            shieldActive = true;
            StartCoroutine(delay(10f));
            cooldown();
            transform.GetChild(32).gameObject.SetActive(true);
            /*
            Debug.Log(photonView.ViewID);
            shieldGO.transform.localScale = new Vector3(300f, 300f, 300f);
            shieldGO.transform.parent = transform;

            Destroy(shieldGO, 20f);
            */

        }
        /*
        Vector3 newPos = truck.transform.position;
        newPos.y = truck.transform.position.y + 1;
        //newPos.x = truck.transform.position.x + 1;

        transform.position = newPos;

        transform.rotation = truck.transform.rotation;
        */

    }


    [PunRPC]
    public void OnDamage(float damage, int senderid)
    {


            healthUpdate(damage);
            
        //Debug.Log(info.photonView.ViewID);
        //Debug.Log(PhotonView.Find(senderid).transform.GetComponent<PhotonView>().ViewID);


        if (!ShieldEffectController.instance.followRobot)
        {
            if (photonView.IsMine)
            {

                if (!shieldActive)
                {
                    if (currentHealth < 1)
                    {
                        sp.respawnPlayer(this.gameObject);
                        Damage d = PhotonView.Find(senderid).transform.GetComponent<Damage>();
                        d.GetComponent<PhotonView>().RPC("updateKill", RpcTarget.All);
                        // updateKill(info);
                        transform.GetChild(31).gameObject.transform.GetChild(2).GetComponent<Image>().fillAmount = 1;
                        currentHealth = 100;
                        healthBar.fillAmount = currentHealth / 100;


                    }
                    else
                    {
                        currentHealth -= damage;
                        healthBar.fillAmount = currentHealth / 100;

                    }
                }

            }

            else
            {
                if (!shieldActive)
                {
                    if (currentHealth < 1)
                    {

                        //    updateKill(info);
                        transform.GetChild(31).gameObject.transform.GetChild(2).GetComponent<Image>().fillAmount = 1;
                        currentHealth = 100;
                        // healthBar.fillAmount = currentHealth / 100;
                    }
                    else
                    {
                        currentHealth -= damage;
                        // healthBar.fillAmount = currentHealth / 100;
                    }
                }

            }


        }

    }
    [PunRPC]
    public void updateKill()
    {


        if (photonView.IsMine)
        {
            kills = kills + 1;
            klls.text = kills.ToString();

        }
        else
        {
            kills = kills + 1;


        }
    }
    public void healthUpdate(float damage)
    {

        if (photonView.IsMine)
        {
        if (!shieldActive)
        {
            transform.GetChild(31).gameObject.transform.GetChild(2).GetComponent<Image>().fillAmount -= damage / 100;
            // Debug.Log(currentHealth);
        }
        }
        else
        {
            if (!shieldActive)
            {
                transform.GetChild(31).gameObject.transform.GetChild(2).GetComponent<Image>().fillAmount -= damage / 100;
                //Debug.Log(currentHealth);
            }
        }
    }
    public override void OnLeftRoom()
    {
        //Debug.Log("asdfasdf");
        PhotonNetwork.LoadLevel(0);
    }


}
