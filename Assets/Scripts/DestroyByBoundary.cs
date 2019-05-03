using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    //Destroying any Other object crossing the boundary
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
