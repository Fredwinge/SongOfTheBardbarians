using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionPoints : MonoBehaviour
{

    public float m_fCurrentActionPoints = 100.0f;
    public float m_fMaxActionPoints = 100.0f;

    [SerializeField] private float m_fRecoveryCooldown = 3.0f;
    [SerializeField] private float m_fRecoveryTimer = 0.0f;
    [SerializeField] private float m_fRecoveryPerSecond = 15.0f;

     public float m_fRecoverySpeedMultiplier = 1.0f;
                     
    [SerializeField] private bool m_bRecoveryOnCooldown = false;

    private void Update()
    {

        if(m_bRecoveryOnCooldown == false && m_fCurrentActionPoints < m_fMaxActionPoints)
        {
            RecoverActionPoints(m_fRecoveryPerSecond * m_fRecoverySpeedMultiplier * Time.deltaTime);

            if (m_fCurrentActionPoints >= m_fMaxActionPoints)
                m_fCurrentActionPoints = m_fMaxActionPoints;

        }
        else if(m_bRecoveryOnCooldown == true)
        {
            m_fRecoveryTimer += Time.deltaTime;

            if (m_fRecoveryTimer >= m_fRecoveryCooldown)
                m_bRecoveryOnCooldown = false;
        }
    }

    //TODO: Remove probably, don't use the function since we don't need to check if value < 0.0f either way
    //so performance will be better without the additional if statements
    //void ChangeActionPoints(float fAmount)
    //{
    //    m_fCurrentActionPoints -= fAmount;

    //    m_fCurrentActionPoints = Mathf.Clamp(m_fCurrentActionPoints, 0.0f , m_fMaxActionPoints);

    //    CUIManager.UpdateActionPointUI();
    //}

    public bool UseActionPoints(float fAmount)
    {
        if(m_fCurrentActionPoints - fAmount < 0.0f)
        {
            //print(gameObject.name + " didn't have enough action points.");

            return false;
        }

        m_fRecoveryTimer = 0.0f;
        m_bRecoveryOnCooldown = true;
        //ChangeActionPoints(fAmount);

        m_fCurrentActionPoints -= fAmount;
        CUIManager.UpdateActionPointUI();

        return true;
    }

    public void RecoverActionPoints(float fAmount)
    {
        //ChangeActionPoints(-fAmount);

        m_fCurrentActionPoints += fAmount;

        if (m_fCurrentActionPoints >= m_fMaxActionPoints)
            m_fCurrentActionPoints = m_fMaxActionPoints;

        CUIManager.UpdateActionPointUI();
    }
}
