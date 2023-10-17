using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Jumping : MonoBehaviour
{
    Rigidbody2D rbody;
    Animator anim;

    int timescore = 0;
    int killscore = 0;

    public bool grounded = false;
    public float castDist = 0.2f;

    public float jumpLimit = 2f;
    public float gravityScale = 5f;
    public float gravityFall = 40f;
    bool jump = false;

    float horizMove;
    public float speedmodify;

    float currenttime = 0;
    float targetTime = 1f;


    [SerializeField]
    private TMP_Text runscore;
    [SerializeField]
    private TMP_Text enemyscore;
    [SerializeField]
    private GameObject restart;

    string display1 = "Total time survived: ";
    string display2 = "Enemies defeated: ";

    // Start is called before the first frame update
    void Start()
    {
        restart.SetActive(false);
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizMove = Input.GetAxis("Horizontal");
        if (Input.GetKey("space") && grounded)
        {
            jump = true;
        }
        currenttime += Time.deltaTime;
        if (currenttime > targetTime)
        {
            currenttime = 0;
            timescore++;
        }
    }
    void FixedUpdate()
    {
        HorizontalMove(horizMove);
        if (jump)
        {
            rbody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            anim.SetBool("jumping", true);
            jump = false;
        }
        if (rbody.velocity.y >= 0)
        {
            rbody.gravityScale = gravityScale;
        }
        else if (rbody.velocity.y < 0)
        {
            rbody.gravityScale = gravityFall;
            anim.SetBool("jumping", false);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (transform.position.y >= collision.gameObject.transform.position.y)
            {
                Destroy(collision.gameObject);
                killscore++;
                jump = true;
            } else if (transform.position.y < collision.gameObject.transform.position.y)
            {
                finalscreen();
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Border")
        {
            finalscreen();
            Destroy(gameObject);
        }
    }

    void HorizontalMove(float toMove)
    {
        float moveX = toMove * speedmodify * Time.fixedDeltaTime;
        rbody.velocity = new Vector3(moveX, rbody.velocity.y);
    }
    void finalscreen()
    {
        string stringtime = timescore.ToString();
        string stringkill = killscore.ToString();

        display1 += stringtime;
        display2 += stringkill;

        restart.SetActive(true);
        runscore.text = display1;
        enemyscore.text = display2;
    }
}
