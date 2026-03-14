using UnityEngine;


public class Wall : MonoBehaviour
{
    public GameManager gameManager;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            bool isGameOver = false;
            
            if (CompareTag("AIWall"))
            {
                isGameOver = gameManager.AIPoint();
            }
            else
            {
                isGameOver = gameManager.PlayerPoint();
            }

            Ball ball = collision.gameObject.GetComponent<Ball>();
            
            if (isGameOver)
            {
                // Game over - hide ball, no explosion
                ball.spriteRenderer.enabled = false;
                ball.trailRenderer.enabled = false;
            }
            else
            {
                // Game continues - show explosion and reset
                ball.PlayBallExplosion();
                ball.PlayExplodeSound();
                ball.ResetBall();
            }
        }
    }
}
