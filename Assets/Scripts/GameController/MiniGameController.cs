using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGameController : MonoBehaviour
{
    public GameObject miniGamePanel; // Mini oyunun UI paneli

    public void StartMiniGame()
    {
        // Oyunu durdur
        miniGamePanel.SetActive(true);
        StartCoroutine(MiniGameScen());
    }
    IEnumerator MiniGameScen()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(2);
        Time.timeScale = 0f;
        
        
    }
    // SceneManager.LoadScene(Random.Range(2, 5));


}
