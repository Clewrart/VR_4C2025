using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuiHua_chashi_finish : MonoBehaviour
{
    public AudioClip[] audios;
    public GameObject[] images;
    private AudioSource audioSource;
    int index = 0;
    int num;
    float time_now;
    public bool is_running = false;
    bool is_trig = false;

    void OnTriggerEnter(Collider other)
    {
        
    }

    void Start()
    {
        num = audios.Length;
        time_now = 0f;

        //添加 AudioSource 组件并设置参数
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f;//设置为 2D 音效，不受距离影响
        audioSource.volume = 1.0f;
        audioSource.playOnAwake = false;

        is_trig = true;
        time_now = audios[index].length;
        audioSource.clip = audios[index];
        audioSource.Play();
        images[index].SetActive(true);
        is_running = true;
    }

    void Update()
    {
        if (is_running && is_trig)
        {
            time_now -= Time.deltaTime;
            if (time_now <= 0)
            {
                images[index].SetActive(false);
                index++;
                if (index == num)
                {
                    is_trig = false;
                    this.gameObject.SetActive(false);
                }
                is_running = false;
            }
        }
        else if (!is_running && index < num && is_trig)
        {
            time_now = audios[index].length;
            audioSource.clip = audios[index];
            audioSource.Play();
            images[index].SetActive(true);
            is_running = true;
        }
    }
}
