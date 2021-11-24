using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GrapplingGun grapplingGun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.transform.position;
            Debug.Log(other.GetComponent<Rigidbody>().velocity);
            Debug.Log(other.GetComponent<Rigidbody>());
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Debug.Log(other.GetComponent<Rigidbody>().velocity);
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Debug.Log(other.GetComponent<Rigidbody>().angularVelocity);
            grapplingGun.StopGrapple();
            Physics.SyncTransforms();
            Debug.Log("PLAYER IS DEAD");
        }
    }
}
