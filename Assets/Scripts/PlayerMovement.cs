using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;

    public float moveForce = 500f;

    public float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;

    public Transform cam;

    private bool isGrounded = true;

    private float jumpForce = 10f;

    private float jumpAmount = 1f;

    private Vector3 playerPos;

    private ContactPoint[] contacts = new ContactPoint[10];

    private CapsuleCollider capsuleCollider;

    public AudioSource pickable;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void LateUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            playerRB.AddForce(moveDir.normalized * moveForce * Time.deltaTime);
        }
        Jump();
    }
    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerPos = new Vector3(0, jumpAmount, 0);

            playerRB.AddForce(playerPos * jumpForce, ForceMode.Impulse);

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            collision.GetContacts(contacts);
            foreach (var contact in contacts)
            {
                //Debug.Log(Vector2.Distance(capsuleCollider.bounds.min, contact.point));
                if (Vector2.Distance(capsuleCollider.bounds.min,contact.point) < 2)
                {
                    isGrounded = true;
                }
            }
        }
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Pickable"))
        {
            GameManager.instance.playerScore++;
            GameManager.instance.pickableNumber--;

            Destroy(other.gameObject);
            pickable.Play();

        }
    }
}
