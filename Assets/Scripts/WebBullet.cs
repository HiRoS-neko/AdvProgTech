using UnityEngine;

public class WebBullet : Bullet
{
    [SerializeField] private Mesh _bullet, _decal;


    [SerializeField] private MeshFilter _mesh;
    [SerializeField] private Rigidbody _rgd;

    private float _speed;

    /// <summary>
    ///     Fires the gameobject towards @direction from @position at @speed
    /// </summary>
    /// <param name="position">Position of the fire point in world space</param>
    /// <param name="direction">Direction to be fired in world space</param>
    /// <param name="speed">Speed at which to be fired at</param>
    /// <param name="layerMask"></param>
    public void Fire(Vector3 position, Vector3 direction, float speed, LayerMask layerMask)
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var temp = transform.GetChild(0);
            temp.GetComponent<Collider>().enabled = true;
        }

        gameObject.layer = layerMask;
        _mesh.mesh = _bullet;

        transform.DetachChildren();

        _rgd.transform.parent = null;

        _speed = speed;

        _rgd.isKinematic = false;
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(direction);
        _rgd.velocity = speed * transform.forward;

        gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("wall") || other.gameObject.CompareTag("floor")||
            other.gameObject.CompareTag("enemy"))
        {
            if (other.gameObject.CompareTag("enemy"))
            {
                var enemy = other.gameObject.GetComponent<LadyBugAi>();
                enemy.Health -= 25;
            }
            _rgd.isKinematic = true;
            transform.parent = other.gameObject.transform;
            _mesh.mesh = _decal;
        }
        
    }
}