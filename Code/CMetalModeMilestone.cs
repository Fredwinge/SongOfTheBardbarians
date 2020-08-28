using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMetalModeMilestone : MonoBehaviour
{
    [Tooltip("The power treshold which must be reached to gain this milestones benefits")]
    public float m_fMilestonePowerTreshold = 0.0f;
    [Tooltip("The extra cooldown time added onto the power cooldown if this milestone is reached")]
    public float m_fMilestoneAddedCooldown = 2.0f;
    [Tooltip("The amount of time which the health regen tick delay is divided by during metal mode")]
    public float m_fHealthRegenMultiplier = 1.0f;
    [Tooltip("The amount the action point regeneration speed is multiplied by during metal mode")]
    public float m_fActionPointRegenMultiplier = 1.0f;
    [Tooltip("How much the players damage is multiplied by during metal mode || Currently unnused")]
    public float m_fDamageMultiplier = 1.0f;
    [Tooltip("How much the shockwaves radius is increased when this milestone is reached")]
    public float m_fShockwaveRadialIncrease = 0.5f;
    [Tooltip("How much the shockwaves force is increased, if there is need for it")]
    public float m_fShockwaveForceIncrease = 0.0f;

}
