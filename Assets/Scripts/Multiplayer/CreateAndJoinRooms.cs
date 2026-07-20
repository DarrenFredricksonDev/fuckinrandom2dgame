using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField roomNameHost;
    public InputField roomNameJoin;
   public void createRoom()
    {
        PhotonNetwork.CreateRoom(roomNameHost.text);
    }
    public void joinRoom()
    {
        PhotonNetwork.JoinRoom(roomNameJoin.text);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("Game");
    }
}
