using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CClipBox : MonoBehaviour
{
    private Transform m_PlayerTransform;
    
    private Vector3 m_StartPos;
    private void Start()
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(0.0f, -90.0f, 0.0f);
        m_StartPos = new Vector3(-18.84f, 8.27f, -4.79f);

        m_PlayerTransform = CPlayerControlls.GetPlayer().transform;
    }

    private void Update()
    {
        transform.position = m_StartPos + m_PlayerTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.GetComponent<IWall>() != null)
            other.gameObject.GetComponent<IWall>().SetClipMaterial();
        
    }

    

    private void OnTriggerExit(Collider other)
    {
        
        if(other.gameObject.GetComponent<IWall>() != null)
            other.gameObject.GetComponent<IWall>().SetBaseMaterial();
        
    }
}
