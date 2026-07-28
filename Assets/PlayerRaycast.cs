using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

public class PlayerRaycast : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float rayDistance = 10f;
    public float force = 100f;
    public float directionX = 0f;
    public LayerMask platformLayer;
    public bool started = false;
    void FixedUpdate()
    {
        bool checkLateGame = GameLogic.isLateGame;
        Vector2 forceDirection = new Vector2(directionX, 1f);
        Vector2 position = new Vector2 (transform.position.x, transform.position.y + 5f);
        RaycastHit2D raycastedObject = Physics2D.Raycast(position, Vector2.down, rayDistance);
        Debug.DrawRay(transform.position, Vector2.down * rayDistance, Color.red);
        if (raycastedObject.collider == null)
        {
            if (checkLateGame == false && started == false)
            {
                Debug.Log("Lategame false.");
                rb.AddForce(forceDirection.normalized * force);
                Debug.Log("Force added.");
                started = true;
                StartCoroutine(WaitToTeleport());
            }
            else if (checkLateGame == true)
            {
                Debug.Log("Lategame true.");
                Vaporize();
            }
        }
    }
    IEnumerator WaitToTeleport()
    {
        yield return new WaitForSeconds(2f);
        started = false;
        Debug.Log("Waiting.");
        TeleportToNearestPlatform();
    }
    void Vaporize()
    {
        GetComponent<PlayerMovementLegacy>().health = 0f;
        Destroy(gameObject);
    }
    void TeleportToNearestPlatform()
    {
        Vector2 findDirection1 = new Vector2(0f, 1f);
        Vector2 findDirection2 = new Vector2(1f, 1f);
        Vector2 findDirection3 = new Vector2(-1f, 1f);
        Vector2 findDirection4 = new Vector2(-0.5f, 1f);
        Vector2 findDirection5 = new Vector2(0.5f, 1f);
        Debug.Log("Teleport function started");
        Vector2 origin = (Vector2)transform.position + Vector2.up * (rayDistance + 10f);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, rayDistance + 2000f, platformLayer);
        Debug.DrawRay(origin, Vector2.up * (rayDistance + 2000f), Color.green, 2f);
        Debug.Log(origin);
        if (hit.collider != null)
        {
            Vector2 target = hit.point;
            rb.linearVelocity = Vector2.zero; // stop current motion
            transform.position = new Vector3(target.x, target.y + 2f, transform.position.z);
            Debug.Log("Teleported.");
        }
    }
}