using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation((_player.transform.position - transform.position).normalized);
        //transform.LookAt(_player.transform.position.z * Vector3.forward + _player.transform.position.y * Vector3.up +transform.position.x * Vector3.right);
    }
}