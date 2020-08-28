using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class CSoundBank : MonoBehaviour
{

    public static CSoundBank Instance;

    private readonly string m_sDefaultEvent = "event:/Goblin/Goblin_Death";

    private void Awake()
    {
        if (Instance != null)
        {
            //Destroy(gameObject);
            Destroy(this);
        }
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    //////////////////////////////////////////////////////////
    //                    PLAYER
    //////////////////////////////////////////////////////////

    [Header("Player sounds")]

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerStepEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerAttack1Event = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerAttack2Event = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerAttack3Event = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerAttackSwing1 = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerAttackSwing2 = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerAttackSwing3 = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerChargingAttackEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerChargedAttackEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerShockwaveEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerTakeDamageEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerDeathEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerDodgeEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_PlayerPowerUp = string.Empty;

    [Space]
    [Space]

    //////////////////////////////////////////////////////////
    //                    GUITAR
    //////////////////////////////////////////////////////////

    //TODO, All guitar events may be 2D, might need special code
    [Header("Guitar sounds")]

    [FMODUnity.EventRef]
    [SerializeField] private string m_GuitarDrawEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GuitarAccord1Event = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GuitarAccord2Event = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GuitarAccord3Event = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GuitarAccord4Event = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GuitarMilestoneEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GuitarSoloFailedEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GuitarFailed1Event = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GuitarFailed2Event = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GuitarFailed3Event = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GuitarFailed4Event = string.Empty;

    [Space]
    [Space]

    //////////////////////////////////////////////////////////
    //                      ORC
    //////////////////////////////////////////////////////////

    [Header("Orc sounds")]

    [FMODUnity.EventRef]
    [SerializeField] private string m_OrcAttack1Event = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_OrcAttack2Event = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_OrcAttack1_HitEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_OrcAttack2_HitEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_OrcMoveEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_OrcAlertedEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_OrcEnterCombatEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_OrcTakeDamageEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_OrcDeathEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_OrcHummingEvent = string.Empty;

    [Space]
    [Space]

    //////////////////////////////////////////////////////////
    //                    GOBLIN
    //////////////////////////////////////////////////////////

    [Header("Goblin sounds")]

    [FMODUnity.EventRef]
    [SerializeField] private string m_GoblinRangedAttackEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GoblinMeleeAttackEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GoblinMeleeAttack_HitEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GoblinIdleEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GoblinAlertedEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GoblinEnterCombatEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GoblinDeathEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_GoblinRangedExplosionEvent = string.Empty;

    [Space]
    [Space]

    //////////////////////////////////////////////////////////
    //                    AMBIENCE
    //////////////////////////////////////////////////////////

    //TODO: We might want to do ambience differently
    [Header("Ambience sounds")]

    [FMODUnity.EventRef]
    [SerializeField] private string m_LavaTouchEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_LavaBubblingEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_TarWadingEvent = string.Empty;

    [FMODUnity.EventRef]
    [SerializeField] private string m_CheckpointEvent = string.Empty;

    //[FMODUnity.EventRef]
    //[SerializeField] private string m_TarBubblingEvent = string.Empty;

    //[FMODUnity.EventRef]
    //[SerializeField] private string m_StatueScrapingEvent = string.Empty;

    private void Start()
    {

        //////////////////////////////////////////////////////////
        //                    PLAYER
        //////////////////////////////////////////////////////////
        
        //Player walk event
        if (m_PlayerStepEvent == string.Empty)
        {
            m_PlayerStepEvent = m_sDefaultEvent;
        }
        //Player attack 1
        if (m_PlayerAttack1Event == string.Empty)
        {
            m_PlayerAttack1Event = m_sDefaultEvent;
        }
        //Player attack 2
        if (m_PlayerAttack2Event == string.Empty)
        {
            m_PlayerAttack2Event = m_sDefaultEvent;
        }
        //Player attack 3
        if (m_PlayerAttack3Event == string.Empty)
        {
            m_PlayerAttack3Event = m_sDefaultEvent;
        }
        //Player attack swing 1
        if(m_PlayerAttackSwing1 == string.Empty)
        {
            m_PlayerAttackSwing1 = m_sDefaultEvent;
        }
        //Player attack swing 2
        if(m_PlayerAttackSwing2 == string.Empty)
        {
            m_PlayerAttackSwing2 = m_sDefaultEvent;
        }
        //Player attack swing 3
        if(m_PlayerAttackSwing3 == string.Empty)
        {
            m_PlayerAttackSwing3 = m_sDefaultEvent;
        }
        //Player charging attack
        if(m_PlayerChargingAttackEvent == string.Empty)
        {
            m_PlayerChargingAttackEvent = m_sDefaultEvent;
        }
        //Player charged attack
        if (m_PlayerChargedAttackEvent == string.Empty)
        {
            m_PlayerChargedAttackEvent = m_sDefaultEvent;
        }
        //Player shockwave
        if (m_PlayerShockwaveEvent == string.Empty)
        {
            m_PlayerShockwaveEvent = m_sDefaultEvent;
        }
        //Player takedamage
        if (m_PlayerTakeDamageEvent == string.Empty)
        {
            m_PlayerTakeDamageEvent = m_sDefaultEvent;
        }
        //Player death
        if (m_PlayerDeathEvent == string.Empty)
        {
            m_PlayerDeathEvent = m_sDefaultEvent;
        }
        //Player dodge
        if (m_PlayerDodgeEvent == string.Empty)
        {
            m_PlayerDodgeEvent = m_sDefaultEvent;
        }
        //Player powerup
        if (m_PlayerPowerUp == string.Empty)
        {
            m_PlayerPowerUp = m_sDefaultEvent;
        }

        //////////////////////////////////////////////////////////
        //                    GUITAR
        //////////////////////////////////////////////////////////

        //Guitar draw
        if (m_GuitarDrawEvent == string.Empty)
        {
            m_GuitarDrawEvent = m_sDefaultEvent;
        }
        //Guitar accord 1
        if (m_GuitarAccord1Event == string.Empty)
        {
            m_GuitarAccord1Event = m_sDefaultEvent;
        }
        //Guitar accord 2
        if (m_GuitarAccord2Event == string.Empty)
        {
            m_GuitarAccord2Event = m_sDefaultEvent;
        }
        //Guitar accord 3
        if (m_GuitarAccord3Event == string.Empty)
        {
            m_GuitarAccord3Event = m_sDefaultEvent;
        }
        //Guitar accord 4
        if (m_GuitarAccord4Event == string.Empty)
        {
            m_GuitarAccord4Event = m_sDefaultEvent;
        }
        //Guitar milestone
        if (m_GuitarMilestoneEvent == string.Empty)
        {
            m_GuitarMilestoneEvent = m_sDefaultEvent;
        }
        //Guitar failed solo
        if (m_GuitarSoloFailedEvent == string.Empty)
        {
            m_GuitarSoloFailedEvent = m_sDefaultEvent;
        }
        //Guitar failed 1
        if(m_GuitarAccord1Event == string.Empty)
        {
            m_GuitarFailed1Event = m_sDefaultEvent;
        }
        //Guitar failed 2
        if (m_GuitarAccord2Event == string.Empty)
        {
            m_GuitarFailed2Event = m_sDefaultEvent;
        }
        //Guitar failed 3
        if (m_GuitarAccord3Event == string.Empty)
        {
            m_GuitarFailed3Event = m_sDefaultEvent;
        }
        //Guitar failed 4
        if (m_GuitarAccord4Event == string.Empty)
        {
            m_GuitarFailed4Event = m_sDefaultEvent;
        }

        //////////////////////////////////////////////////////////
        //                      ORC
        //////////////////////////////////////////////////////////

        //Orc attack 1
        if (m_OrcAttack1Event == string.Empty)
        {
            m_OrcAttack1Event= m_sDefaultEvent;
        }
        //Orc attack 2
        if (m_OrcAttack2Event == string.Empty)
        {
            m_OrcAttack2Event = m_sDefaultEvent;
        }
        //Orc attack hit 1
        if (m_OrcAttack1Event == string.Empty)
        {
            m_OrcAttack1Event = m_sDefaultEvent;
        }
        //Orc attack hit 2
        if (m_OrcAttack2Event == string.Empty)
        {
            m_OrcAttack2Event = m_sDefaultEvent;
        }
        //Orc move
        if (m_OrcMoveEvent == string.Empty)
        {
            m_OrcMoveEvent = m_sDefaultEvent;
        }
        //Orc alerted
        if (m_OrcAlertedEvent == string.Empty)
        {
            m_OrcAlertedEvent = m_sDefaultEvent;
        }
        //Orc enter combat
        if (m_OrcEnterCombatEvent == string.Empty)
        {
            m_OrcEnterCombatEvent = m_sDefaultEvent;
        }
        //Orc take damage
        if (m_OrcTakeDamageEvent == string.Empty)
        {
            m_OrcTakeDamageEvent = m_sDefaultEvent;
        }
        //Orc death
        if (m_OrcDeathEvent == string.Empty)
        {
            m_OrcDeathEvent = m_sDefaultEvent;
        }
        //Orc humming
        if (m_OrcHummingEvent == string.Empty)
        {
            m_OrcHummingEvent = m_sDefaultEvent;
        }

        //////////////////////////////////////////////////////////
        //                    GOBLIN
        //////////////////////////////////////////////////////////

        //Goblin ranged attack
        if (m_GoblinRangedAttackEvent == string.Empty)
        {
            m_GoblinRangedAttackEvent = m_sDefaultEvent;
        }
        //Goblin melee attack
        if (m_GoblinMeleeAttackEvent == string.Empty)
        {
            m_GoblinMeleeAttackEvent = m_sDefaultEvent;
        }
        //Goblin melee attack Hit event
        if(m_GoblinMeleeAttack_HitEvent == string.Empty)
        {
            m_GoblinMeleeAttack_HitEvent = m_sDefaultEvent;
        }
        //Goblin idle
        if (m_GoblinIdleEvent == string.Empty)
        {
            m_GoblinIdleEvent = m_sDefaultEvent;
        }
        //Goblin alerted
        if (m_GoblinAlertedEvent == string.Empty)
        {
            m_GoblinAlertedEvent = m_sDefaultEvent;
        }
        //Goblin enter combat
        if (m_GoblinEnterCombatEvent == string.Empty)
        {
            m_GoblinEnterCombatEvent = m_sDefaultEvent;
        }
        //Goblin death
        if (m_GoblinDeathEvent == string.Empty)
        {
            m_GoblinDeathEvent = m_sDefaultEvent;
        }
        //Goblin ranged explosion
        if(m_GoblinRangedExplosionEvent == string.Empty)
        {
            m_GoblinRangedExplosionEvent = m_sDefaultEvent;
        }

        //////////////////////////////////////////////////////////
        //                    AMBIENCE
        //////////////////////////////////////////////////////////
        
        //Lava touch
        if (m_LavaTouchEvent == string.Empty)
        {
            m_LavaTouchEvent = m_sDefaultEvent;
        }
        //Lava bubbling
        if (m_LavaBubblingEvent == string.Empty)
        {
            m_LavaBubblingEvent = m_sDefaultEvent;
        }
        //Tar wading
        if (m_TarWadingEvent == string.Empty)
        {
            m_TarWadingEvent = m_sDefaultEvent;
        }
    }

    //////////////////////////////////////////////////////////
    //                    PLAYER
    //////////////////////////////////////////////////////////

    public void PlayerStepNormal(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerStepEvent, go);
    }

    public void PlayerAttack1(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerAttack1Event, go);
    }
    
    public void PlayerAttack2(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerAttack2Event, go);
    }

    public void PlayerAttack3(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerAttack3Event, go);
    }

    public void PlayerAttackSwing1(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerAttackSwing1, go);
    }

    public void PlayerAttackSwing2(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerAttackSwing2, go);
    }

    public void PlayerAttackSwing3(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerAttackSwing3, go);
    }

    public void PlayerChargingAttack(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerChargingAttackEvent, go);
    }

    public void PlayerChargedAttack(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerChargedAttackEvent, go);
    }

    public void PlayerShockwave(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerShockwaveEvent, go);
    }

    public void PlayerTakeDamage(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerTakeDamageEvent, go);
    }

    public void PlayerDeath(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerDeathEvent, go);
    }

    public void PlayerDodge(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerDodgeEvent, go);
    }

    public void PlayerPowerUp(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_PlayerPowerUp, go);
    }

    //////////////////////////////////////////////////////////
    //                    GUITAR
    //////////////////////////////////////////////////////////

    public void GuitarDraw(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_GuitarDrawEvent, go);
    }

    //TODO: Might want a bigger function which handles all 4 accords instead of keeping them individually
    //2D Sound
    public void GuitarAccord1(/*GameObject go*/)
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_GuitarAccord1Event);
    }

    //2D Sound
    public void GuitarAccord2(/*GameObject go*/)
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_GuitarAccord2Event);
    }

    //2D Sound
    public void GuitarAccord3(/*GameObject go*/)
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_GuitarAccord3Event);
    }

    //2D Sound
    public void GuitarAccord4(/*GameObject go*/)
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_GuitarAccord4Event);
    }

    public void GuitarMilestone(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_GuitarMilestoneEvent, go);
    }

    //2D Sound
    public void GuitarSoloFailed(/*GameObject go*/)
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_GuitarSoloFailedEvent);
    }

    public void GuitarFailed1(/*GameObject go*/)
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_GuitarFailed1Event);
    }

    public void GuitarFailed2(/*GameObject go*/)
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_GuitarFailed2Event);
    }

    public void GuitarFailed3(/*GameObject go*/)
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_GuitarFailed3Event);
    }

    public void GuitarFailed4(/*GameObject go*/)
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_GuitarFailed4Event);
    }

    //////////////////////////////////////////////////////////
    //                      ORC
    //////////////////////////////////////////////////////////

    public void OrcAttack1(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_OrcAttack1Event, go);
    }

    public void OrcAttack2(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_OrcAttack2Event, go);
    }

    public void OrcAttack1_Hit(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_OrcAttack1_HitEvent, go);
    }

    public void OrcAttack2_Hit(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_OrcAttack2_HitEvent, go);
    }

    public void OrcMove(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_OrcMoveEvent, go);
    }

    public void OrcAlerted(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_OrcAlertedEvent, go);
    }

    public void OrcEnterCombat(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_OrcEnterCombatEvent, go);
    }

    public void OrcTakeDamage(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_OrcTakeDamageEvent, go);
    }

    public void OrcDeath(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_OrcDeathEvent, go);
    }

    public void OrcHumming(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_OrcHummingEvent, go);
    }


    //////////////////////////////////////////////////////////
    //                    GOBLIN
    //////////////////////////////////////////////////////////

    public void GoblinRangedAttack(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_GoblinRangedAttackEvent, go);
    }

    public void GoblinMeleeAttack(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_GoblinMeleeAttackEvent, go);
    }

    public void GoblinMeleeAttack_Hit(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_GoblinMeleeAttack_HitEvent, go);
    }

    public void GoblinIdle(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_GoblinIdleEvent, go);
    }

    public void GoblinAlerted(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_GoblinAlertedEvent, go);
    }

    public void GoblinEnterCombat(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_GoblinEnterCombatEvent, go);
    }

    public void GoblinDeath(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_GoblinDeathEvent, go);
    }

    public void GoblinRangedExplosion(Vector3 position)
    {
        FMODUnity.RuntimeManager.PlayOneShot(m_GoblinRangedExplosionEvent, position);
    }

    //////////////////////////////////////////////////////////
    //                    AMBIENCE
    //////////////////////////////////////////////////////////
    
        //TODO: We might want to do ambience differently

    public void LavaTouch(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_LavaTouchEvent, go);
    }

    public void LavaBubbling(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_LavaBubblingEvent, go);
    }

    public void TarWading(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_TarWadingEvent, go);
    }

    public void CheckPoint(GameObject go)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_CheckpointEvent, go);
    }

    //GENERAL FUNCTIONS
    public void AITakeDamage(CAI.AIType type, GameObject go)
    {
        switch (type)
        {
            case CAI.AIType.Orc:
                OrcTakeDamage(go);
                break;
            case CAI.AIType.Goblin:
                //Goblin doesn't have a takedamage sound
                break;
            default:
                break;
        }
    }

    public void AIDeath(CAI.AIType type, GameObject go)
    {
        switch (type)
        {
            case CAI.AIType.Orc:
                OrcDeath(go);
                break;
            case CAI.AIType.Goblin:
                GoblinDeath(go);
                break;
            default:
                break;
        }
    }

    public void AIAlerted(CAI.AIType type, GameObject go)
    {
        switch (type)
        {
            case CAI.AIType.Orc:
                OrcAlerted(go);
                break;
            case CAI.AIType.Goblin:
                GoblinAlerted(go);
                break;
            default:
                break;
        }
    }

    public void AIEnterCombat(CAI.AIType type, GameObject go)
    {
        switch (type)
        {
            case CAI.AIType.Orc:
                OrcEnterCombat(go);
                break;
            case CAI.AIType.Goblin:
                GoblinEnterCombat(go);
                break;
            default:
                break;
        }
    }

    public void AIIdle(CAI.AIType type, GameObject go)
    {
        switch (type)
        {
            case CAI.AIType.Orc:
                OrcHumming(go);
                break;
            case CAI.AIType.Goblin:
                GoblinIdle(go);
                break;
            default:
                break;
        }
    }

    public void AIMelee(CAI.AIType type, int iAttackIndex, GameObject go)
    {
        switch (type)
        {
            case CAI.AIType.Orc:
                switch (iAttackIndex)
                {
                    case 0:
                        OrcAttack1(go);
                        break;
                    case 1:
                        OrcAttack2(go);
                        break;
                }
                break;
            case CAI.AIType.Goblin:
                GoblinMeleeAttack(go);
                break;
            default:
                break;
        }
    }

    public void AIMelee_Hit(CAI.AIType type, int iAttackIndex, GameObject go)
    {
        switch (type)
        {
            case CAI.AIType.Orc:
                switch (iAttackIndex)
                {
                    case 0:
                        OrcAttack1_Hit(go);
                        break;
                    case 1:
                        OrcAttack2_Hit(go);
                        break;
                }
                break;
            case CAI.AIType.Goblin:
                GoblinMeleeAttack(go);
                break;
            default:
                break;
        }
    }

    public void PlayerStep(bool InTar, GameObject go)
    {
        if(InTar == true)
        {
            TarWading(go);
        }
        else
        {
            PlayerStepNormal(go);
        }
    }

    public void QTENote(int Button)
    {
        switch (Button)
        {
            case (int)CQTESystem.Buttons.CROSS:
                GuitarAccord1();
                break;

            case (int)CQTESystem.Buttons.SQUARE:
                GuitarAccord2();
                break;

            case (int)CQTESystem.Buttons.TRIANGLE:
                GuitarAccord3();
                break;

            case (int)CQTESystem.Buttons.CIRCLE:
                GuitarAccord4();
                break;
            default:
                break;
        }
    }

    public void QTEFailed(int Button)
    {
        switch (Button)
        {
            case (int)CQTESystem.Buttons.CROSS:
                GuitarFailed1();
                break;

            case (int)CQTESystem.Buttons.SQUARE:
                GuitarFailed2();
                break;

            case (int)CQTESystem.Buttons.TRIANGLE:
                GuitarFailed3();
                break;

            case (int)CQTESystem.Buttons.CIRCLE:
                GuitarFailed4();
                break;
            default:
                break;
        }
    }

    public void PlayerWeaponSwing(int index, GameObject go)
    {
        switch (index)
        {
            case 1:
                PlayerAttackSwing1(go);
                break;
            case 2:
                PlayerAttackSwing2(go);
                break;
            case 3:
                PlayerAttackSwing3(go);
                break;
        }
    }
}
