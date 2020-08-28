using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPower : MonoBehaviour
{

    public float m_fCurrentPower = 0.0f;
    public float m_fMaxPower = 100.0f;

    [Tooltip("The power cooldown controlls when you can gain power")]
    public float m_fCooldownLength = 5.0f;
    public float m_fCooldownTimer = 0.0f;
    [SerializeField] private float m_fReductionPerSecond = 10.0f;
    [HideInInspector] public float m_fMilestoneAddedCooldown = 0.0f;

    [HideInInspector] public bool m_bOnCooldown = false;

    private void Update()
    {

        if (m_fCurrentPower > 0.0f && m_bOnCooldown == true)
        {
            ReducePowerCharge(m_fReductionPerSecond * Time.deltaTime);

            if (m_fCurrentPower >= m_fMaxPower)
                m_fCurrentPower = m_fMaxPower;

        }
        else if (m_bOnCooldown == true)
        {
            m_fCooldownTimer += Time.deltaTime;

            if (m_fCooldownTimer >= m_fCooldownLength + m_fMilestoneAddedCooldown)
            {
                m_fCurrentPower = 0.0f;
                m_fCooldownTimer = 0.0f;
                m_bOnCooldown = false;
            }
        }
    }

    void ChangePowerCharge(float fAmount)
    {
        m_fCurrentPower += fAmount;

        m_fCurrentPower = Mathf.Clamp(m_fCurrentPower, 0.0f, m_fMaxPower);

        CUIManager.UpdatePowerChargeUI();
    }

    public bool AddPowerCharge(float fAmount)
    {
        if(m_bOnCooldown == true)
        {
            return false;
        }

        ChangePowerCharge(fAmount);
        //m_bOnCooldown = true;

        return true;
    }

    void ReducePowerCharge(float fAmount)
    {
        ChangePowerCharge(-fAmount);
    }

}
