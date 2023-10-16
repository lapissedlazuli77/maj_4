using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    Rigidbody2D rbody;
    Animator anim;

    public bool grounded = false;
    public float castDist = 0.2f;

    public float jumpLimit = 2f;
    public float gravityScale = 5f;
    public float gravityFall = 40f;
    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("space") && grounded)
        {
            jump = true;
        }
    }
    void FixedUpdate()
    {
        if (jump)
        {
            rbody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            jump = false;
        }
        if (rbody.velocity.y >= 0)
        {
            rbody.gravityScale = gravityScale;
        }
        else if (rbody.velocity.y < 0)
        {
            rbody.gravityScale = gravityFall;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down * castDist, new Color(255, 0, 0));
        if (hit.collider != null && hit.transform.tag == "Platform")
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}
