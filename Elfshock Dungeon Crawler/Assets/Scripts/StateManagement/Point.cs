using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.isTrigger == false)
        {
           //Debug.Log("Coin Collected");

           other.GetComponent<PlayerControllerRigid>().points++;

           Destroy(gameObject);
        }
    }
}
