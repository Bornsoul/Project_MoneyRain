using UnityEngine;
using System.Collections;

public class JARotationObj : MonoBehaviour
{
    public Vector3 m_stRot;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(m_stRot * Time.deltaTime);
    }
}
