using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BaseMovement
{
    [SerializeField]
    private AnimatorController myAnim;

    private Vector3 tempMovement;
    private Rigidbody rb;
    public float gravity = 9.81f; // Gravity strength
    private Quaternion targetRotation; // Store target rotation

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        tempMovement = Input.GetAxis("Horizontal") * Camera.main.transform.right + Input.GetAxis("Vertical") * Camera.main.transform.forward;
        tempMovement.y = 0f; // Ensure no vertical movement

        // Rotate the player towards movement direction if there's input
        if (tempMovement.magnitude > 0.1f)
        {
            float rot = Mathf.Atan2(-tempMovement.z, tempMovement.x) * Mathf.Rad2Deg + 90f;
            targetRotation = Quaternion.Euler(0f, rot, 0f);
        }
    }

    void FixedUpdate()
    {
        ApplyGravity(); // Apply gravity in FixedUpdate
        PlayerMove();
        ChangeAnimation();
    }

    void PlayerMove()
    {
        Move(tempMovement);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    void ChangeAnimation()
    {
        if (myAnim)
        {
            myAnim.ChangeAnimBoolValue("Running", tempMovement.magnitude > 0f);
        }
    }

    void ApplyGravity()
    {
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }
}
