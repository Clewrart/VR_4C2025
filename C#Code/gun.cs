using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    public int shou = 0;
    GameObject qian;
    GameObject hou;
    GameObject zhong;
    public Material original;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject effect;
    public GameObject finish;
    public bool isActive = false;
    private AudioSource audioSource;
    public AudioClip thisAudio;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<OVRGrabbable>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = true;//禁用物理交互
        this.GetComponent<Collider>().enabled = false;//禁用碰撞器交互
        qian = this.transform.GetChild(0).gameObject;
        hou = this.transform.GetChild(1).gameObject;
        zhong = this.transform.GetChild(2).gameObject;

        //添加 AudioSource 组件并设置参数
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f;//设置为 2D 音效，不受距离影响
        audioSource.volume = 1.0f;
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shou == 2)
        {
            shou = 0;
            this.GetComponent<OVRGrabbable>().enabled = true;
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.GetComponent<Collider>().enabled = true;
            Destroy(qian);
            Destroy(hou);
            Destroy(zhong);
            Invoke("returnO", 1.5f);
        }
    }

    void returnO()
    {
        LeftHand.GetComponent<Renderer>().material = original;
        RightHand.GetComponent<Renderer>().material = original;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "tie")
        {
            effect.SetActive(true);
            var ps = effect.GetComponent<ParticleSystem>();
            ps.Play();
            audioSource.clip = thisAudio;
            audioSource.Play();
            if (isActive == false)
            {
                Invoke("Finish", 1.5f);
            }
        }
    }

    void Finish()
    {
        finish.SetActive(true);
        isActive = true;
    }
}
