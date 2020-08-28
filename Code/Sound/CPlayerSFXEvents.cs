using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerSFXEvents : MonoBehaviour
{
    private GameObject m_PlayerGO;
    private CStatusEffects m_PlayerStatus;

    [SerializeField] private GameObject m_LeftFoot;
    [SerializeField] private GameObject m_RightFoot;

    private void Start()
    {
        m_PlayerGO = CPlayerControlls.GetPlayer().gameObject;
        m_PlayerStatus = CPlayerControlls.GetPlayer().GetComponent<CStatusEffects>();
    }

    void PlayerStep(int Foot)
    {
        //Do we want sounds to come frome the left and right foot or does that sound weird?
        //TODO: Make a decision concerning the above question
        switch (Foot)
        {
            case 0:
                CSoundBank.Instance.PlayerStep(m_PlayerStatus.GetIsSlowed(), m_LeftFoot);
                break;
            case 1:
                CSoundBank.Instance.PlayerStep(m_PlayerStatus.GetIsSlowed(), m_RightFoot);
                break;
        }
    }

    void WeaponSwing(int ComboIndex)
    {
        CSoundBank.Instance.PlayerWeaponSwing(ComboIndex, gameObject);
    }
}
