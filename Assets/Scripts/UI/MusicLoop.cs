using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoop : MonoBehaviour
{
    AudioSource _music;
    public List<AudioClip> musicClips; // Çalınacak müziklerin listesi
    private int _currentClipIndex;

    private void Awake() 
    {
        _music = GetComponent<AudioSource>();
        if (_music == null)
        {
            Debug.LogError("AudioSource component is missing on this GameObject.");
        }
    }

    private void Start() 
    {
        if (musicClips == null || musicClips.Count == 0)
        {
            Debug.LogError("Music clips list is empty.");
            return;
        }

        PlayRandomMusic();
    }

    void PlayRandomMusic()
    {
        if (musicClips.Count == 0) return;

        _currentClipIndex = Random.Range(0, musicClips.Count);
        _music.clip = musicClips[_currentClipIndex];
        _music.Play();

        // Müzik bittiğinde bir sonraki rastgele müziği çalmak için coroutine başlat
        StartCoroutine(WaitForMusicToEnd());
    }

    IEnumerator WaitForMusicToEnd()
    {
        while (_music.isPlaying)
        {
            yield return null;
        }
        
        // Müzik bittiğinde bir sonraki rastgele müziği çal
        PlayRandomMusic();
    }
}
