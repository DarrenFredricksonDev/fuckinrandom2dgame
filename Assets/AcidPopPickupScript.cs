using UnityEngine;

public class AcidPopPickupScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SendMessage("OnPickedUp", SendMessageOptions.DontRequireReceiver);
    }
    public void PickupSuccess()
    {
        Destroy(gameObject);
    }
}
