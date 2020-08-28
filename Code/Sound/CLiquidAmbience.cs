using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLiquidAmbience : MonoBehaviour
{

    private Collider m_ParentCollider;
    private Transform m_PlayerTransform;

    // Start is called before the first frame update
    void Start()
    {
        m_ParentCollider = transform.parent.gameObject.GetComponent<Collider>();
        //if(m_ParentCollider == null)
        //{
        //    print("Parent doesn't have a collider");
        //}

        m_PlayerTransform = CPlayerControlls.GetPlayer().transform;
        //if(m_PlayerTransform == null)
        //{
        //    print("Player transform couldn't be found");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_ParentCollider.ClosestPoint(m_PlayerTransform.position);
    }
}
