using UnityEngine;
using UnityEngine.Networking;

public class WebGrab : Bullet
{
    [SerializeField] private Mesh _web, _captured;


    [SerializeField] private MeshFilter _mesh;
    [SerializeField] private Rigidbody _rgd;

    private float _speed;


    private bool _move;

    private void Update()
    {
        if (_move)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            //transform.RotateAround(Vector3.zero, Vector3.forward, 2);
            //transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 2);
        }
    }


    /// <summary>
    ///     Fires the gameobject towards @direction from @position at @speed
    /// </summary>
    /// <param name="position">Position of the fire point in world space</param>
    /// <param name="direction">Direction to be fired in world space</param>
    /// <param name="speed">Speed at which to be fired at</param>
    public void Fire(Vector3 position, Vector3 direction, float speed, LayerMask layerMask)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var temp = transform.GetChild(0);
            temp.GetComponent<Collider>().enabled = true;
        }


        _mesh.mesh = _web;

        transform.DetachChildren();

        _rgd.transform.parent = null;

        _speed = speed;

        transform.position = position;
        transform.rotation = Quaternion.LookRotation(direction);
        //_rgd.velocity = speed * transform.forward;

        gameObject.SetActive(true);
        _move = true;
    }

    private void OnCollisionEnter(Collision other)
    {
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
            _move = false;
        }
    }
}