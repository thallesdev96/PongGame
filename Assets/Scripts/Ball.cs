using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Ball : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rig;
    public SpriteRenderer spriteRenderer;
    public TrailRenderer trailRenderer;
    public GameObject ballExplosionPrefab;

    [Header("Audio Sources")]
    public AudioSource audioSource;
    public AudioClip ballHit;
    public AudioClip ballExplode;
    public AudioClip gameOver;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayBall());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Launch()
    {
        audioSource.PlayOneShot(ballHit);
        Vector2 direction = Vector2.zero;
        if (Random.value < 0.5f)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }
        direction.y = Random.Range(-1f, 1f);
        rig.linearVelocity = direction * speed;
    }

    public void ResetBall()
    {
        spriteRenderer.transform.localScale = Vector3.one;
        rig.linearVelocity = Vector2.zero;
        trailRenderer.Clear();
        trailRenderer.enabled = false;
        transform.position = Vector2.zero;
        trailRenderer.enabled = true;
        StartCoroutine(PlayBall());
    }

    IEnumerator PlayBall()
    {
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = true;
        Launch();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            collision.gameObject.GetComponent<PaddleJuice>().PlayHitEffect();

            BallJuice();
        }

        
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            BallJuice();
        }

    }

    private void BallJuice()
    {
        audioSource.PlayOneShot(ballHit);
        // Destroy(gameObject, 1f);


        spriteRenderer.transform.DOKill();
        spriteRenderer.transform.localScale = Vector3.one;
        spriteRenderer.transform.DOScale(1.8f, 0.1f).SetLoops(2, LoopType.Yoyo);

        Camera.main.transform.DOKill();
        Camera.main.transform.DOShakePosition(
            0.1f, 
            0.2f, 
            10, 
            90, 
            false, 
            true
            );
    }

    public void PlayBallExplosion()
    {
        GameObject explosion = Instantiate(ballExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
    }

    public void PlayExplodeSound()
    {
        audioSource.PlayOneShot(ballExplode);
        audioSource.PlayOneShot(gameOver);

    }
}
