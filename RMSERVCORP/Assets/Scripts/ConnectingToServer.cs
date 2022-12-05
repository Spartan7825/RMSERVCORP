using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectingToServer : MonoBehaviourPunCallbacks
{
    public GameObject txt;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        //SceneManager.LoadScene("Lobby");
        txt.GetComponent<UnityEngine.UI.Text>().text = "Connected";
        panel.gameObject.SetActive(false);
    }
}
