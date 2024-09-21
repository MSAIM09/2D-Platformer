using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float jumpForce = 15f;

    private Rigidbody2D rb;
    private int jumpCount = 0;
    public int maxJumpCount = 2;
    private uint coins = 0;
    private uint diamonds = 0;
    private uint hearts = 0;

    public Text coinsCollectedLabel;
    public Text diamondsCollectedLabel;
    public Text heartsCollectedLabel;

    public AudioClip coinCollectSound;
    public AudioClip diamondCollectSound;
    public AudioClip heartCollectSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Horizontal movement
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Flip sprite based on movement direction
        if (moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && (jumpCount < maxJumpCount))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Coins"))
        {
            CollectCoin(collider);
        }
        else if (collider.gameObject.CompareTag("Diamonds"))
        {
            CollectDiamond(collider);
        }
        else if (collider.gameObject.CompareTag("Hearts"))
        {
            CollectHeart(collider);
        }
    }

    private void CollectCoin(Collider2D coinCollider)
    {
        coins++;
        coinsCollectedLabel.text = coins.ToString();
        Destroy(coinCollider.gameObject);
        AudioSource.PlayClipAtPoint(coinCollectSound, transform.position);
    }

    private void CollectDiamond(Collider2D diamondCollider)
    {
        diamonds++;
        diamondsCollectedLabel.text = diamonds.ToString();
        Destroy(diamondCollider.gameObject);
        AudioSource.PlayClipAtPoint(diamondCollectSound, transform.position);
    }

    private void CollectHeart(Collider2D heartCollider)
    {
        hearts++;
        heartsCollectedLabel.text = hearts.ToString();
        Destroy(heartCollider.gameObject);
        AudioSource.PlayClipAtPoint(heartCollectSound, transform.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jumpCount when landing on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
        }
    }
}
