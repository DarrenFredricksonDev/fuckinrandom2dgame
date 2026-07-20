using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementLegacy : MonoBehaviour
{
    public float moveForce = 200f;
    public float maxSpeed = 6f;
    public float jumpImpulse = 8f;
    public float jumpCooldown = 1f;
    public int AxisHorizontal = 0;
    public float forceMultiplier = 5f;
    public float gravityScale = 4f;
    PhotonView view;

    Rigidbody2D rb;
    float horiz = 0f;
    float lastJumpTime = -Mathf.Infinity;
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (view.IsMine)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                AxisHorizontal = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                AxisHorizontal = 1;
            }
            else
            {
                AxisHorizontal = 0;
            }

            horiz = Input.GetAxisRaw("Horizontal"); // -1..0..1

            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastJumpTime + jumpCooldown)
            {
                rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
                lastJumpTime = Time.time;
            }
        }
    }
    void FixedUpdate()
    {
        float dir = 0f;
        if (horiz == 1f) dir = 1f;
        else if (horiz == -1f) dir = -1f;

        rb.AddForce(new Vector2(dir * moveForce * Time.fixedDeltaTime, forceMultiplier), ForceMode2D.Force);

        Vector2 v = rb.linearVelocity;
        v.x = Mathf.Clamp(v.x, -maxSpeed, maxSpeed);
        rb.linearVelocity = v;
    }
}   