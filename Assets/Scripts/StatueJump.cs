using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueJump : MonoBehaviour
{
    Rigidbody2D rbody;

    public bool grounded = false;
    public float castDist = 0.4f;

    public float jumpLimit = 2f;
    public float gravityScale = 5f;
    public float gravityFall = 40f;
    bool jump = false;

    float currenttime = 0;
    float targetTime = 0.6f;

    public GameObject player;
    float xspot;
    Vector3 destination;
    public float maxMoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        xspot = player.transform.position.x;
        destination = transform.position;
        destination.x = xspot;
    }

    // Update is called once per frame
    void Update()
    {
        currenttime += Time.deltaTime;
        if (currenttime > targetTime)
        {
            currenttime = 0;
            targetTime = Random.Range(0.8f, 2.0f);
            int jumpcoin = Random.Range(0, 2);
            if (jumpcoin == 1) { jump = true; }
        }
        if (!grounded)
        {
            var step = maxMoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
        }
    }
    void FixedUpdate()
    {
        if (jump)
        {
            rbody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            xspot = player.transform.position.x;
            destination.x = xspot;
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
