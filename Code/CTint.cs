using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTint : MonoBehaviour
{

    public Color m_TintColor;

    private Color m_BaseColor;
    private Color m_TintedBaseColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

    private Renderer m_Renderer;
    private MaterialPropertyBlock m_PropertyBlock;


    void Awake()
    {

        m_PropertyBlock = new MaterialPropertyBlock();
        m_Renderer = GetComponent<Renderer>();

        m_Renderer.GetPropertyBlock(m_PropertyBlock);

        m_BaseColor = m_Renderer.material.color;
        m_PropertyBlock.SetColor("_BaseColor", m_TintColor);

        m_Renderer.SetPropertyBlock(m_PropertyBlock);

        m_TintedBaseColor.a = m_BaseColor.a;
    }

    //We don't want to change color every frame outside of the editor
#if UNITY_EDITOR

    void Update()
    {
        m_Renderer.GetPropertyBlock(m_PropertyBlock);

        m_TintedBaseColor.r = ((m_TintColor.r - m_BaseColor.r) * 1.0f + m_BaseColor.r);
        m_TintedBaseColor.g = ((m_TintColor.g - m_BaseColor.g) * 1.0f + m_BaseColor.g);
        m_TintedBaseColor.b = ((m_TintColor.b - m_BaseColor.b) * 1.0f + m_BaseColor.b);

        //m_TintColor.r = Mathf.Sin(Time.time);
        //m_TintColor.g = Mathf.Sin(Time.time);
        //m_TintColor.b = Mathf.Sin(Time.time);
        m_PropertyBlock.SetColor("_BaseColor", m_TintedBaseColor);

        m_Renderer.SetPropertyBlock(m_PropertyBlock);
    }

#endif
}
