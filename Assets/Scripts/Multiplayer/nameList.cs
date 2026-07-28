using UnityEngine;
using Photon.Pun;
using TMPro;

public class nameList : MonoBehaviour
{
    public Transform content;
    public GameObject playerText;
    public float lastFrameCount = 0f;

    void Update()
    {
        if (PhotonNetwork.PlayerList.Length != lastFrameCount)
        {
            lastFrameCount = PhotonNetwork.PlayerList.Length;
            UpdatePlayerList();
        }
    }
    void UpdatePlayerList()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        } 
        foreach (var player in PhotonNetwork.PlayerList)
        {
            GameObject newText = Instantiate(playerText, content);
            TMP_Text text = newText.GetComponent<TMP_Text>();
            text.text = player.NickName;
        }
    }
}
