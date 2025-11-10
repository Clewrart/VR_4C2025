using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunhou : MonoBehaviour
{
    GameObject gun;
    public Material original;
    public Material green;
    public GameObject LeftHand;
    public GameObject RightHand;
    // Start is called before the first frame update
    void Start()
    {
        gun = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "hand")
        {
            gun.GetComponent<gun>().shou += 1;
        }
        //检查触发器是否与右手或左手接触
        if (other.gameObject.name == "RightHand")
        {
            RightHand.GetComponent<Renderer>().material = green;
        }
        else if (other.gameObject.name == "LeftHand")
        {
            LeftHand.GetComponent<Renderer>().material = green;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "hand")
        {
            gun.GetComponent<gun>().shou -= 1;
        }
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
