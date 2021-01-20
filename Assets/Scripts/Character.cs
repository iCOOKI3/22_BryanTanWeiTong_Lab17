using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public float moveSpeed;
    public float jumpforce;
    public GameObject HealthText;
    public GameObject CoinText;

    public int healthCount;
    public int coinCount;

    private Rigidbody2D rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        HealthText.GetComponent<Text>().text = "Health: 100";
        CoinText.GetComponent<Text>().text = "Coin: 0";
    }

    // Update is called once per frame
    void Update()
    {
        float hVelocity = 0;
        float vVelocity = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            hVelocity = -moveSpeed;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            hVelocity = moveSpeed;
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            vVelocity = jumpforce;
        }

        hVelocity = Mathf.Clamp(rb.velocity.x + hVelocity, -5,5);

        rb.velocity = new Vector2(hVelocity,rb.velocity.y + vVelocity);

        animator.SetFloat("xVelocity",Mathf.Abs(hVelocity));

        animator.SetFloat("xVelocity",0);

        animator.SetTrigger("JumpTrigger");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mace")
        {
            healthCount -= 10;
            HealthText.GetComponent<Text>().text = "Health:" + healthCount;
        }
        if(collision.gameObject.tag == "Coin")
        {
            coinCount++;
            Destroy(collision.gameObject);
            CoinText.GetComponent<Text>().text = "Coin:" + coinCount;
        }
    }
}
