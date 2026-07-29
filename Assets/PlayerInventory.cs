using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool slot1Filled = false;
    public bool slot2Filled = false;
    public bool slot3Filled = false;
    public GameObject slot1;
    public GameObject slot2;
    public GameObject slot3;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (slot1Filled || slot2Filled || slot3Filled == false)
            {
                //pick up item change a slot to filled
            }
            else
            {
                // do nothing
                Debug.Log("Full Inventory.");
            }
        }
    }
}
