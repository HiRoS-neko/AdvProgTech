using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    private void Start()
    {
        if (_player == null)
        {
            Debug.LogWarning("Camera was not assigned a player object, now looking for player within the scene");
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        if (_player == null)
        {
            Debug.LogError("Player was not successfully found in scene");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }

    // Update is called once per frame
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation((_player.transform.position - transform.position).normalized);
        //transform.LookAt(_player.transform.position.z * Vector3.forward + _player.transform.position.y * Vector3.up +transform.position.x * Vector3.right);
    }
}