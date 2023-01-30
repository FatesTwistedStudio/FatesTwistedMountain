using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class S_SurfaceAlignment : MonoBehaviour
{
    [SerializeField]
    private Transform playerModel;
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private AnimationCurve anim;
    [SerializeField]
    private float _time;
    [SerializeField]
    private float _modelTime;

    private void Update()
    {
        SurfaceAlignment();
    }

    private void SurfaceAlignment()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit info = new RaycastHit();
        Quaternion RotationRef = Quaternion.identity;

        if (Physics.Raycast(ray, out info, 1.5f, ground))
        {
            RotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, info.normal), anim.Evaluate(_time));
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-RotationRef.eulerAngles.x, transform.rotation.eulerAngles.y, RotationRef.eulerAngles.z), _time);
            playerModel.rotation = Quaternion.Lerp(playerModel.transform.rotation, Quaternion.Euler(-RotationRef.eulerAngles.x, transform.rotation.eulerAngles.y, RotationRef.eulerAngles.z), _modelTime);
        }
    }
}