using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource currentMusic;

    [SerializeField] private AudioSource musicObject;
    [SerializeField] private AudioClip[] musicList;

    private void Awake()
    {
        currentMusic = Instantiate(musicObject, transform.position, Quaternion.identity);
        
    }

    private void Start()
    {
        
        
    }

    private void FixedUpdate()
    {
        if (!currentMusic.isPlaying)
        {
            PlayRandomMusicClip();
        }
    }


    public void PlayRandomMusicClip()
    {
        int rand = Random.Range(0, musicList.Length);
        currentMusic.clip = musicList[rand];
        currentMusic.volume = 1f;
        currentMusic.Play();
    }
}
