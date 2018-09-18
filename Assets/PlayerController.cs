using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField, Range(1, 5)] private float _cameraSens;
    
    [SerializeField] private Insect _playerObj;

    [SerializeField] private Camera _camera;

    private Vector3 _dir;

    private float _rot;

    // Update is called once per frame
    void Update()
    {
        _dir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");

        _rot = Input.GetAxis("Mouse X") * _cameraSens;

        _camera.transform.rotation =
            Quaternion.Euler(_camera.transform.rotation.eulerAngles + Input.GetAxis("Mouse Y") * _cameraSens * Vector3.right);

        if (Input.GetButtonDown("Jump")) _playerObj.Jump(5);
    }

    private void FixedUpdate()
    {
        _playerObj.Move(_dir, _rot);
    }
}