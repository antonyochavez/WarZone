using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningProjectile : MonoBehaviour
{
    [SerializeField] float spinningSpeed = 45f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, spinningSpeed * Time.deltaTime);
    }
}
