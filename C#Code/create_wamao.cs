using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_wamao : MonoBehaviour
{
    public GameObject teacher;
    public Vector3 teacher_pos;
    public Vector3 teacher_rotation;
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
        if (other.gameObject.tag == "Player" && !is_running && index == 0)
        {
            teacher.transform.position = teacher_pos;
            teacher.transform.rotation = Quaternion.Euler(teacher_rotation);
            is_trig = true;
            time_now = audios[index].length;
            audioSource.clip = audios[index];
            audioSource.Play();
            images[index].SetActive(true);
            is_running = true;
        }
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
