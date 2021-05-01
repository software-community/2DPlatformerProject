using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float damage = 5f;
    public float lifetime = 5f;

    private float _lifetimer = 0f;
    private void Update() {
        // keep moving toward bullet's right direction
        transform.position += transform.right * (speed * Time.deltaTime);
        
        // die if too old
        _lifetimer += Time.deltaTime;
        if (_lifetimer >= lifetime) {
            // destroy
            Destroy(gameObject);
        }
    }

    // if the bullet hits something
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
    }
}
