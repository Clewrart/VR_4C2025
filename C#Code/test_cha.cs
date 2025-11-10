using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_cha : MonoBehaviour
{
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
        Transform child=other.gameObject.transform.Find("茶盖");
        if(child != null && child.gameObject.name=="茶盖")
        {
            child.gameObject.SetActive(true);
            Destroy(gameObject);
        }
        
    }
}
