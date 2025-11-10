using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunzhong : MonoBehaviour
{
    public Material original;
    public Material red;
    public GameObject LeftHand;
    public GameObject RightHand;

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
        //检查触发器是否与右手或左手接触
        if (other.gameObject.name == "RightHand")
        {
            RightHand.GetComponent<Renderer>().material = red;
        }
        else if (other.gameObject.name == "LeftHand")
        {
            LeftHand.GetComponent<Renderer>().material = red;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //检查触发器是否与右手或左手离开
        if (other.gameObject.name == "RightHand")
        {
            RightHand.GetComponent<Renderer>().material = original;
        }
        else if (other.gameObject.name == "LeftHand")
        {
            LeftHand.GetComponent<Renderer>().material = original;
        }
    }
}
