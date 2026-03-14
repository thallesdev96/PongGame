using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddle : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * direction * speed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }
}
