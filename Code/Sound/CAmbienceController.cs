using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class CAmbienceController : MonoBehaviour
{

    public static CAmbienceController Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Destroy(this);
        }
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    private StudioEventEmitter m_HeartbeatEmitter = null;

    //[FMODUnity.EventRef]
    //[SerializeField] private string m_sWindEvent = string.Empty;

    [SerializeField] private float m_fMinRandomWindInterval = 10.0f;
    [SerializeField] private float m_fMaxRandomWindInterval = 20.0f;

    private float m_fTimer = 0.0f;
    private float m_fRandomRange;

    void Start()
    {
        m_HeartbeatEmitter = GetComponent<StudioEventEmitter>();
        //if(m_HeartbeatEmitter == null)
        //{
        //    print("Heartbeat doesn't exist");
        //}

        //if(m_sWindEvent == string.Empty)
        //{
        //    print("No wind event has been set!");
        //}

        m_fRandomRange = Random.Range(m_fMinRandomWindInterval, m_fMaxRandomWindInterval);
    }

    
    void Update()
    {
        //m_HeartbeatEmitter.SetParameter("Health", (float)m_PlayerHealth.GetCurrentHP() / m_PlayerHealth.m_iMaxHP);
        //m_fTimer += Time.deltaTime;

        //if(m_fTimer >= m_fRandomRange)
        //{
        //    RuntimeManager.PlayOneShot(m_sWindEvent);

        //    m_fRandomRange = Random.Range(m_fMinRandomWindInterval, m_fMaxRandomWindInterval);
        //}
    }

    public void SetHeartbeatParameter(float PercentageHP)
    {
        m_HeartbeatEmitter.SetParameter("Heart_Beat", 100.0f - 100.0f * PercentageHP);
    }
}
