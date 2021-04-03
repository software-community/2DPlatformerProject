using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        
        Vector2 vel = _rb.velocity;
        vel.x = horizontal * moveSpeed;
        _rb.velocity = vel;
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
