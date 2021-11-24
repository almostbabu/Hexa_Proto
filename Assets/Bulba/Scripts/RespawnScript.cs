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
            grapplingGun.StopGrapple();
            Physics.SyncTransforms();
            Debug.Log("PLAYER IS DEAD");
        }
    }
}
