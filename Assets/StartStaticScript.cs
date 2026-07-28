using UnityEngine;
using Photon.Pun;

public class StartStaticScript : MonoBehaviourPunCallbacks
{
    static bool isPressed = false;
   public static void LoadGame ()
    {
        isPressed = true;
        if (isPressed == true)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
