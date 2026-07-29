using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField roomNameHost;
    public InputField roomNameJoin;
    public InputField userName;
   public void createRoom()
    {
        PhotonNetwork.NickName = userName.text;
        PhotonNetwork.CreateRoom(roomNameHost.text);
    }
    public void joinRoom()
    {
        PhotonNetwork.NickName = userName.text;
        PhotonNetwork.JoinRoom(roomNameJoin.text);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + PhotonNetwork.CurrentRoom.Name);
        PhotonNetwork.LoadLevel("GameLobby");

    }
    public void OnJoinRoomFailed()
    {
        Debug.LogError("Failed.");
    }
}
