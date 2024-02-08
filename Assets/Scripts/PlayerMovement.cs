using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BaseMovement
{

    [SerializeField]
    private AnimatorController myAnim;

    private Vector3 tempMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        tempMovement = Input.GetAxis("Horizontal") * Camera.main.transform.right + Input.GetAxis("Vertical") * Camera.main.transform.forward;
        tempMovement.y = 0f; // Ensure no vertical movement
    }

    void FixedUpdate()
    {
        PlayerMove();
        ChangeAnimation();
    }

    void PlayerMove()
    {
        Move(tempMovement);
    }

    void ChangeAnimation()
    {
        if (myAnim)
        {
            if (tempMovement.magnitude > 0f)
            {
                myAnim.ChangeAnimBoolValue("Running", true);

                float rot = Mathf.Atan2(-tempMovement.z, tempMovement.x) * Mathf.Rad2Deg + 90f;
                transform.rotation = Quaternion.Euler(0f, rot, 0f);
            }
            else
            {
                myAnim.ChangeAnimBoolValue("Running", false);
            }
        }
    }
}
