using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip Song1;
    [SerializeField] private AudioClip Song2;
    
    [SerializeField] private AudioClip BGMusic;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private bool quarantine;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(quarantine ?  Song2 : Song1);
        
        DontDestroyOnLoad(this);
        //StartCoroutine(PlayBGMusic());
    }

   

    // Update is called once per frame
    void Update()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = BGMusic;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
