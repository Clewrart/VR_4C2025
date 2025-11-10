using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class change_mesh : MonoBehaviour
{
    public Mesh[] shapeStates;//存储不同形状
    public Vector3[] ScaleStates;//存储不同大小
    public ParticleSystem[] particleEffects;//粒子特效数组
    private int shape = 0;//当前变形状态索引
    private float cooldownDuration = 2f;//冷却时间，单位为秒
    public GameObject thing;//需要改变的物体
    private AudioSource audioSource;
    public AudioClip thisAudio;
    public GameObject finish;
    public GameObject pan;
    public GameObject sinan;
    public Material touming;
    float nowtime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //添加AudioSource组件并设置参数
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f;//设置为 2D 音效，不受距离影响
        audioSource.volume = 1.0f;
        audioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        nowtime += Time.deltaTime;
    }

    //物体进入触发器范围
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("change") & nowtime > 2f)  
        {   
            nowtime = 0f;
            shape++;
            StartCoroutine(HandleCollision());
        }
        if (other.gameObject.tag == "pan")
        {
            if (shape != 2)
            {
                shape = 0;
                StartCoroutine(HandleCollision());
                audioSource.clip = thisAudio;
                audioSource.Play();
            } else
            {
                pan.SetActive(false);
                sinan.SetActive(true);
                thing.GetComponent<Renderer>().material = touming;
                thing.transform.position = new Vector3(0, 0, 0);
                Invoke("Finish", 2f);
            }
        }
    }

    //执行变形和粒子特效
    private IEnumerator HandleCollision()
    {
        //执行变形
        if (shape < shapeStates.Length - 1)
        {
            ChangeShapeAndSize(shape);//根据当前索引改变形状和大小
        }

        //等待冷却时间结束
        yield return new WaitForSeconds(cooldownDuration);

    }

    //改变形状和大小
    void ChangeShapeAndSize(int shapeIndex)
    {
        //改变形状
        thing.GetComponent<MeshFilter>().mesh = shapeStates[shapeIndex];
        //改变大小
        if (ScaleStates != null && shapeIndex < ScaleStates.Length)
        {
            thing.transform.localScale = ScaleStates[shapeIndex];
        }

    }

    void Finish()
    {
        finish.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
