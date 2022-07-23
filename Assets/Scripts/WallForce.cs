using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallForce : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ObjectPrefab"))
        {
            collision.transform.position = new Vector3(Random.Range(-3, 4), 3.0f, Random.Range(-13, -7));
        }
    }
}
