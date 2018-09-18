using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    private Bullet[] _bulletArray;

    private int _index = 0;

    private void Start()
    {
        _bulletArray = new Bullet[10];

        for (int i = 0; i < _bulletArray.Length; i++)
        {
            var temp = Instantiate(_bullet.gameObject);
            temp.SetActive(false);
            _bulletArray[i] = temp.GetComponent<Bullet>();
        }
    }

    public void FireBullet(Vector3 position, Vector3 direction, float speed)
    {
        _bulletArray[_index].Fire(Bullet.BulletType.WebBullet, position, direction, speed);
        _index++;
        if (_index >= _bulletArray.Length) _index = 0;
    }
}