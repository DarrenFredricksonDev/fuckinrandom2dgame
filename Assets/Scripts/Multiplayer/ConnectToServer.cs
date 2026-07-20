using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToSever : MonoBehaviourPunCallbacks
{
    void Start()
    {
       PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        SceneManager.LoadScene("Main Menu");
    }
}
