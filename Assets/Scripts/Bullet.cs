using System.Collections.Specialized;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rgd;

    [SerializeField] private MeshFilter _mesh;

    [SerializeField] private Mesh _bullet, _decal, _web, _captured;

    public enum BulletType
    {
        WebBullet,
        WebGrab
    }

    [SerializeField] private BulletType _bulletType;

    private float speed;

    /// <summary>
    /// Fires the gameobject towards @direction from @position at @speed
    /// </summary>
    /// <param name="bulletType">Type of the bullet to be fired</param>
    /// <param name="position">Position of the fire point in world space</param>
    /// <param name="direction">Direction to be fired in world space</param>
    /// <param name="speed">Speed at which to be fired at</param>
    public void Fire(BulletType bulletType, Vector3 position, Vector3 direction, float speed)
    {
        _rgd.transform.parent = null;

        if (bulletType == BulletType.WebBullet)
            _mesh.mesh = _bullet;
        else
            _mesh.mesh = _web;

        _rgd.isKinematic = false;
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.Rotate(Vector3.up, -90);
        _rgd.velocity = speed * transform.right;

        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (_bulletType)
        {
            case BulletType.WebBullet:
                _rgd.isKinematic = true;
                transform.parent = other.gameObject.transform;
                _mesh.mesh = _decal;
                break;
            case BulletType.WebGrab:
                if (other.gameObject.CompareTag("enemy"))
                {
                    other.collider.enabled = false;
                    other.transform.parent = this.transform;
                    other.rigidbody.isKinematic = true;
                    _rgd.velocity = _rgd.transform.forward * speed;
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