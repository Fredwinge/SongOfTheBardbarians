using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMilestoneManager : MonoBehaviour
{

    public static List<CMetalModeMilestone> m_Milestones = null;

    private void Awake()
    {
        m_Milestones = new List<CMetalModeMilestone>();

        foreach(Transform child in transform)
        {
            m_Milestones.Add(child.GetComponent<CMetalModeMilestone>());
        }

        //Sort the milestones depending on their power treshold
        m_Milestones.Sort((m1, m2) => m1.m_fMilestonePowerTreshold.CompareTo(m2.m_fMilestonePowerTreshold));
    }
}
