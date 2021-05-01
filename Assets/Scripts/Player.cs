using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public LayerMask groundLayers;
    public int maxAirJumps;
    public GameObject bullet;
    public Transform shootPoint;
    
    private Rigidbody2D _rb;

    private bool _isGrounded;
    private int _airJumpsLeft;
    private bool _isFacingRight;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _isGrounded = false;
        _isFacingRight = true;
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

        if (horizontal > 0f) _isFacingRight = true;
        if (horizontal < 0f) _isFacingRight = false;

        if (_isFacingRight) transform.localScale = Vector3.one;
        else transform.localScale = new Vector3(-1f, 1f, 1f);
        
        if (Input.GetMouseButtonDown(0)) {
            // create a bullet
            GameObject b = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            // make the face the right direction
            if (!_isFacingRight) b.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
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
