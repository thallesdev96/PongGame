using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public int playerScore;
    public int AIScore;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI AIScoreText;    
    public GameOver gameOver;
    public int winScore = 5;

    [Header("Audio Sources")]
    public AudioSource audioSource;
    public AudioClip scoreSFX;

    public bool PlayerPoint()
    {
        PlayerScoreJuice(playerScoreText);
        playerScore++;
        playerScoreText.text = playerScore.ToString();
        if (playerScore >= winScore)
        {
            gameOver.ShowGameOver("Player 1 Wins");
            return true;
        }
        return false;
    }

    public bool AIPoint()
    {
        PlayerScoreJuice(AIScoreText);
        AIScore++;
        AIScoreText.text = AIScore.ToString();
        if (AIScore >= winScore)
        {
            gameOver.ShowGameOver("Player 2 Wins"); 
            return true;
        }
        return false;
    }

    private void PlayerScoreJuice(TextMeshProUGUI text)
    {
        audioSource.PlayOneShot(scoreSFX);
        text.transform.DOKill();
        text.transform.localScale = Vector3.one;
        text.transform.DOScale(1.5f, 0.5f).SetLoops(2, LoopType.Yoyo);
        // text.DOColor(Color.yellow, 0.5f).SetLoops(2, LoopType.Yoyo);
    }
}
