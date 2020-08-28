using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWallMultiMaterial : MonoBehaviour, IWall
{

    [SerializeField] private List<Material> m_BaseMaterials = new List<Material>();
    [SerializeField] private List<Material> m_ClipMaterials = new List<Material>();

    private MeshRenderer m_MeshRenderer = null;
    private Material[] m_Materials;
    

    void Start()
    {

        m_MeshRenderer = GetComponent<MeshRenderer>();
        //if (m_MeshRenderer == null)
        //{
        //    print(gameObject.name + " doesn't have a Mesh Renderer component!");
        //}

        //Failsafes incase someone forgets to set multiple materials
        if(m_BaseMaterials.Count == 0)
        {
            m_BaseMaterials.Add(m_MeshRenderer.material);
        }

        if(m_ClipMaterials.Count == 0)
        {
            m_ClipMaterials.Add(m_MeshRenderer.material);
        }

        //Set m_Materials base value to the base materials
        m_Materials = new Material[m_BaseMaterials.Count];
        for (int i = 0; i < m_BaseMaterials.Count; ++i)
        {
            m_Materials[i] = m_BaseMaterials[i];
        }

    }

    public void SetBaseMaterial()
    {
        //Material[] m_Materials = new Material[m_BaseMaterials.Count];
        for (int i = 0; i < m_BaseMaterials.Count; ++i)
        {
            m_Materials[i] = m_BaseMaterials[i];
        }

        m_MeshRenderer.materials = m_Materials;
    }

    public void SetClipMaterial()
    {
        //Material[] m_Materials = new Material[m_ClipMaterials.Count];
        for (int i = 0; i < m_ClipMaterials.Count; ++i)
        {
            m_Materials[i] = m_ClipMaterials[i];
        }

        m_MeshRenderer.materials = m_Materials;
    }
}
