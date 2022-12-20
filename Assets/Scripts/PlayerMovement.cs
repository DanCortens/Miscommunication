using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float groundedDrag;
    [SerializeField] private bool grounded;
    [SerializeField] private float accel;
    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private bool canJump;
    [SerializeField] private float airSlowdown;
    [Header("Misc")]
    [SerializeField] private Transform orientation;
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask groundLayer;
    

    private float horzInput;
    private float vertInput;

    private Vector3 moveDir;
    private Rigidbody rb;
    public GameObject footstepControl;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);
        GetInput();
        if (Mathf.Abs( Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical"))>0)
        {
            footstepControl.SetActive(true);
        }
        else
        {
            footstepControl.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void GetInput()
    {
        horzInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.Space) && canJump && grounded)
        {
            canJump = false;
            Jump();
            Invoke("CanJump", jumpCooldown);
        }
    }
    private void Movement()
    {
        rb.drag = (grounded) ? groundedDrag : 0f;
        moveDir = orientation.forward * vertInput + orientation.right * horzInput;
        float multiplier = (grounded) ? accel : accel * airSlowdown;
        rb.AddForce(moveDir * moveSpeed * multiplier, ForceMode.Force);
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            flatVel = (flatVel.normalized) * moveSpeed;
            rb.velocity = new Vector3(flatVel.x, rb.velocity.y, flatVel.z);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void CanJump()
    {
        canJump = true;
    }
}
