using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Insect _playerObj;
    //clamp from 0 to 50

    private Vector3 _dir;

    private bool _secondary = false;

    private float _rot;

    // Update is called once per frame
    void Update()
    {
        _dir = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal"))
            .normalized;

        if (Input.GetButtonDown("Jump")) _playerObj.Jump(5);

        if (Input.GetButtonDown("Fire1"))
        {
            _playerObj.Attack(_playerObj.transform.position + _playerObj.transform.forward * 50,
                Bullet.BulletType.WebBullet);
        }

        if (_secondary && Input.GetButtonDown("Fire2"))
        {
            _playerObj.Attack(_playerObj.transform.position + _playerObj.transform.forward * 50,
                Bullet.BulletType.WebGrab);
        }
    }

    private void FixedUpdate()
    {
        _playerObj.Move(_dir);
    }

    public void EnableSecondary()
    {
        _secondary = true;
    }
}