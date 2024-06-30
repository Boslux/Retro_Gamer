using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawner : MonoBehaviour
{
    [Header("Spawn Position")]
    Vector3 spawnPosition;
    float _xMin;
    float _yMax;

    [Header("Spawn Time")]
    float _timer=12;
    public float maxTime=45;
    float minTime=15;

    [Header("Game Object")]
    public GameObject npc;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void ReadyToTimer()
    {
        _timer = Random.Range(minTime, maxTime);
    }
    IEnumerator Spawner()
    {
        while (true)
        {
            if (_timer <= 0)
            {
                spawnPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                Instantiate(npc, spawnPosition, Quaternion.identity);
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
