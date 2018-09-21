using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_player.transform.position);
    }
}