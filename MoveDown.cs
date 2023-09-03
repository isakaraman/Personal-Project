using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float zDestroyPos;

    private Rigidbody objRigid;
    void Start()
    {
        objRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        objRigid.AddForce(Vector3.forward *speed);

        if (transform.position.z < zDestroyPos)
        {
            Destroy(gameObject);
        }
    }
}
