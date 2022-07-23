using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCubeRotate : MonoBehaviour
{
    private Vector3 rotateVector = new Vector3(0, -1, 0);

    [SerializeField] private float rotateSpeed;

    void FixedUpdate()
    {
        transform.Rotate(rotateVector * (rotateSpeed * Time.deltaTime));
    }
}
