using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Spawner Value")]
    [SerializeField] float xRight;
    [SerializeField] float yLeft;

    [Header("Spawn Items")]
    public GameObject items;

    [Header("Spawn Timer")]
    float _timer;
    public float maxTime = 15;

    [Header("Orders")]
    Vector3 spawnPosition;
    float _xMin, _yMax;

    void Start()
    {
        SpawnPosition();
        StartCoroutine(Spawner());
        ReadyToTimer();
    }

    void SpawnPosition()
    {
        _xMin = transform.position.x - yLeft;
        _yMax = transform.position.x + xRight;
    }

    void ReadyToTimer()
    {
        _timer = Random.Range(0, maxTime);
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            if (_timer <= 0)
            {
                spawnPosition = new Vector3(Random.Range(_xMin, _yMax), transform.position.y, transform.position.z);
                Instantiate(items, spawnPosition, Quaternion.identity);
                ReadyToTimer(); // _timer'ı yeniden ayarla
                Debug.Log("Item Spawned");
            }
            else
            {
                _timer -= Time.deltaTime;
            }

            yield return null; // Bir frame bekleniyor
        }
    }
}
