using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BaseMovement
{
    [SerializeField] private AnimatorController myAnim;
    private Vector3 tempMovement;
    private Rigidbody rb;
    public float gravity = 20f; // Adjust this value as needed

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the player GameObject.");
        }
    }

    private void Update()
    {
        tempMovement = Input.GetAxis("Horizontal") * Camera.main.transform.right + Input.GetAxis("Vertical") * Camera.main.transform.forward;
        tempMovement.y = 0f; // Ensure no vertical movement
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        PlayerMove();
        ChangeAnimation();
    }

    private void PlayerMove()
    {
        Move(tempMovement);
        if (tempMovement.magnitude > 0.1f)
        {
            float rot = Mathf.Atan2(-tempMovement.z, tempMovement.x) * Mathf.Rad2Deg + 90f;
            transform.rotation = Quaternion.Euler(0f, rot, 0f);
        }
    }

    private void ChangeAnimation()
    {
        if (myAnim)
        {
            myAnim.ChangeAnimBoolValue("Running", tempMovement.magnitude > 0f);
        }
    }

    private void ApplyGravity()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
        else
        {
            Debug.LogError("Rigidbody component not found on the player GameObject.");
        }
    }
}
