using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AudioManager : MonoBehaviour
{
    static AudioSource audioSource;
    static Dictionary<string, AudioClip> audioClips =
        new Dictionary<string, AudioClip>();
    void Awake(){
        audioSource = gameObject.GetComponent<AudioSource>();
        Initialize();
        Player.StarCollected += Play;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Initialize()
    {
        audioClips.Clear();
        audioClips.Add("collected", Resources.Load<AudioClip>("Sounds/collected"));
    }
    public void Play(object sender, EventArgs e)
    {
        audioSource.PlayOneShot(audioClips["collected"]);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
