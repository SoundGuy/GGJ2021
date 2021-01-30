using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip Song1;
    [SerializeField] private AudioClip Song2;

    [SerializeField] private bool quarantine;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(quarantine ?  Song2 : Song1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
