using System;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event Action EnemyHit = delegate { };
    [SerializeField] private Rigidbody2D rb;
    private Animator animator;
    private bool isJumping;
    public bool gameOver;

    private Sequence jumpDotweenAnimation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isJumping || gameOver) return;
            isJumping = true;
            FindObjectOfType<AudioManager>().PlaySound("Jump");
            animator.SetBool("isJumping", true);
            jumpDotweenAnimation = transform.DOJump(new Vector3(-8, (float) -1.133316), 6, 1, 1, false)
                .OnComplete(() => isJumping = false);
        }
    }

    private void FixedUpdate()
    {
        animator.SetBool("isJumping", false);
    }

    public void SetGameState(bool isGameRunning)
    {
        animator.speed = isGameRunning ? 1 : 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var objectTag = other.gameObject.tag;

        if (objectTag.Equals("Enemy"))
        {
            gameOver = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.bodyType = RigidbodyType2D.Kinematic;
            var positionWhileJumping = transform.position;
            jumpDotweenAnimation.Kill(true);
            transform.position = positionWhileJumping;
            EnemyHit();
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
            FindObjectOfType<AudioManager>().StopSound("Background");
        }
    }
}