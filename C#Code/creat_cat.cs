using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using OVR;

public class creat_cat : MonoBehaviour
{
    public GameObject[] cat;
    private int cat_index = 0;
    private bool get_hand;
    private int hands = 0;
    public AudioClip[] audios;
    public GameObject[] images;
    private AudioSource audioSource;
    int index = 0;
    float time_now;
    public bool is_running = false;
    private bool finish_talk;
    // Start is called before the first frame update
    void Start()
    {
        time_now = 0f;

        //添加 AudioSource 组件并设置参数
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f;//设置为 2D 音效，不受距离影响
        audioSource.volume = 1.0f;
        audioSource.playOnAwake = false;

        get_hand = false;
        finish_talk = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch) && hands > 0 && cat_index < 2)
        {
            if (finish_talk)
            {
                finish_talk = false;
                cat[cat_index].SetActive(false);
                cat_index++;
                index = 2*(cat_index-1);
                cat[cat_index].SetActive(true);
            }
        }
        if (is_running)
        {
            time_now -= Time.deltaTime;
            if (time_now <= 0)
            {
                images[index].SetActive(false);
                index++;
                if (index == 2*(cat_index-1)+2)
                {
                    finish_talk = true;
                }
                is_running = false;
            }
        }
        else if (!is_running && index < 2*(cat_index-1)+2)
        {
            time_now = audios[index].length;
            audioSource.clip = audios[index];
            audioSource.Play();
            images[index].SetActive(true);
            is_running = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hand")
        {
            hands++;
            get_hand = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "hand")
        {
            hands--;
            get_hand = false;
        }
    }
}
