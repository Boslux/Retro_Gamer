using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public Transform hitPosition;
    public LayerMask layer;
    public Vector2 size; // size float yerine Vector2 olmalı
    MiniGameController _miniGame;

    [Header("npc list")]
    private SpriteRenderer _sp;
    public Sprite[] npc;
    float moveSpeed = 3;
    
    private void Awake() 
    {
        _miniGame=GameObject.Find("MiniGameController").GetComponent<MiniGameController>();
        _sp=GetComponent<SpriteRenderer>();
    }
    private void Start() 
    {
        _sp.sprite=npc[Random.Range(0,npc.Length)];
    }
    void Update() 
    {
        MatchStartFunction();
        MoveDown();
    }
    void MatchStartFunction()
    {
        // OverlapCapsuleAll kullanımı
        Collider2D[] hits = Physics2D.OverlapCapsuleAll(hitPosition.position, size, CapsuleDirection2D.Vertical, 0f, layer);

        // Çarpışan nesneleri işleme
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player")) // Tag'ı "Player" olan objeyle temas kontrolü
            {
                _miniGame.StartMiniGame(); // Mini oyunu başlat
                break; // Bir kez çalıştıktan sonra döngüden çık
            }
        }
    }
    void MoveDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);    
    }
    // Saldırı alanını sahnede görmek için
    private void OnDrawGizmosSelected()
    {
        if (hitPosition == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(hitPosition.position, size);
    }
}
