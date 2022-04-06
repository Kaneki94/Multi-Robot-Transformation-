using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public MeshRenderer tankTreadMesh, tankTreadMesh2;
    public float treadSpeed = 10f;

    private Material TreadMaterial, TreadMaterial2;
    private Rigidbody _rigidbody;
    float treadOffset = 0;


    void Awake()
    {
        TreadMaterial = tankTreadMesh.material;
        TreadMaterial2 = tankTreadMesh2.material;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        Vector3 locVel = transform.InverseTransformDirection(_rigidbody.velocity);

        treadOffset -= locVel.z * treadSpeed * Time.deltaTime;
        TreadMaterial.mainTextureOffset = new Vector2(0, treadOffset);
        TreadMaterial2.mainTextureOffset = new Vector2(0, treadOffset);
        if (treadOffset < -1)
            treadOffset = 0;
    }
}
