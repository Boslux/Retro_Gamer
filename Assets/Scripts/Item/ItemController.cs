using UnityEngine;

public class ItemController : MonoBehaviour
{
    [Header("Sprite")]
    public Sprite[] sprite;
    SpriteRenderer _spriteRenderer;
    [Header("Movement")]
    public float moveSpeed;

    [Header("Bag")]
    PlayerBag _playerBag;
    int moneyIncrease=5;

    GameObject _afterEffect;
    Animator _anim;

    private void Awake() 
    {
        Component();
    }
    void Start()
    {
        RandomSprite();
        RandomMoneyCount();
    }
    void Component()
    {
        _anim=GetComponent<Animator>();
        _spriteRenderer=GetComponent<SpriteRenderer>();
        _playerBag=Resources.Load<PlayerBag>("PlayerBag");
        _afterEffect=Resources.Load<GameObject>("After Effect");
    }
    void Update()
    {
        ItemMoveDown();
    }
    void RandomSprite()
    {
        //random obje oluştururken random bir sprite çekmesini istedim
         if (sprite != null && sprite.Length > 0)
        {
            _spriteRenderer.sprite = sprite[Random.Range(0, sprite.Length)];
        }
        else
        {
            Debug.LogWarning("Sprite array is null or empty.");
        }
    }
    void RandomMoneyCount()
    {
        //para miktarını random arttır.
        moneyIncrease=Random.Range(1,25);
    }
    void OnMouseDown()
    {
        //PlayerBag e girip para miktarını random arttır
        if (_playerBag != null)
        {
            Instantiate(_afterEffect,transform.position,Quaternion.identity);
            _playerBag.money += moneyIncrease;
            Destroy(gameObject); // Ekrandan kaldır
            Debug.Log("Item Collected");
        }
        else
        {
            Debug.LogError("_playerBag is not assigned.");
        }   
    }
    void ItemMoveDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime, transform.position.z);    
    }
}
