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

//    [SerializeField] private QuarantineElement _quarantineElement;
    private static MusicManager _instance;
    // Start is called before the first frame update
    void Start()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

  //      quarantine = _quarantineElement.enabled && _quarantineElement.gameObject.activeInHierarchy;
        
        _audioSource = GetComponent<AudioSource>();
        AudioClip clipToPlay = quarantine ? Song2 : Song1;
        Debug.Log("Playing clip " + clipToPlay);
        _audioSource.PlayOneShot(clipToPlay );
        
//        DontDestroyOnLoad(this);
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
