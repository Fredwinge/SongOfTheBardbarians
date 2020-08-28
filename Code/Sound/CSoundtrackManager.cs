using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class CSoundtrackManager : MonoBehaviour
{
    //TODO: IS A SINGLETON GOOD HERE?
    public static CSoundtrackManager Instance;

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

    [FMODUnity.EventRef]
    [SerializeField] private string m_MainSoundtrack = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_VictorySoundtrack = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_MenuSoundtrack = string.Empty;

    private FMODUnity.StudioEventEmitter m_SoundtrackEmitter;

    [SerializeField] private float m_fParameterValue;

    private readonly float m_fParameterMinValue = 0.0f;
    private readonly float m_fParameterMaxValue = 60.0f;

    private float m_fTimer = 0.0f;
    private float m_fUpdateFrequency = 2.0f;
    private float m_fPreferedValue = 0.0f;
    private readonly float m_fOutOfCombatValue = 50.0f;
    private readonly float m_fInCombatValue = 30.0f;
    private readonly float m_fInvestigatingValue = 10.0f;

    [SerializeField] private float m_fMaxBlendCorrectionTime = 2.0f;

    private bool m_bCorrection = false;


    private int m_DirectionMultiplier = 1;

    //TODO: REMOVE DEBUG VALUES
    //DEBUG VALUES
    [SerializeField] private int EnemiesInCombat = 0;
    [SerializeField] private int EnemiesInvestigating = 0;

    [SerializeField] private SoundtrackState m_SoundtrackState = SoundtrackState.Main;

    public enum SoundtrackState
    {
        Main,
        Victory,
        Menu
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_SoundtrackEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
        //if(m_SoundtrackEmitter == null)
        //{
        //    print("Couldn't find FMOD Studio Event Emitter on " + gameObject.name);
        //}
        //if(m_MainSoundtrack == string.Empty)
        //{
        //    print("Main soundtrack hasn't been set!");
        //}
        //if(m_VictorySoundtrack == string.Empty)
        //{
        //    print("Victory soundtrack hasn't been set!");
        //}

        SetSoundtrackState(m_SoundtrackState);

        m_fParameterValue = m_fOutOfCombatValue;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (m_SoundtrackState)
        {
            //Only update main soundtrack parameters if the main soundtrack is currently playing
            case SoundtrackState.Main:
                {
                    //TODO: DEBUG - REMOVE FOR RELEASE
                    EnemiesInCombat = CAIManager.m_List_Combat.Count;
                    EnemiesInvestigating = CAIManager.m_List_Investigate.Count;

                    m_SoundtrackEmitter.SetParameter("Soundtrack", m_fParameterValue);

                    //Update
                    m_fTimer += Time.deltaTime;
                    if (m_fTimer >= m_fUpdateFrequency)
                    {
                        m_fTimer = 0.0f;
                        CheckCorrection();
                    }
                    //Gradual change
                    if (m_bCorrection == true)
                    {

                        //float Diff = Mathf.Abs(m_fPreferedValue - m_fParameterValue);
                        //Diff /= Diff;

                        float Diff = (m_fParameterMaxValue / m_fMaxBlendCorrectionTime) * Time.deltaTime;

                        m_fParameterValue += Diff * m_DirectionMultiplier;

                        if (Mathf.Abs(m_fPreferedValue - m_fParameterValue) <= Mathf.Abs(Diff))
                        {
                            m_fParameterValue = m_fPreferedValue;
                            m_bCorrection = false;
                        }
                        else if (m_fParameterValue > m_fParameterMaxValue)
                            m_fParameterValue = m_fParameterMinValue + (m_fParameterValue - m_fParameterMaxValue);
                        else if (m_fParameterValue < m_fParameterMinValue)
                            m_fParameterValue = m_fParameterMaxValue + (m_fParameterValue - m_fParameterMinValue);
                    }
                }
                break;
            default:
                break;
        }
    }

    private void CheckCorrection()
    {
        if (IsInCombat() == true)
        {
            m_fPreferedValue = m_fInCombatValue;
        }
        else if(IsInvestigating() == true)
        {
            m_fPreferedValue = m_fInvestigatingValue;
        }
        else
        {
            m_fPreferedValue = m_fOutOfCombatValue;
        }


        float Difference = m_fPreferedValue - m_fParameterValue;

        //See if it's faster to correct soundtrack by reducing or increasing m_parametervalue
        if (Difference < 0.0f)
            Difference += m_fParameterMaxValue;
        if (Difference >= m_fParameterMaxValue / 2.0f)
            m_DirectionMultiplier = -1;
        else
            m_DirectionMultiplier = 1;

        Difference /= Difference;

        Difference *= (m_fParameterMaxValue / m_fMaxBlendCorrectionTime) * Time.deltaTime;

        //See if difference between Preffered value and current value is greater
        //than the per frame correction
        if (Mathf.Abs(m_fPreferedValue - m_fParameterValue) > Mathf.Abs(Difference))
        {
            m_bCorrection = true;
        }
    }

    private bool IsInCombat()
    {
        if (CAIManager.m_List_Combat.Count > 0)
            return true;

        return false;
    }

    private bool IsInvestigating()
    {
        if (CAIManager.m_List_Investigate.Count > 0)
            return true;

        return false;
    }

    public void SetSoundtrackState(SoundtrackState newState)
    {

        m_SoundtrackEmitter.Stop();

        switch (newState)
        {
            case SoundtrackState.Main:
                {
                    m_SoundtrackState = newState;
                    m_SoundtrackEmitter.ChangeEvent(m_MainSoundtrack);
                }
                break;
            case SoundtrackState.Victory:
                {
                    m_SoundtrackState = newState;
                    m_SoundtrackEmitter.ChangeEvent(m_VictorySoundtrack);
                }
                break;
            case SoundtrackState.Menu:
                {
                    m_SoundtrackState = newState;
                    m_SoundtrackEmitter.ChangeEvent(m_MenuSoundtrack);
                }
                break;
            default:
                break;
        }
        
        m_SoundtrackEmitter.Play();
    }
}
