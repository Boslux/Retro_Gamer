using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GasController : MonoBehaviour
{
    PlayerBag _bag;
    
    float _timer = 6.4f;

    int gasMoney=100;

    private void Awake() 
    {
        _bag = Resources.Load<PlayerBag>("PlayerBag");
    }

    private void Start() 
    {
        StartCoroutine(Gas());
    }

    IEnumerator Gas()
    {
        float temporaryTimer=_timer;
        while (true)
        {
            if (_timer <= 0)
            {
                _bag.gas -= 1;
                _timer = temporaryTimer;  // Timer'ı tekrar başlat
                CheckProgress();
            }
            else
            {
                _timer -= Time.deltaTime;
            }
            yield return null;  // Bir frame bekle
        }
    }

    void CheckProgress()
    {
        
        if (_bag.gas < 0)
        {
            if (_bag.money >= gasMoney)
            {
                _bag.gas = 4;
                _bag.money -= 100;  // Para düşmeli
                gasMoney+=20;
            }
            else
            {
                Debug.Log("Game Over Amınyum");
                StopAllCoroutines();  // Tüm coroutineleri durdurur.
                SceneManager.LoadScene("Game Over");
            }
        }   
    }
}
