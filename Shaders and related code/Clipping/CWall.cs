using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWall : MonoBehaviour, IWall
{
    //Public so they can be reached by MeshCombineWizard
    public Material m_BaseMaterial = null;
    public Material m_ClipMaterial = null;

    private MeshRenderer m_MeshRenderer = null;
    
    void Start()
    {

        m_MeshRenderer = GetComponent<MeshRenderer>();
        //if(m_MeshRenderer == null)
        //{
        //    print(gameObject.name + " doesn't have a Mesh Renderer component!");
        //}

        if(m_BaseMaterial == null)
           m_BaseMaterial = m_MeshRenderer.material;

        //Failsafe incase someone forgets to set a clip material
        if(m_ClipMaterial == null)
            m_ClipMaterial = m_MeshRenderer.material;

    }

    public void SetBaseMaterial()
    {
        if (m_MeshRenderer != null)
        {
            m_MeshRenderer.material = m_BaseMaterial;
        }
    }

    public void SetClipMaterial()
    {
        if (m_MeshRenderer != null)
            m_MeshRenderer.material = m_ClipMaterial;
    }
}
