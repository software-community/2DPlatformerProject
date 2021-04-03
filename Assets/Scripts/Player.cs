using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public LayerMask groundLayers;
    public int maxAirJumps;
    
    private Rigidbody2D _rb;

    private bool _isGrounded;
    private int _airJumpsLeft;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _isGrounded = false;
    }

    private void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        
        Vector2 vel = _rb.velocity;
        vel.x = horizontal * moveSpeed;
        _rb.velocity = vel;
        
        if (Input.GetKeyDown(KeyCode.Space)) {        // if we pressed space
            if (_isGrounded || _airJumpsLeft > 0) {    // if we are grounded or can air jump
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);    // jump
                if (!_isGrounded) {    // if we used an air jump
                    _airJumpsLeft--;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        // 0001 0000 0010
        // 0000 0000 1000
        
        // 0000 0000 0000
        // other.layer = 8
        // 1 left x
        if (((1 << other.gameObject.layer) & groundLayers.value) != 0) {
            _isGrounded = true;
            _airJumpsLeft = maxAirJumps;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (((1 << other.gameObject.layer) & groundLayers.value) != 0) {
            _isGrounded = false;
        }
    }
}
