using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        WebBullet,
        WebGrab
    }

    [SerializeField] private Mesh _bullet, _decal, _web, _captured;

    [SerializeField] private BulletType _bulletType;

    [SerializeField] private MeshFilter _mesh;
    [SerializeField] private Rigidbody _rgd;

    private float _speed;

    /// <summary>
    ///     Fires the gameobject towards @direction from @position at @speed
    /// </summary>
    /// <param name="bulletType">Type of the bullet to be fired</param>
    /// <param name="position">Position of the fire point in world space</param>
    /// <param name="direction">Direction to be fired in world space</param>
    /// <param name="speed">Speed at which to be fired at</param>
    public void Fire(BulletType bulletType, Vector3 position, Vector3 direction, float speed)
    {
        _bulletType = bulletType;

        for (var i = 0; i < transform.childCount; i++)
        {
            var temp = transform.GetChild(0);
            temp.GetComponent<Collider>().enabled = true;
        }


        transform.DetachChildren();

        _rgd.transform.parent = null;

        _speed = speed;

        if (bulletType == BulletType.WebBullet)
            _mesh.mesh = _bullet;
        else
            _mesh.mesh = _web;

        _rgd.isKinematic = false;
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(direction);
        _rgd.velocity = speed * transform.forward;

        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (_bulletType)
        {
            case BulletType.WebBullet:
                if (other.gameObject.CompareTag("wall") || other.gameObject.CompareTag("floor") ||
                    other.gameObject.CompareTag("enemy"))
                {
                    _rgd.isKinematic = true;
                    transform.parent = other.gameObject.transform;
                    _mesh.mesh = _decal;
                }

                break;
            case BulletType.WebGrab:
                if (other.gameObject.CompareTag("enemy"))
                {
                    other.collider.enabled = false;
                    other.transform.parent = transform;
                    other.rigidbody.isKinematic = true;
                    _rgd.velocity = _rgd.transform.forward * _speed;
                }


                if (other.gameObject.CompareTag("wall") || other.gameObject.CompareTag("floor"))
                {
                    _rgd.isKinematic = true;
                    transform.parent = other.gameObject.transform;
                    _mesh.mesh = _captured;
                }

                break;
        }
    }
}