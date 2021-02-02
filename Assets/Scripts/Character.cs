using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    public float moveSpeed;
    public float jumpforce;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;

    public GameObject HealthText;
    public GameObject CoinText;
    public AudioClip[] AudioClipArr;

    private int countJump = 0;
    private int healthCount = 100;
    private int coinCount;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (healthCount <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }

        if (coinCount >= 2)
        {
            SceneManager.LoadScene("WinScene");
        }

    }

    private void PlayerMovement()
    {
        float hVelocity = 0;
        float vVelocity = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            hVelocity = -moveSpeed;
            animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));
            transform.localScale = new Vector3(-1, 1, 1);
            audioSource.PlayOneShot(AudioClipArr[1], 0.5f);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            animator.SetFloat("xVelocity", 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            hVelocity = moveSpeed;
            animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));
            transform.localScale = new Vector3(1, 1, 1);
            audioSource.PlayOneShot(AudioClipArr[1], 0.5f);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetFloat("xVelocity", 0);
        }
        if (Input.GetKeyDown(KeyCode.Space) && countJump == 0)
        {
            vVelocity = jumpforce;
            animator.SetTrigger("JumpTrigger");
            audioSource.PlayOneShot(AudioClipArr[3], 0.5f);
            countJump = 1;
        }

        hVelocity = Mathf.Clamp(rb.velocity.x + hVelocity, -5, 5);

        rb.velocity = new Vector2(hVelocity, rb.velocity.y + vVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mace")
        {
            healthCount -= 10;
            HealthText.GetComponent<Text>().text = "Health:" + healthCount;
            audioSource.PlayOneShot(AudioClipArr[2], 0.5f);
        }
        if(collision.gameObject.tag == "Coin")
        {
            coinCount++;
            Destroy(collision.gameObject);
            CoinText.GetComponent<Text>().text = "Coin:" + coinCount;
            audioSource.PlayOneShot(AudioClipArr[0], 0.5f);
        }
        if (collision.gameObject.tag == "Ground")
        {
            countJump = 0;
        }
    }
}
