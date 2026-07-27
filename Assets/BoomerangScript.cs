using UnityEngine;
using Photon.Pun;
using System.Collections;
public class BoomerangScript : MonoBehaviour
{
    public float speed = 10f;
    public float returnDelay = 1f;
    private Vector3 startPosition;
    private bool isReturning = false;
    private PhotonView view;
    public int hits = 0;
    public float damage = 15f;
    public float multiplier = 1.5f;
    public float rotationSpeed = 360f;
    public Rigidbody2D rb;
    void Start()
    {
        
        if (!GetComponent<PhotonView>().IsMine)
        {
            Destroy(this);
        }
        transform.parent = GameObject.FindGameObjectWithTag("Player").transform;

    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            rb.AddForce(-transform.right * speed, ForceMode2D.Impulse);
            rb.AddTorque(rotationSpeed * Time.deltaTime, ForceMode2D.Impulse);
            transform.parent = null;
            StartCoroutine(wait());
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
            rb.AddTorque(rotationSpeed * Time.deltaTime, ForceMode2D.Impulse);
            transform.parent = null;
            StartCoroutine(wait());
        }
        else {transform.parent = GameObject.FindGameObjectWithTag("Player").transform; }
        if (isReturning == true)
        {
            Vector3 direction = (startPosition - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }
    public IEnumerator wait()
    {
        yield return new WaitForSeconds(returnDelay);
        returnBoomerang();
    }
    public void returnBoomerang()
    {
        isReturning = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovementLegacy player = collision.GetComponent<PlayerMovementLegacy>();
            if (player != null)
            {
                player.health -= damage;
                hits++;
                damage = ((float)speed * hits + 1f) * multiplier;
            }
        }
    }
}