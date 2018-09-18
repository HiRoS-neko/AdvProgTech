using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(1, 5)] private float _cameraSens;

    [SerializeField] private Insect _playerObj;

    [SerializeField] private GameObject _camera;
    //clamp from 0 to 50

    private Vector3 _dir;

    private float _rot;

    // Update is called once per frame
    void Update()
    {
        _dir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");

        _rot = Input.GetAxis("Mouse X") * _cameraSens;

        //todo refactor this
        _camera.transform.localRotation = Quaternion.Euler(
            Mathf.LerpAngle(_camera.transform.localRotation.eulerAngles.x, Mathf.Clamp(
                    _camera.transform.localRotation.eulerAngles.x + Input.GetAxis("Mouse Y") * _cameraSens, 0, 50),
                0.5f) * Vector3.right);

        if (Input.GetButtonDown("Jump")) _playerObj.Jump(5);
    }

    private void FixedUpdate()
    {
        _playerObj.Move(_dir, _rot);
    }
}