using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class see_bowuguan : MonoBehaviour
{
    public GameObject teacher;
    public Vector3 teacher_pos;
    public Vector3 teacher_rotation;
    public AudioClip door;
    public AudioClip[] audios;
    public GameObject[] images;
    public GameObject blackObject;
    private RawImage black; // 确保 black 是 RawImage 组件
    private AudioSource audioSource;
    public AudioSource doorAudioSource;
    int index = 0;
    int num;
    float time_now;
    public bool is_running = false;
    bool is_trig = false;
    public GameObject Player;
    public Vector3 player_pos;
    public Vector3 player_rotation;
    float speed;
    bool is_play;

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
        black = blackObject.GetComponent<RawImage>(); // 获取 RawImage 组件
        num = audios.Length;
        time_now = 0f;
        speed = 0.5f;
        is_play = false;

        //添加 AudioSource 组件并设置参数
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f; //设置为 2D 音效，不受距离影响
        audioSource.volume = 1.0f;
        audioSource.playOnAwake = false;

        // 初始化 doorAudioSource（专门用于 door）
        doorAudioSource = gameObject.AddComponent<AudioSource>();
        doorAudioSource.spatialBlend = 0f;
        doorAudioSource.playOnAwake = false;
    }

    void Update()
    {
        if (blackObject.activeInHierarchy) // 检查 RawImage 的 GameObject 是否激活
        {
            if (!is_play)
            {
                is_play = true;
                doorAudioSource.clip = door;
                doorAudioSource.Play();
            }
            Color currentColor = black.color; // 获取当前颜色
            currentColor.a += Time.deltaTime * speed; // 修改 alpha
            black.color = currentColor; // 重新赋值

            if (black.color.a >= 1f)
            {
                Player.transform.position = player_pos;
                Player.transform.rotation = Quaternion.Euler(player_rotation);
                speed = -speed; // 反向变化
            }
            if (black.color.a <= 0f && speed < 0)
            {
                black.gameObject.SetActive(false); // 关闭 GameObject
                this.gameObject.SetActive(false);
            }
        }

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
                    blackObject.SetActive(true); // 激活 GameObject
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