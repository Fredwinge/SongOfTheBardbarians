using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CQTESettings : MonoBehaviour
{

    [Tooltip("The total amount of buttons in the QTE sequence")]
    [SerializeField] private int m_iButtonsCount = 10;

    [Tooltip("The percentual distance of the QTEArea that the button will travel in one second")]
    [SerializeField] private float m_fButtonBaseSpeed = 25.0f;
    [Tooltip("The percentual distance of the QTEArea between buttons during the first milestone")]
    [SerializeField] private float m_fButtonBaseDistance = 100.0f;
    //[Tooltip("The amount of speed which the Button base speed is multiplied by after each correct press")]
    //[SerializeField] private float m_fSpeedMultiplier = 1.05f;
    [Tooltip("The time scale during the QTE")]
    [SerializeField] private float m_fTimeScaleSlowDown = 0.8f;
    [SerializeField] private float m_fPressForgiveness = 0.0f;

    [SerializeField] private int m_iFirstMilestoneMultiplier = 2;
    [SerializeField] private int m_iSecondMilestoneMultiplier = 4;
    [SerializeField] private int m_iThirdMilestoneMultiplier = 8;

    [SerializeField] private Sprite m_CrossSprite = null;
    [SerializeField] private Sprite m_SquareSprite = null;
    [SerializeField] private Sprite m_TriangleSprite = null;
    [SerializeField] private Sprite m_CircleSprite = null;


    static public int s_iButtonsCount;

    static public float s_fButtonBaseSpeed;
    static public float s_fButtonBaseDistance;
    //static public float s_fSpeedMultiplier;
    static public float s_fTimeScaleSlowDown;
    static public float s_fPressForgiveness;

    static public int s_iFirstMilestoneMultiplier;
    static public int s_iSecondMilestoneMultiplier;
    static public int s_iThirdMilestoneMultiplier;

    static public List<Sprite> s_SpriteList = new List<Sprite>();

    
    void Awake()
    {
        s_iButtonsCount = m_iButtonsCount;

        s_fButtonBaseSpeed = m_fButtonBaseSpeed;
        s_fButtonBaseDistance = m_fButtonBaseDistance;
        //s_fSpeedMultiplier = m_fSpeedMultiplier;
        s_fTimeScaleSlowDown = m_fTimeScaleSlowDown;
        s_fPressForgiveness = m_fPressForgiveness;

        s_iFirstMilestoneMultiplier = m_iFirstMilestoneMultiplier;
        s_iSecondMilestoneMultiplier = m_iSecondMilestoneMultiplier;
        s_iThirdMilestoneMultiplier = m_iThirdMilestoneMultiplier;

        s_SpriteList.Add(m_CrossSprite);
        s_SpriteList.Add(m_SquareSprite);
        s_SpriteList.Add(m_TriangleSprite);
        s_SpriteList.Add(m_CircleSprite);
    }
}
