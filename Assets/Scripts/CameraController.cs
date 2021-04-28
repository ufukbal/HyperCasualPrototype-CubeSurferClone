using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    public Vector3 Offset;

    private void LateUpdate()
    {
        if (_target != null)
            FollowTarget();
    }
    private void FollowTarget()
    {
        transform.position = new Vector3(_target.position.x + Offset.x, transform.position.y, _target.position.z + Offset.z);
    }

}
