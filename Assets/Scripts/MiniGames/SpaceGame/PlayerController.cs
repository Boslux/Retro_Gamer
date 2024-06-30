using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Component")]
    Rigidbody2D _rb;
    
    [Header("Fire")]
    public GameObject bullet;
    public float bulletSpeed; // Mermi hızı
    bool _canAttack = true;

    [Header("Movement")]
    public float speed;

    [Header("Health")]
    public int health = 3;
    public GameObject afterEffect;
    
    [Header("Damage")]
    public AudioClip damageSound; // Hasar alındığında çalacak ses efekti
    private AudioSource _audioSource;

    private void Awake() 
    {
        _rb = GetComponent<Rigidbody2D>();  
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        Movement();
        Attack();
    }

    void Movement()
    {
        float hMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime * 100;
        Vector2 move = new Vector2(hMove, 0);

        _rb.velocity = move;
    }

    void Attack()
    {
        if (Input.GetButtonDown("Jump") && _canAttack)
        {
            FireBullet();
            StartCoroutine(CanAttack());
        }
    }

    IEnumerator CanAttack()
    {
        _canAttack = false;
        yield return new WaitForSeconds(0.5f);
        _canAttack = true;
    }

    void FireBullet()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * bulletSpeed; // Mermiyi yukarı doğru hareket ettir
    }

    void Damage()
    {
        health--;
        PlayDamageSound();
        if (health <= 0)
        {
            Instantiate(afterEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); // Hemen yok et
        }
    }

    void PlayDamageSound()
    {
        if (damageSound != null)
        {
            _audioSource.PlayOneShot(damageSound);  // Ses efektini çal
        }
        else
        {
            Debug.LogWarning("Damage sound is not assigned.");
        }
    }

    private void OnTriggerEnter2D(Collider2D cls) 
    {
        if (cls.gameObject.CompareTag("EnemyBullet"))
        {
            Damage();
            Destroy(cls.gameObject);
            Debug.Log("hasar alındı");
        }
    }
}
