using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMetalMode : MonoBehaviour
{

    [HideInInspector] public bool m_bIsPlaying = false;
    [HideInInspector] public bool m_bOnCooldown = false;

    private bool m_bMetalModeActive = false;
    private bool m_bChargeIsOffCooldown = true;
    
    [Tooltip("The instrument cooldown length, controlls when you can play the QTE")]
    [SerializeField] private float m_fInstrumentCooldownLength = 5.0f;
    [Tooltip("Debug value to see how far the cooldown timer has progressed")]
    [SerializeField] private float m_fCooldownTimer = 0.0f;
    [Tooltip("The amount of health regained whenever the correct note has been hit")]
    [SerializeField] private int m_HealthRegenPerNote = 10;
    [Tooltip("The amount of time between each health regen tick during metal mode, one tick regains 1 hp")]
    [SerializeField] private float m_fHealthRegenTick = 0.1f;
    private float m_fHealthTimer = 0.0f;

    [Tooltip("The shockwaves base radius")]
    [SerializeField] private float m_fShockwaveRadius = 5.0f;
    [Tooltip("The shockwaves knockback force")]
    [SerializeField] private float m_fShockwaveForce = 1500.0f;
    //Used when power can't be gained
    private float m_fPercentageNotesHit = 0.0f;

    
    private CHealth m_PlayerHealth;
    private CActionPoints m_PlayerActionPoints;
    private CPower m_PlayerPower;
    private CAttack m_PlayerAttack;

    private List<CMetalModeMilestone> m_MilestoneComponents = new List<CMetalModeMilestone>();
    
    private int m_iMilestoneReachedIndex = -1;
    [HideInInspector] public int m_iCurrentMilestoneIndex = 0;

    [Tooltip("The metalmode aura VFX")]
    [SerializeField] private GameObject m_MetalModeVFXPrefab;

    private List<GameObject> m_MetalModeVFXStages = new List<GameObject>();

    [Tooltip("The VFX that occurs when a milestone is reached")]
    [SerializeField] private GameObject m_MilestoneVFXPrefab;

    private ParticleSystem m_MilestoneVFXParticleSystem = null;

    private void Awake()
    {

        //Instantiate VFX
        m_MetalModeVFXPrefab = Instantiate(m_MetalModeVFXPrefab, transform);

        m_MilestoneVFXPrefab = Instantiate(m_MilestoneVFXPrefab, transform);

        //Set y axis a little higher to avoid z-fighting
        Vector3 MilestoneVFXPos = m_MilestoneVFXPrefab.transform.position;
        MilestoneVFXPos.y += 0.5f;
        m_MilestoneVFXPrefab.transform.position = MilestoneVFXPos;

    }

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerHealth = GetComponent<CHealth>();
        m_PlayerActionPoints = GetComponent<CActionPoints>();
        m_PlayerPower = GetComponent<CPower>();
        m_PlayerAttack = GetComponent<CAttack>();

        m_MilestoneComponents = CMilestoneManager.m_Milestones;
        if(m_MilestoneComponents == null)
        {
            //Set to empty list to avoid errors
            m_MilestoneComponents = new List<CMetalModeMilestone>();
            print("Couldn't find the Metalmode Milestone Settings");
        }

        if(m_MilestoneVFXPrefab == null)
        {
            print("Milestone vfx hasn't been set");
        }

        m_MilestoneVFXParticleSystem = m_MilestoneVFXPrefab.transform.GetComponentInChildren<ParticleSystem>();

        if(m_MilestoneVFXParticleSystem == null)
        {
            print("Milestone vfx doesn't contain a child with an attached particle system!");
        }

        if(m_MetalModeVFXPrefab == null)
        {
            print("Metalmode VFX hasn't been set!");
        }

        foreach(Transform child in m_MetalModeVFXPrefab.transform)
        {
            m_MetalModeVFXStages.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

        //COOLDOWN / ISPLAYING
        if (m_bIsPlaying == true)
        {
            if (CQTESystem.m_bQTEActive == false)
            {
                //Stop playing
                StopPlaying();
            }
        }
        else if (m_bOnCooldown == true)
        {
            m_fCooldownTimer += Time.deltaTime;
            

            if (m_fCooldownTimer >= m_fInstrumentCooldownLength)
            {
                m_bOnCooldown = false;
                m_fCooldownTimer = 0.0f;
                //print("Metal mode is off cooldown");
            }
        }


        if (m_bMetalModeActive == true && m_PlayerPower.m_fCurrentPower <= 0.0f)
        {

            if (m_iMilestoneReachedIndex > -1)
            {
                //Reset passive buffs
                m_PlayerActionPoints.m_fRecoverySpeedMultiplier /= m_MilestoneComponents[m_iMilestoneReachedIndex].m_fActionPointRegenMultiplier;
                m_fHealthRegenTick *= m_MilestoneComponents[m_iMilestoneReachedIndex].m_fHealthRegenMultiplier;

                m_PlayerAttack.MultiplyDamage(1.0f / m_MilestoneComponents[m_iMilestoneReachedIndex].m_fDamageMultiplier);

                m_iMilestoneReachedIndex = -1;
                //m_iCurrentMilestoneIndex = 0;
                m_MilestoneVFXPrefab.transform.position = transform.position;
                m_MilestoneVFXPrefab.transform.SetParent(transform);

                for (int i = 0; i < m_MetalModeVFXStages.Count; ++i)
                {
                    m_MetalModeVFXStages[i].SetActive(false);
                }
            }

            m_bMetalModeActive = false;

            Shader.SetGlobalFloat("_Milestone", 0.0f);
        }
        //Add variable
        else if (m_bMetalModeActive == true)
        {
            m_fHealthTimer += Time.deltaTime;

            if (m_fHealthTimer >= m_fHealthRegenTick)
            {
                m_fHealthTimer = 0.0f;
                m_PlayerHealth.Heal(1);
            }

            ////Deactivate VFX
            //if (m_iCurrentMilestoneIndex >= 0)
            //{
            //    //Check if passed milestone then play milestone sound
            //    if (m_MilestoneComponents[m_iCurrentMilestoneIndex].m_fMilestonePowerTreshold > m_PlayerPower.m_fCurrentPower)
            //    {
            //        m_MetalModeVFXChildren[m_iCurrentMilestoneIndex].SetActive(false);
            //        --m_iCurrentMilestoneIndex;
                        
            //    }
            //}
        }

    }

    //CONTROLS
    public void StartPlaying()
    {
        if (m_bOnCooldown == false)
        {
            m_iCurrentMilestoneIndex = 0;
            m_fPercentageNotesHit = 0.0f;
            m_bIsPlaying = true;
            CQTESystem.m_bQTEActive = true;

            Time.timeScale = CQTESettings.s_fTimeScaleSlowDown;

            CSoundBank.Instance.GuitarDraw(gameObject);
        }
    }

    public void StopPlaying()
    {
        if (m_bIsPlaying == true)
        {
            CQTESystem.m_bQTEActive = false;
            m_bIsPlaying = false;
            m_bOnCooldown = true;

            Time.timeScale = 1.0f;
        }
    }

    //QTE FUNCTIONS
    public void CalculateMilestone(float PercentageNotesHit)
    {
        if (m_bChargeIsOffCooldown == true)
        {
            float ValueReached = m_PlayerPower.m_fMaxPower * PercentageNotesHit;
            float CurrentMilestoneTreshold = 0.0f;

            for (int i = 0; i < m_MilestoneComponents.Count; ++i)
            {
                if (m_MilestoneComponents[i].m_fMilestonePowerTreshold <= ValueReached
                    && m_MilestoneComponents[i].m_fMilestonePowerTreshold > CurrentMilestoneTreshold)
                {
                    CurrentMilestoneTreshold = m_MilestoneComponents[i].m_fMilestonePowerTreshold;
                    m_iMilestoneReachedIndex = i;
                }
            }

            if (m_iMilestoneReachedIndex < 0)
            {
                //print("No milestone reached");
                m_PlayerPower.m_fMilestoneAddedCooldown = 0.0f;


            }
            else
            {
                //print("Reached milestone " + m_iMilestoneReachedIndex + " at " + CurrentMilestoneTreshold + " power charge with " + ValueReached + " power generated");

                //Set passive buffs for metal mode
                m_PlayerActionPoints.m_fRecoverySpeedMultiplier *= m_MilestoneComponents[m_iMilestoneReachedIndex].m_fActionPointRegenMultiplier;
                m_fHealthRegenTick /= m_MilestoneComponents[m_iMilestoneReachedIndex].m_fHealthRegenMultiplier;


                m_bMetalModeActive = true;
                m_fHealthTimer = 0.0f;

                m_PlayerPower.m_fMilestoneAddedCooldown = m_MilestoneComponents[m_iMilestoneReachedIndex].m_fMilestoneAddedCooldown;

                Shader.SetGlobalFloat("_Milestone", CurrentMilestoneTreshold / 100.0f);
                CSoundBank.Instance.PlayerShockwave(gameObject);
                CSoundBank.Instance.PlayerPowerUp(gameObject);

                //if (m_iCurrentMilestoneIndex == m_MilestoneComponents.Count)
                //    --m_iCurrentMilestoneIndex;

                m_PlayerAttack.MultiplyDamage(m_MilestoneComponents[m_iMilestoneReachedIndex].m_fDamageMultiplier);

                //for(int i = 0; i < m_MetalModeVFXChildren.Count; ++i)
                //{
                //    m_MetalModeVFXChildren[i].SetActive(false);
                //}
                
                Shockwave();
            }
        }
        else
        {
            //print("Power charge is on cooldown, no milestone could be reached");
        }
    }

    public void HitNote(float NotePercentage)
    {
        
        if (m_bChargeIsOffCooldown == true)
        {
            m_PlayerPower.AddPowerCharge(m_PlayerPower.m_fMaxPower * NotePercentage);
            if (m_iCurrentMilestoneIndex < m_MilestoneComponents.Count)
            {
                //Check if passed milestone then play milestone sound
                if (m_MilestoneComponents[m_iCurrentMilestoneIndex].m_fMilestonePowerTreshold <= m_PlayerPower.m_fCurrentPower)
                {
                    m_MetalModeVFXStages[m_iCurrentMilestoneIndex].SetActive(true);
                    ++m_iCurrentMilestoneIndex;

                    CSoundBank.Instance.GuitarMilestone(gameObject);
                    
                    //Make sure milestone vfx faces camera
                    m_MilestoneVFXPrefab.transform.rotation = Quaternion.identity;
                    m_MilestoneVFXParticleSystem.Play(true);
                }
            }
        }
        else
        {
            //Update Current milestone every time you hit a note instead of only when you can gain power
            if (m_iCurrentMilestoneIndex < m_MilestoneComponents.Count)
            {
                if (m_MilestoneComponents[m_iCurrentMilestoneIndex].m_fMilestonePowerTreshold <= m_PlayerPower.m_fMaxPower * m_fPercentageNotesHit)
                {
                    ++m_iCurrentMilestoneIndex;
                }
            }

            m_fPercentageNotesHit += NotePercentage;
        }

        m_PlayerActionPoints.RecoverActionPoints(m_PlayerActionPoints.m_fMaxActionPoints * NotePercentage);
        
        m_PlayerHealth.Heal(m_HealthRegenPerNote);
    }

    
    public void SetChargeOnCooldown()
    {
        //Only set charge on cooldown if any charge was actually generated
        if (m_bChargeIsOffCooldown == true  && m_PlayerPower.m_fCurrentPower > 0.0f)
            m_PlayerPower.m_bOnCooldown = true;

        m_bChargeIsOffCooldown = !m_PlayerPower.m_bOnCooldown;
        m_fCooldownTimer = 0.0f;
    }

    public void CheckChargeOffCooldown()
    {
        m_bChargeIsOffCooldown = !m_PlayerPower.m_bOnCooldown;

        if (m_bChargeIsOffCooldown == true)
            m_MilestoneVFXPrefab.transform.SetParent(null);
    }

    private void Shockwave()
    {
        //Compare only x and z values incase there's a significant difference in the pivot points elevations
        Vector3 ShockwaveOrigin = transform.position;
        ShockwaveOrigin.y = 0.0f;

        /*for(int i = 0; i < CAIManager.m_List_ActiveAI.Count; ++i)
        {
            Vector3 EnemyPos = CAIManager.m_List_ActiveAI[i].transform.position;
            EnemyPos.y = 0.0f;

            //TODO: Figure out which one is more performant and use that one / remove commented code

            //Distance check
            
            float Distance = Vector3.Distance(ShockwaveOrigin, EnemyPos);

            if(Distance < m_fShockwaveRadius + m_MilestoneComponents[m_iMilestoneReachedIndex].m_fShockwaveRadialIncrease)
            {
                Vector3 KnockBackDirection = CAIManager.m_List_ActiveAI[i].transform.position - transform.position;
                CAIManager.m_List_ActiveAI[i].GetStatusEffects().Knockback(KnockBackDirection, m_fShockwaveForce + m_MilestoneComponents[m_iMilestoneReachedIndex].m_fShockwaveForceIncrease);
            }
            
        }*/

        //Collider check
        Collider[] EnemyColliders = Physics.OverlapSphere(transform.position, m_fShockwaveRadius + m_MilestoneComponents[m_iMilestoneReachedIndex].m_fShockwaveRadialIncrease, CLayers.GetLayerMask_Enemy());

        for (int i = 0; i < EnemyColliders.Length; ++i)
        {
            Vector3 KnockBackDirection = EnemyColliders[i].transform.position - transform.position;
            EnemyColliders[i].gameObject.GetComponent<CStatusEffects>().Knockback(KnockBackDirection, m_fShockwaveForce + m_MilestoneComponents[m_iMilestoneReachedIndex].m_fShockwaveForceIncrease);
        }

        //if (Physics.CheckSphere(transform.position, m_fShockwaveRadius + m_MilestoneComponents[m_iMilestoneReachedIndex].m_fShockwaveRadialIncrease, CLayers.GetLayerMask_Enemy()) == true)
        //{
        //    Vector3 KnockBackDirection = CAIManager.m_List_ActiveAI[i].transform.position - transform.position;
        //    CAIManager.m_List_ActiveAI[i].GetStatusEffects().Knockback(KnockBackDirection, m_fShockwaveForce + m_MilestoneComponents[m_iMilestoneReachedIndex].m_fShockwaveForceIncrease);
        //}

    }
}
