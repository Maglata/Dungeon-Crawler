using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject target;

    [SerializeField] private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + offset;
    }
}
