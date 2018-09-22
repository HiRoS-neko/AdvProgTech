using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private float _floatRange = 0.125f;

    private float _move = 0.002f;

    private Vector3 _initialPos;

    // Use this for initialization
    void Start()
    {
        _initialPos = transform.position;
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (gameObject != null)
        {
            transform.Rotate(Vector3.up, 2f);
            if (Vector3.Distance(_initialPos, transform.position) > _floatRange) _move *= -1;
            transform.Translate(Vector3.up * _move);
            yield return null;
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.gameObject.GetComponentsInParent<PlayerController>().First().EnableSecondary();
            Destroy(gameObject);
        }
    }
}