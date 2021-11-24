using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class RespawnScript : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GrapplingGun grapplingGun;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            grapplingGun.StopGrapple();
            player.transform.position = respawnPoint.transform.position;
            Physics.SyncTransforms();
            Debug.Log("PLAYER IS DEAD");
        }
    }
}
