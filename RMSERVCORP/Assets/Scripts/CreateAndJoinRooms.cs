using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    private string input;
    private string inputt;

    public void ReadStringInput(string s) 
    {
        input = s;
    }

    public void CompareStringInput(string cs) 
    {
        inputt = cs;
    }
    public void CreateRoom() 
    {
        PhotonNetwork.CreateRoom(input);
    }

    public void JoinRoom()
    {
            PhotonNetwork.JoinRoom(inputt);

    }

    public override void OnJoinedRoom()
    {
        MainMenu.instance.SetCurrrentLevel(999);
        PhotonNetwork.LoadLevel("GameLevel");
    }

}
