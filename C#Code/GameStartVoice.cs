using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameStartVoice : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip audioClip;
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(audioClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
