using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackPattern : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefabı
    public float baseFireRate = 1f; // Temel atış hızı
    public float bulletSpeed = 5f; // Mermi hızı
    public Transform firePoint; // Mermilerin çıkacağı nokta
    public int bulletCount = 8; // Bir seferde atılacak mermi sayısı

    // Public değişkenler
    public float tripleShotSpacing = 0.5f; // Triple Shot mermileri arasındaki mesafe
    public int sweepCount = 5; // Sweep mermi sayısı
    public float sweepStartAngle = -30f; // Sweep başlangıç açısı
    public float sweepAngleStep = 15f; // Sweep açılı saldırıdaki açı farkı

    private int attackType = 1; // Saldırı tipi

    public PlayerBag _stats;
    private float fireRate;

    private void Start()
    {
        UpdateFireRate(); // Başlangıçta fire rate'i güncelle
        InvokeRepeating("Fire", 0.5f, fireRate);
    }

    void UpdateFireRate()
    {
        // Boss seviyesine bağlı olarak ateş hızını güncelle
        fireRate = baseFireRate - (_stats.bossLevel * 0.02f);
        // Fire rate çok düşük olmamalı, bu yüzden minimum bir değeri sınırlandırabilirsiniz
        if (fireRate < 0.1f) 
        {
            fireRate = 0.1f;
        }
    }

    void Fire()
    {
        switch (attackType)
        {
            case 1:
                FirePattern();
                break;
            case 2:
                FireTripleShot();
                break;
            case 3:
                FireSweep();
                break;
        }
        
        // Rastgele saldırı tipi değiştir (isteğe bağlı)
        attackType = Random.Range(1, 4);
    }

    void FirePattern()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (360f / bulletCount);
            SpawnBullet(angle);
        }
    }

    void FireTripleShot()
    {
        for (int i = -1; i <= 1; i++)
        {
            Vector3 position = firePoint.position + new Vector3(i * tripleShotSpacing, 0, 0);
            GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.down * bulletSpeed; // Y ekseninde -1 yönünde hareket ettir
        }
    }

    void FireSweep()
    {
        for (int i = 0; i < sweepCount; i++)
        {
            float angle = sweepStartAngle + (i * sweepAngleStep);
            SpawnBullet(angle);
        }
    }

    void SpawnBullet(float angle)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.down; // Y ekseninde -1 yönünde hareket ettir
        rb.velocity = direction * bulletSpeed;
    }

    public void IncreaseBossLevel()
    {
        _stats.bossLevel++;
        UpdateFireRate();
        CancelInvoke("Fire");
        InvokeRepeating("Fire", 0.5f, fireRate);
    }
}
