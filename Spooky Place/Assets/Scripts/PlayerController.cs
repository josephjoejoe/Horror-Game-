using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float mouseSensitivity;

    RaycastHit hit;
    public Vector3 cubeSize;
    public float groundCheckDistance;

    public Rigidbody RB;

    public Camera eyes;
    private float xRotation; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (walkSpeed > 0) 
        {
            Vector3 vel = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                vel += transform.forward;
            }

            if (Input.GetKey(KeyCode.A))
            {
                vel -= transform.right;
            }

            if (Input.GetKey(KeyCode.S))
            {
                vel -= transform.forward;
            }

            if (Input.GetKey(KeyCode.D))
            {
                vel += transform.right;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                walkSpeed = runSpeed;
            }
            else
            {
                walkSpeed = 2;
            }

                vel = vel.normalized * walkSpeed;

            if (Input.GetKey(KeyCode.Space) && isGrounded())
            {
                vel.y = jumpForce;
            }
            else
            {
                vel.y = RB.linearVelocity.y;
            }

            RB.linearVelocity = vel;
        }
        // if my mouse goes left/right my body moves left/right
        float xRot = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, xRot, 0);
        // if my mouse goes up/down my aim moves up/down (not the body)
        float yRot = -Input.GetAxis("Mouse Y") * mouseSensitivity;
        eyes.transform.Rotate(yRot, 0, 0);

        // Horizontal body rotation
        transform.Rotate(0, xRot, 0);

        // Vertical camera rotation with clamp
        xRotation += yRot;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        eyes.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public bool isGrounded()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckDistance, cubeSize);
    }
}
