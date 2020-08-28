using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CClipSettings : MonoBehaviour
{
    [SerializeField] private float fCullingSize = 250.0f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float fNoiseAmount = 0.5f;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float fShowNoise = 0.0f;

    [Range(-1.0f, 1.0f)]
    [SerializeField] private float fClipOffsetX = 0.0f;

    [Range(-1.0f, 1.0f)]
    [SerializeField] private float fClipOffsetY = 0.0f;

    private Transform m_PlayerTransform;

    Camera m_MainCamera;
    
    void Awake()
    {
        m_MainCamera = Camera.main;        
    }

    void Start()
    {
        m_PlayerTransform = CPlayerControlls.GetPlayer().transform;

        //These values can be set during start
        Shader.SetGlobalFloat("_ClipCullSize", fCullingSize);
        Shader.SetGlobalFloat("_ClipNoiseAmount", fNoiseAmount);
        Shader.SetGlobalFloat("_ShowNoise", fShowNoise);

        Vector2 Vec2_ClipOffset = new Vector2(fClipOffsetX, fClipOffsetY);
        Shader.SetGlobalVector("_ClipOffset", Vec2_ClipOffset);

        Shader.SetGlobalVector("_PlayerPos", m_PlayerTransform.position);
    }

    
    
    void Update()
    {
        /*
        Shader.SetGlobalFloat("_ClipCullSize", fCullingSize);
        Shader.SetGlobalFloat("_ClipNoiseAmount", fNoiseAmount);
        Shader.SetGlobalFloat("_ShowNoise", fShowNoise);

        Vector2 Vec2_ClipOffset = new Vector2(fClipOffsetX, fClipOffsetY);
        Shader.SetGlobalVector("_ClipOffset", Vec2_ClipOffset);

        Shader.SetGlobalVector("_PlayerPos", m_PlayerTransform.position);
        */

        //Players screen space position needs to be updated every frame to function correctly
        Vector3 PlayerViewPos = m_MainCamera.WorldToViewportPoint(m_PlayerTransform.transform.position);
        Shader.SetGlobalVector("_PlayerView", PlayerViewPos);
    }
}
