using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Shooting : MonoBehaviourPunCallbacks
{
    public Text txt;
    public float damaga = 10f;
    public float range = 100f;

    public float firerate = 15;

    private float nextTimetofire = 0f;

    public ParticleSystem muzzelflash;
    public GameObject impactEffect;

    public Camera fpsCam;

    protected Animator animator;

    public float currentBullets = 30;

    public bool reloading = false;

    public bool startFire = false;


    //PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        //pv = this.GetComponent<PhotonView>();
        animator = this.GetComponent<Animator>();
        fpsCam = FindObjectOfType<Camera>();
        muzzelflash = transform.GetChild(29).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(5).GetChild(0).GetChild(15).GetChild(0).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startFire) 
        {
            shoot();
        }


    }
    public void SelectstartFire() 
    {
        startFire = true;
       // this.GetComponent<PlayerController>().CanMove = false;
    }
    public void DeselectstartFire() 
    {
        startFire = false;
        animator.SetLayerWeight(1, 0);
       /// this.GetComponent<PlayerController>().CanMove = true;
    }

    public void shoot()
    {
        
        if (currentBullets > 0 && reloading == false)
        {
           

            if ( Time.time >= nextTimetofire)
            {
                Debug.Log("shoot");
                if (photonView.IsMine)
                {
                    currentBullets = currentBullets - 1;
                    txt.text = currentBullets.ToString();
                    animator.SetLayerWeight(1, 1);
                    nextTimetofire = Time.time + 1f / firerate;
                    photonView.RPC("Shoot", RpcTarget.All);
                    // Shoot();

                }
            }
        }
        else
        {
            reloading = true;
            animator.SetBool("reload", true);
            StartCoroutine(ExecuteAfterTime(1.5f));
        }
/*
        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetLayerWeight(1, 0);
        }
*/


    }


    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        currentBullets = 30;
        reloading = false;
        animator.SetBool("reload", false);
       
        // Code to execute after the delay
    }
   


    [PunRPC]
    void Shoot() 
    {
        if (photonView.IsMine)
        {
            muzzelflash.Play();
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {


                Damage target = hit.transform.GetComponent<Damage>();
                //Debug.Log();
                if (target != null && !target.GetComponent<PhotonView>().IsMine)
                {
                    target.GetComponent<PhotonView>().RPC("OnDamage", RpcTarget.All, 10f, photonView.ViewID);
                    //target.OnDamage(10);
                }
                if (photonView.IsMine)
                {
                    GameObject impactGO = PhotonNetwork.Instantiate(impactEffect.name, hit.point, Quaternion.LookRotation(hit.normal));
                    impactGO.transform.parent = hit.transform;
                    Destroy(impactGO, 2f);
                }
            }

        }
        else
        {
            muzzelflash.Play();

        }
    }
}
