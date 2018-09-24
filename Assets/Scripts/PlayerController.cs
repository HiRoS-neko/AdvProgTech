using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //clamp from 0 to 50

    private Vector3 _dir;
    [SerializeField] private Insect _playerObj;

    private float _rot;

    private bool _secondary;

    // Update is called once per frame
    private void Update()
    {
        _dir = (Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal"))
            .normalized;

        if (Input.GetButtonDown("Jump")) _playerObj.Jump(5);

        if (Input.GetButtonDown("Fire1"))
            _playerObj.Attack(_playerObj.transform.position + _playerObj.transform.forward * 50,
                Bullet.BulletType.WebBullet);

        if (_secondary && Input.GetButtonDown("Fire2"))
            _playerObj.Attack(_playerObj.transform.position + _playerObj.transform.forward * 50,
                Bullet.BulletType.WebGrab);
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