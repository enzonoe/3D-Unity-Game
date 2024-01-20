using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatisground;
    bool grounded;

    public Transform orientation;

    float xInput; //horizontal
    float yInput; //vertical

    Vector3 moveDirection;

    Rigidbody rigidb;

    // Start is called before the first frame update
    void Start()
    {
        rigidb = GetComponent<Rigidbody>();
        rigidb.freezeRotation = true;
    }

   

    // Update is called once per frame
    void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisground);

        MyInput();
        SpeedControl();

        //handle drag
        if (grounded)
            rigidb.drag = groundDrag;
        else
            rigidb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * yInput + orientation.right * xInput;

        rigidb.AddForce(moveDirection.normalized * moveSpeed * 10f,ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rigidb.velocity.x, 0f, rigidb.velocity.z);

        //limit velocity if needed
        if(flatVelocity.magnitude>moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rigidb.velocity = new Vector3(limitedVelocity.x, rigidb.velocity.y, limitedVelocity.z);
        }
    }
}
