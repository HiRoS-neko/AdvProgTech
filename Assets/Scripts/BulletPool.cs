using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private WebBullet _webBullet;
    [SerializeField] private WebGrab _webGrab;

    private WebBullet[] _bulletArray;
    private WebGrab[] _webBulletArray;

    private int _bulletIndex;
    private int _webIndex;

    [SerializeField] private int _numOfBullets;

    private void Start()
    {
        _bulletArray = new WebBullet[_numOfBullets];
        
        _webBulletArray = new WebGrab[_numOfBullets];

        for (var i = 0; i < _bulletArray.Length; i++)
        {
            var bullet = Instantiate(_webBullet.gameObject);
            bullet.SetActive(false);
            _bulletArray[i] = bullet.GetComponent<WebBullet>();
            
            var grab = Instantiate(_webGrab.gameObject);
            grab.SetActive(false);
            _webBulletArray[i] = grab.GetComponent<WebGrab>();
        }
    }

    public void FireBullet(Vector3 position, Vector3 direction, float speed, Bullet.BulletType bulletType, LayerMask layerMask)
    {
        switch (bulletType)
        {
            case Bullet.BulletType.WebBullet:
                _bulletArray[_bulletIndex].Fire(position, direction, speed, layerMask);
                _bulletIndex++;
                if (_bulletIndex >= _bulletArray.Length) _bulletIndex = 0;
                break;
            case Bullet.BulletType.WebGrab:
                _webBulletArray[_webIndex].Fire(position, direction, speed, layerMask);
                _webIndex++;
                if (_webIndex >= _webBulletArray.Length) _webIndex = 0;
                break;
        }

    }
}