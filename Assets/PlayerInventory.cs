using UnityEngine;
using Photon.Pun;
using System;

public class PlayerInventory : MonoBehaviour
{
    public GameObject AcidPopPrefab;
    private GameObject[] inventory = new GameObject[3];

    public void OnPickedUp(GameObject pickup)
    {
        int freeIndex = Array.IndexOf(inventory, null);
        if (freeIndex == -1)
        {
            Debug.Log("Inventory full");
            return;
        }

        GameObject spawned = PhotonNetwork.Instantiate(AcidPopPrefab.name, transform.position, Quaternion.identity);
        inventory[freeIndex] = spawned;
        pickup.SendMessage("PickupSuccess", SendMessageOptions.DontRequireReceiver);
        Destroy(pickup);
    }
}
