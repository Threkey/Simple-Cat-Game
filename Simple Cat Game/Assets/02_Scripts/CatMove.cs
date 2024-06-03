using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatMove : MonoBehaviour
{
    Rigidbody2D rb;

    public Animator anim;

    public float speed, maxSpeed, force;
    float screenWidthHalf;
    public bool isGround;
    float animSpeedX;

    Vector3 pos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        screenWidthHalf = Camera.main.aspect * Camera.main.orthographicSize;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CatMoveX();
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
            CatJump();

        if (Camera.main.WorldToScreenPoint(transform.position).y < -100)
            Dead();
    }

    void CatMoveX()
    {
        // 화면 밖으로 나가지 않게
        pos = transform.position;

        if(transform.position.x > screenWidthHalf)
            pos.x = screenWidthHalf;
        else if(transform.position.x < -screenWidthHalf)
            pos.x = -screenWidthHalf;

        transform.position = pos;

        // 속도 최대치보다 빠르지 않게
            int key = 0;
            if (Input.GetKey(KeyCode.LeftArrow) && rb.velocity.x > -maxSpeed)
            {
                rb.AddForce(Vector3.left * speed);
                key = 1;
            }

            else if (Input.GetKey(KeyCode.RightArrow) && rb.velocity.x < maxSpeed)
            {
                rb.AddForce(Vector3.right * speed);
                key = -1;
            }

        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x) / 2f);
            /*
        float threshold = 0.2f;
        int key = 0;
        if (Input.acceleration.x > this.threshold) key = 1;
        if (Input.acceleration.x < -this.threshold) key = -1;
            */
    }

    void CatJump()
    {
        rb.AddForce(Vector3.up * force);

        anim.SetInteger("isJump", 1);

        /*
        // 위로 올라갈때는 안부딪히게
        if (rb.velocity.y < 0)
            rb.isKinematic = false;
        else
            rb.isKinematic = true;
        */
    }

    void Dead()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
        anim.SetInteger("isJump", 0);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
}
