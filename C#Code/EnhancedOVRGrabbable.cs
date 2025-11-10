using System;
using UnityEngine;

public class EnhancedOVRGrabbable : OVRGrabbable
{
    [SerializeField]
    private GameObject duplicatePrefab;
    private GameObject duplicateObject;
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        if (duplicatePrefab != null)
        {
            duplicateObject = Instantiate(duplicatePrefab, transform.position, transform.rotation);
            duplicateObject.transform.localScale = transform.localScale;
            duplicateObject.name = gameObject.name + "_Duplicate";
            MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                MonoBehaviour duplicateScript = duplicateObject.AddComponent(script.GetType()) as MonoBehaviour;
                if (duplicateScript != null)
                {
                    System.Reflection.FieldInfo[] fields = script.GetType().GetFields();
                    foreach (System.Reflection.FieldInfo field in fields)
                    {
                        field.SetValue(duplicateScript, field.GetValue(script));
                    }
                }
            }
            OVRGrabbable grabbable = duplicateObject.GetComponent<OVRGrabbable>();
            if (grabbable != null)
            {
                grabbable.GrabBegin(hand, grabPoint);
            }
        }
    }
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        if (duplicateObject != null)
        {
            Destroy(duplicateObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Terrain"))
        {
            Destroy(duplicateObject);
        }
    }
}
