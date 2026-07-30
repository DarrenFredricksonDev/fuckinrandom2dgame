using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using Photon.Pun;

public class PlayerRaycast : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float rayDistance = 10f;
    public float force = 100f;
    public float directionX = 0f;
    public LayerMask platformLayer;
    public bool started = false;
    public GameObject vaporizeParticles;
    void FixedUpdate()
    {
        bool checkLateGame = GameLogic.isLateGame;
        Vector2 forceDirection = new Vector2(directionX, 1f);
        Vector2 position = (Vector2)transform.position + Vector2.up * 5f;
        RaycastHit2D raycastedObject = Physics2D.Raycast(position, Vector2.down, rayDistance, platformLayer);
        Debug.DrawRay(position, Vector2.down * rayDistance, Color.red); ;
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
        Debug.Log("Waiting.");
        TeleportToNearestPlatform();
    }
    void Vaporize()
    {
        PhotonNetwork.Instantiate(vaporizeParticles.name, transform.position, Quaternion.identity);
        GetComponent<PlayerMovementLegacy>().health = 0f;
        PhotonNetwork.Destroy(gameObject);
    }
    void TeleportToNearestPlatform()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.up * (rayDistance + 10f);
        float circleRadius = 2000f;
        Collider2D[] circleHit = Physics2D.OverlapCircleAll(origin, circleRadius, platformLayer);
        if (circleHit == null || circleHit.Length == 0)
        {
            Debug.Log("No platform found.");
            return;
        }
        float bestDist = Mathf.Infinity;
        Vector2 bestPoint = origin;
        foreach (var hit in circleHit)
        {
            Vector2 closest = hit.ClosestPoint(origin);
            float d = (closest - origin).sqrMagnitude;
            if (d < bestDist)
            {
                bestDist = d;
                bestPoint = closest;
            }
        }
        if (bestDist == Mathf.Infinity)
        {
            Debug.Log("No platform found.");
            return;
        }
        rb.linearVelocity = Vector2.zero;
        transform.position = new Vector3(bestPoint.x, bestPoint.y + 2f, transform.position.z);
        Debug.Log("Teleported.");
        started = false; 
    }

}
