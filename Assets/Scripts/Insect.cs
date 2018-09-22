using UnityEngine;

public class Insect : MonoBehaviour
{
    [SerializeField] private Rigidbody _rgd;
    [SerializeField, Range(0, 5)] private float _speed;
    [SerializeField, Range(0, 5)] private float _fireSpeed;

    [SerializeField] private GameObject _insectBody;
    [SerializeField] private GameObject _shootLocation;

    [SerializeField] private BulletPool _bulletPool;

    // Use this for initialization
    void Start()
    {
        if (_rgd == null) _rgd = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 dir)
    {
        if (dir.magnitude >= 0.02f)
            _insectBody.transform.rotation =
                Quaternion.Slerp(_insectBody.transform.rotation, Quaternion.LookRotation(dir), 0.6f);
        _rgd.MovePosition(transform.position + dir * _speed);
        //Add force to insect
    }

    public void Jump(float force)
    {
        _rgd.AddForce(transform.up * force, ForceMode.Impulse);
    }

    public void Attack(Vector3 point, Bullet.BulletType bulletType)
    {
        _bulletPool.FireBullet(_shootLocation.transform.position, _insectBody.transform.forward, _fireSpeed, bulletType);
    }
}