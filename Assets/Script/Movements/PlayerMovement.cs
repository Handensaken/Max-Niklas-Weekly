using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    [Tooltip("Hur mycket rigidbody drag som spelaren ska få när den är på marken")]
    public float groundDrag;
    [Tooltip("Kraft upåt när man hoppar")]
    public float jumpForce;
    [Tooltip("Hur ofta man får hoppa")]
    public float jumpCooldown;
    [Tooltip("Multiplier för input som ges medans man är i luften")]
    public float airMultiplier;
    private bool readyToJump;
    [Tooltip("Value för hur snabbt spelaren roteras till rätt riktning/kamerans riktning när den rör på sig")]
    public float rotationSpeed;
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;
    private Rigidbody rb;
    [Header("Ground Check")]
    [Tooltip("Används för att raycasta groundcheck")]
    public float playerHeight;
    public LayerMask groundMask;
    private bool grounded;
    public Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        readyToJump = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position + new Vector3(0, 0.8f, 0), Vector3.down, playerHeight * 0.5f + 0.1f, groundMask);

        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        if (Input.GetKeyDown(KeyCode.V)){
            GameEventsManager.instance.inputEvents.SubmitPressed();
        }
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void MovePlayer()
    {
        moveDirection = cameraTransform.forward * verticalInput + cameraTransform.right * horizontalInput;
        moveDirection.y = 0;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10 * airMultiplier, ForceMode.Force);
        }
        if (moveDirection != Vector3.zero)
        {
            Vector3 tempDir = new Vector3(moveDirection.x, 0, moveDirection.z);

            Quaternion toRotation = Quaternion.LookRotation(tempDir, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + new Vector3(0, 0.8f, 0), new Vector3(transform.position.x, transform.position.y - (playerHeight * 0.5f + 0.1f), transform.position.z) -transform.position);
    }*/
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            //Cursor.lockState = CursorLockMode.None;
        }
    }
}
