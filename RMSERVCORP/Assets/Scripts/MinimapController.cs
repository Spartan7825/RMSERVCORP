using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{

    public Transform truck;
    public Transform robot;
    public Transform player;
    public SpawnPlayers sp;



    public bool toggle;
    public Transformation trans;


    public void Start()
    {
        if (MainMenu.instance.GetCurrentLevel() < 990)
        {
            player = truck;
        }
        else
        {
            player = sp.spawnedPlayer.transform;

        }
    }

    public void togglePlayer()
    {
        toggle = trans.GetIsTruck();
        if (toggle)
        {
            player = truck;
            toggle = false;
        
        }
        else {
            player = robot;
            toggle = true;
        }
    
    }

    public void LateUpdate()
    {
        Vector3 position = player.position;
        position.y = transform.position.y;
        transform.position = position;

        transform.rotation = Quaternion.Euler(90f,player.eulerAngles.y,0f);
    }
    public void Update()
    {



    }

}
