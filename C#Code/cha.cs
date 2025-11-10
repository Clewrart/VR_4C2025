using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cha : MonoBehaviour
{
    public GameObject otherTeapot;//用于存储另一个茶壶
    public GameObject finish;
    public Material touming;
    public GameObject chagai;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "gai")
        {
            Transform child2 = gameObject.transform.Find("水蒸气特效");
            Transform child3 = gameObject.transform.Find("水蒸气特效2");

            if (chagai != null)
            {
                chagai.SetActive(true); // 激活茶盖

                //激活当前茶壶的子对象
                other.gameObject.GetComponent<Renderer>().material = touming;//将当前茶壶的材质设置为透明
                //other的子对象设置为透明材质
                other.transform.GetChild(0).GetComponent<Renderer>().material = touming;//将当前茶壶的子对象的材质设置为透明

                if (child2 != null)
                {
                    child2.gameObject.SetActive(true);//激活当前茶壶的水蒸气特效
                }

                if (otherTeapot != null)
                {
                    Transform otherTeapotChild3 = otherTeapot.transform.Find("水蒸气特效2");
                    if (otherTeapotChild3 != null)
                    {
                        otherTeapotChild3.gameObject.SetActive(true);//激活另一个茶壶的水蒸气特效
                    }
                }
            }
            Invoke("Finish", 6f);
        }
    }

    void Finish(){
        finish.SetActive(true);
    }
}
