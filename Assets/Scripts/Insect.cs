using System.Collections.Generic;
using UnityEngine;

public class Insect : MonoBehaviour
{
    public enum AnimationStates
    {
        Idle,
        Walking,
        Flying,
        Attack
    }

    [SerializeField] private List<AnimationStates> _animationStates = new List<AnimationStates>();

    [SerializeField] private Animator _animator;

    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] [Range(0, 5)] private float _fireSpeed;

    [SerializeField] private GameObject _insectBody;
    private bool _moving;
    [SerializeField] private Rigidbody _rgd;
    [SerializeField] private GameObject _shootLocation;
    [SerializeField] [Range(0, 5)] private float _speed;

    // Use this for initialization
    private void Start()
    {
        if (_rgd == null) _rgd = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 dir)
    {
        if (dir.magnitude >= 0.02f)
            _insectBody.transform.rotation =
                Quaternion.Slerp(_insectBody.transform.rotation, Quaternion.LookRotation(dir), 0.6f);
        //check vertical component, if greater then 0.5f, then jump
        if (dir.y > 0.5f) Jump(5);

        //remove vertical component and renormalize
        dir = Vector3.ProjectOnPlane(dir, Vector3.up).normalized;

        _moving = !Mathf.Approximately(dir.magnitude, 0);

        _rgd.MovePosition(transform.position + dir * _speed);
        //Add force to insect
    }

    public void Jump(float force)
    {
        if (Mathf.Abs(_rgd.velocity.y) < 0.1f)
            _rgd.AddForce(transform.up * force, ForceMode.Impulse);
    }

    public void AttackForward()
    {
        _bulletPool.FireBullet(_shootLocation.transform.position, _insectBody.transform.forward, _fireSpeed,
            Bullet.BulletType.WebBullet, LayerMask.NameToLayer("EnemyBullet"));
    }

    public void Attack(Vector3 point, Bullet.BulletType bulletType, LayerMask bulletMask)
    {
        if (_animationStates.Contains(AnimationStates.Attack))
        {
            _insectBody.transform.rotation =
                Quaternion.Slerp(_insectBody.transform.rotation,
                    Quaternion.LookRotation(transform.InverseTransformPoint(point).normalized), 1);
            _animator.SetTrigger("Attack");
        }
        else
            _bulletPool.FireBullet(_shootLocation.transform.position, _insectBody.transform.forward, _fireSpeed,
                bulletType, bulletMask);
    }

    private void Update()
    {
        if (_animator != null)
        {
            if (_animationStates.Contains(AnimationStates.Idle))
                _animator.SetBool("Idle", Mathf.Approximately(_rgd.velocity.magnitude, 0));
            if (_animationStates.Contains(AnimationStates.Walking))
                _animator.SetBool("Moving", _moving);
            if (_animationStates.Contains(AnimationStates.Flying))
                _animator.SetBool("Flying", Mathf.Abs(_rgd.velocity.y) > 0.1f);
        }
    }
}