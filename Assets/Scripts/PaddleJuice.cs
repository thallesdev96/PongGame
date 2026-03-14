using UnityEngine;
using DG.Tweening;

public class PaddleJuice : MonoBehaviour
{
    public Transform paddleSprite;
    public float BounceForce = 5f;

    public void PlayHitEffect()
    {
        paddleSprite.transform.DOScale(1.3f, 0.1f).SetLoops(2, LoopType.Yoyo);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ballRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            float yOffset = collision.transform.position.y - transform.position.y;
            float paddleHeight = GetComponent<Collider2D>().bounds.size.y;
            float normalizedY = yOffset / (paddleHeight / 2f);
            Vector2 direction = new Vector2(Mathf.Sign(ballRigidbody.linearVelocity.x), normalizedY).normalized;
            ballRigidbody.linearVelocity = direction * BounceForce;        }
    }
}
