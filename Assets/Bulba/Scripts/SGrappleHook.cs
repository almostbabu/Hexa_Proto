using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGrappleHook : MonoBehaviour
{
    [SerializeField] private Transform _grapplingHook;
   [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _handpos;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private LayerMask _grappleLayer;
    [SerializeField] private float _maxGrappleDistance;
    [SerializeField] private float _hookSpeed;
    [SerializeField] private Vector3 _offset;

    private bool _isShooting, _isGrappling;
    private Vector3 _hookPoint;
   

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ShootHook();
        }

        if (_isGrappling)
        {
            _grapplingHook.position = Vector3.Lerp(_grapplingHook.position, _hookPoint, _hookSpeed * Time.deltaTime);
            if (Vector3.Distance(_grapplingHook.position, _hookPoint) < 0.5f)
            {
                _controller.enabled = false;
                _playerBody.position = Vector3.Lerp(_playerBody.position, _hookPoint - _offset, _hookSpeed * Time.deltaTime);
            }
        }
    }


    private void ShootHook()
    {

        if (_isShooting || _isGrappling) return;
        _isShooting = true;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, _maxGrappleDistance, _grappleLayer))
        {
            _hookPoint = hit.point;
            _isGrappling = true;
            _grapplingHook.parent = null;
            _grapplingHook.LookAt(_hookPoint);
            Debug.Log("Its a HIT");
        }

        _isShooting = false;
    }
}
