using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [Header("Patrol")]
    public Transform pointA; // İlk hedef nokta
    public Transform pointB; // İkinci hedef nokta
    public float speed = 2f; // Hareket hızı

    private Transform targetPoint; // Hedef nokta
    private bool movingToA = true; // Hangi yöne hareket ettiğini kontrol eder

    [Header("Damage")]
    public int health = 3; // Sağlık değeri
    public GameObject afterEffect;
    public AudioClip damageSound; // Hasar alındığında çalacak ses efekti
    private AudioSource _audioSource;

    [Header("Boss Level")]
    public PlayerBag _stats; // Bu değişkenin doğru şekilde atandığından emin olun

    private void Start() 
    {
        // Başlangıç hedef noktası olarak pointA'yı ayarlayın
        targetPoint = pointA;
        _audioSource = gameObject.AddComponent<AudioSource>();

        // Başlangıç hızlarını güncelle
        UpdateStats();
    }

    private void Update() 
    {
        PatrolMovement();
    }

    void PatrolMovement()
    {
        // Karakteri hedef noktaya doğru hareket ettir
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // Hedef noktaya ulaşıp ulaşmadığını kontrol et
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Hedef noktayı değiştir
            if (movingToA)
            {
                targetPoint = pointB;
            }
            else
            {
                targetPoint = pointA;
            }
            movingToA = !movingToA; // Yönü değiştir
        }
    }

    void Damage()
    {
        health--;
        PlayDamageSound(); // Hasar alındığında ses efekti çal
        if (health <= 0)
        {
            _stats.bossLevel++; // Boss seviyesi artır
            UpdateStats(); // Hızları güncelle
            Instantiate(afterEffect, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f); // Biraz gecikme ekleyin
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

    void UpdateStats()
    {
        // Boss seviyesi arttıkça hızları artır
        speed = 2f + (_stats.bossLevel * 0.1f);
        // Burada boss'un saldırı hızını da artırabilirsiniz, örneğin:
        // attackSpeed = baseAttackSpeed + (_stats.bossLevel * 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D cls) 
    {
        if (cls.gameObject.CompareTag("PlayerBullet"))
        {
            Damage();
            Destroy(cls.gameObject);
            Debug.Log("hasar verildi");
        }
    }
}
