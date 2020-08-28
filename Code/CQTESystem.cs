using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CQTESystem : MonoBehaviour
{

    //Floats

    private float m_fTimeSinceStart = 0.0f;
    private float m_fButtonSpeed = 0.0f;

    private float m_fDistance = 0.0f;
    private float m_fMaxDistance = 0.0f;

    private float m_fButtonBaseSpeed = 20.0f;
    private float m_fBaseButtonDistance = 1.0f;
    private float m_fNotePercentageHit = 0.0f;
    
    private float m_fPressForgivenessDistance = 0.0f;

    //Ints
    private int m_iButtonsCount = 10;

    private int m_iCurrentButtonIndex = 0;
    private int m_iMaxShownButtonIndex = 0;

    private int m_iMaxHeight;
    private int m_iMinHeight;

    private int m_iButtonCountMultiplier = 15;
    private int m_iCurrentMilestone = 0;

    //To keep track of which index in m_ButtonImagesIndex is equal to m_iCurrentButtonIndex
    private int m_iCurrentListIndex = 0;

    public static int m_iInput = -1;

    //Bools
    private bool m_bFailed = false;
    private bool m_bInitialized = false;
    static public bool m_bQTEActive = false;

    //Lists

    //List holding the generated button sequence
    private List<int> m_iButtonSequenceList = new List<int>();

    //List holding all instantiated button images
    private List<Image> m_ButtonImages = new List<Image>();

    //List holding the instantiated images currently displayed index from the generated QTE-Sequence
    private List<int> m_ButtonImagesIndex = new List<int>();

    //List holding starting Y positions for all buttons
    private List<float> m_StartYPositions = new List<float>();

    [SerializeField] private Image m_PressArea;

    [SerializeField] private GameObject m_ButtonPrefab;

    private CMetalMode m_PlayerMetalMode;

    public static CQTESystem instance = null;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }


    public enum Buttons
    {
        CROSS,
        SQUARE,
        TRIANGLE,
        CIRCLE,
        BUTTON_TYPES_COUNT
    }

    private void Start()
    {

        //Get values from CQTESettings
        m_iButtonsCount = CQTESettings.s_iButtonsCount;
        m_fButtonBaseSpeed = CQTESettings.s_fButtonBaseSpeed;
        m_fBaseButtonDistance = CQTESettings.s_fButtonBaseDistance / 100.0f;

        m_iButtonCountMultiplier = CQTESettings.s_iFirstMilestoneMultiplier + CQTESettings.s_iSecondMilestoneMultiplier + CQTESettings.s_iThirdMilestoneMultiplier + 1;

        m_PlayerMetalMode = CPlayerControlls.GetPlayer().GetComponent<CMetalMode>();
        GenerateNewSequence();

        m_iMaxHeight = (int)(gameObject.GetComponent<Image>().rectTransform.anchoredPosition.y + gameObject.GetComponent<Image>().rectTransform.rect.height / 2.0f);
        m_iMinHeight = (int)(gameObject.GetComponent<Image>().rectTransform.anchoredPosition.y - gameObject.GetComponent<Image>().rectTransform.rect.height / 2.0f);
        
        
        //Initialize
        {

            m_fDistance = Vector2.Distance(new Vector2(0, m_iMaxHeight), new Vector2(0, m_PressArea.rectTransform.anchoredPosition.y));


            m_fButtonSpeed = m_fDistance * (m_fButtonBaseSpeed / 100.0f);

            //Calculate the minimum distance between two buttons
            float fButtonDist = Mathf.LerpUnclamped(m_fDistance, m_fMaxDistance, 1.0f / (m_iButtonsCount * CQTESettings.s_iThirdMilestoneMultiplier)) - m_fDistance;
            fButtonDist = (m_fBaseButtonDistance * m_fDistance) / CQTESettings.s_iThirdMilestoneMultiplier;

            float fMinToMax = Vector2.Distance(new Vector2(0.0f, m_iMaxHeight), new Vector2(0.0f, m_iMinHeight));

            int iMaxShownButtons = (int)(fMinToMax / fButtonDist) + 1;
            if (iMaxShownButtons < 1)
                iMaxShownButtons = 1;

            //Instantiate the amount of images needed
            if(m_ButtonPrefab != null)
            {
                for(int i = 0; i < iMaxShownButtons && i < m_iButtonsCount * m_iButtonCountMultiplier; ++i)
                {
                    GameObject image = Instantiate(m_ButtonPrefab) as GameObject;
                    image.transform.SetParent(transform, false);

                    m_ButtonImages.Add(image.GetComponent<Image>());

                }
            }

            //Ugly for loops, there has to be a better way
            {
                //ONE
                for (float i = 0; i < m_iButtonsCount; ++i)
                {
                    m_StartYPositions.Add((i * m_fBaseButtonDistance * m_fDistance) + m_fDistance);
                }

                float diff = (m_fBaseButtonDistance ) * m_fDistance;

                //TWO
                for (float i = 0; i < m_iButtonsCount * CQTESettings.s_iFirstMilestoneMultiplier; ++i)
                {
                    m_StartYPositions.Add((i * (m_fBaseButtonDistance / CQTESettings.s_iFirstMilestoneMultiplier) * m_fDistance)
                        + m_StartYPositions[m_StartYPositions.Count - 1 - (int)i] + diff);

                }

                diff = (m_fBaseButtonDistance / CQTESettings.s_iFirstMilestoneMultiplier) * m_fDistance;

                //THREE
                for (float i = 0; i < m_iButtonsCount * CQTESettings.s_iSecondMilestoneMultiplier; ++i)
                {
                    m_StartYPositions.Add((i * (m_fBaseButtonDistance / CQTESettings.s_iSecondMilestoneMultiplier) * m_fDistance)
                        + m_StartYPositions[m_StartYPositions.Count - 1 - (int)i] + diff);
                }

                diff = (m_fBaseButtonDistance / CQTESettings.s_iSecondMilestoneMultiplier) * m_fDistance;

                //FOUR
                for (float i = 0; i < m_iButtonsCount * CQTESettings.s_iThirdMilestoneMultiplier; ++i)
                {
                    m_StartYPositions.Add((i * (m_fBaseButtonDistance / CQTESettings.s_iThirdMilestoneMultiplier) * m_fDistance)
                        + m_StartYPositions[m_StartYPositions.Count - 1 - (int)i] + diff);
                }
            }


            for (int i = 0; i < m_ButtonImages.Count; ++i)
            {
                Vector2 OffsetPosition = m_ButtonImages[i].rectTransform.anchoredPosition;
                

                OffsetPosition.y = m_StartYPositions[m_iMaxShownButtonIndex];


                m_ButtonImages[i].rectTransform.anchoredPosition = OffsetPosition;

                m_ButtonImagesIndex.Add(i);
                ChangeButtonAtIndex(i);

                ++m_iMaxShownButtonIndex;
            }

            //Decrease value so it's accurate, this wasn't done earlier because m_iMaxShownButton 
            //needs to have a value of atleast 0 for the above for loop to work correctly
            --m_iMaxShownButtonIndex;
            
        }

        //Resize height if the camera doesn't match the exact pixel height/width of the ui canvas
        float ResizedHeight = (float)m_iMaxHeight / 1653;
        ResizedHeight *= Camera.main.pixelHeight;

        Vector2 HeightPos = new Vector2(0.0f, 0.0f);
        HeightPos.y += m_iMaxHeight;
        HeightPos.y /= GetComponentInParent<CanvasScaler>().referenceResolution.y;
        HeightPos.y *= Screen.height;
        
        float QTEButtonCutoff = (HeightPos.y + Screen.height / 2.0f) / Screen.height;

        Shader.SetGlobalFloat("_QTEButtonCutoff", QTEButtonCutoff);

        m_fPressForgivenessDistance = (CQTESettings.s_fPressForgiveness / 100) * m_fDistance;

    }

    private void ResetEvent()
    {
        GenerateNewSequence();

        m_ButtonImagesIndex.Clear();
        m_fTimeSinceStart = 0.0f;
        m_iMaxShownButtonIndex = 0;
        m_iCurrentButtonIndex = 0;
        m_iCurrentListIndex = 0;
        m_bFailed = false;
        m_bInitialized = false;
        m_PressArea.material.SetFloat("_ButtonOverlap", 0.0f);

        m_iCurrentMilestone = 0;
        m_fNotePercentageHit = 0.0f;

        foreach (Image buttonImage in m_ButtonImages)
            buttonImage.enabled = true;

        for (int i = 0; i < m_ButtonImages.Count; ++i)
        {
            Vector2 OffsetPosition = m_ButtonImages[i].rectTransform.anchoredPosition;
            OffsetPosition.y = m_StartYPositions[m_iMaxShownButtonIndex];
            m_ButtonImages[i].rectTransform.anchoredPosition = OffsetPosition;

            m_ButtonImagesIndex.Add(i);

            ChangeButtonAtIndex(i);

            ++m_iMaxShownButtonIndex;
        }

        //Decrease value so it's accurate, this wasn't done earlier because m_iMaxShownButton 
        //needs to have a value of atleast 0 for the above for loop to work correctly
        --m_iMaxShownButtonIndex;
    }

    private void GenerateNewSequence()
    {
        m_iButtonSequenceList.Clear();

        for(int i = 0; i < m_iButtonsCount * m_iButtonCountMultiplier; ++i)
        {
            m_iButtonSequenceList.Add(Random.Range(0, (int)Buttons.BUTTON_TYPES_COUNT));
        }
        
    }
    
    public void Update()
    {
        if (m_bQTEActive == true)
        {
            //Check if charge is off cooldown here for lack of a better place
            if(m_bInitialized == false)
            {
                m_PlayerMetalMode.CheckChargeOffCooldown();
                m_bInitialized = true;
            }

            m_fTimeSinceStart += Time.unscaledDeltaTime;
            ImageUpdate();
            PressEvent();

            //Solution can maybe be optimized, add mesh renderer to press area and use that instead?
            m_PressArea.material.SetFloat("_ButtonOverlap", System.Convert.ToSingle(RectOverlap(m_PressArea.rectTransform, m_ButtonImages[m_iCurrentListIndex].rectTransform)));

            //End event either when the final button has been pressed or
            //when the player has failed
            if (m_iCurrentButtonIndex == m_iButtonsCount * m_iButtonCountMultiplier || m_bFailed == true)
            {
                m_bQTEActive = false;

                EndQTE();

                //Reset event here for lack of a better place
                ResetEvent();
            }
        }
        //else if(m_bInitialized == true)
        //{
        //    EndQTE();

        //    //Reset event here for lack of a better place
        //    ResetEvent();
        //}
    }



    private void PressEvent()
    {
        
        if (m_iInput != -1 )
        {

            int CurrentButtonImageIndex = m_iCurrentListIndex;

            //Placeholder?
            bool SequenceButton = true;

            switch (m_iInput)
            {
                case (int)Buttons.CROSS:
                    break;

                case (int)Buttons.SQUARE:
                    break;

                case (int)Buttons.TRIANGLE:
                    break;

                case (int)Buttons.CIRCLE:
                    break;

                default:
                    SequenceButton = false;
                    break;
            }

            if (SequenceButton == true)
            {
                if (RectOverlap(m_PressArea.rectTransform, m_ButtonImages[CurrentButtonImageIndex].rectTransform) == true)
                {

                    if (m_iInput == m_iButtonSequenceList[m_iCurrentButtonIndex])
                    {

                        HitNote();

                        ++m_iCurrentListIndex;
                        if (m_iCurrentListIndex == m_ButtonImages.Count)
                            m_iCurrentListIndex = 0;

                        ++m_iCurrentButtonIndex;
                        ++m_iMaxShownButtonIndex;
                        m_ButtonImagesIndex[CurrentButtonImageIndex] = m_iMaxShownButtonIndex;

                        Vector2 vImagePos = m_ButtonImages[CurrentButtonImageIndex].rectTransform.anchoredPosition;

                        if (m_iMaxShownButtonIndex < m_iButtonsCount * m_iButtonCountMultiplier)
                            vImagePos.y = m_StartYPositions[m_iMaxShownButtonIndex] - m_fButtonSpeed * m_fTimeSinceStart;
                        else
                            vImagePos.y = float.MaxValue;

                        m_ButtonImages[CurrentButtonImageIndex].rectTransform.anchoredPosition = vImagePos;


                        if (m_ButtonImagesIndex[CurrentButtonImageIndex] < m_iButtonsCount * m_iButtonCountMultiplier)
                            ChangeButtonAtIndex(CurrentButtonImageIndex);
                        else
                            m_ButtonImages[CurrentButtonImageIndex].enabled = false;

                        //Play accord sound here since we only want it to play if a note was hit
                        CSoundBank.Instance.QTENote(m_iInput);

                    }
                    else
                    {
                        m_bFailed = true;
                        CSoundBank.Instance.QTEFailed(m_iInput);
                    }
                }
                else
                {
                    m_bFailed = true;
                    CSoundBank.Instance.QTEFailed(m_iInput);
                }

                
            }

            //Reset input
            m_iInput = -1;
        }
    }

    private void ImageUpdate()
    {
        int NextButtonIndex = m_iCurrentButtonIndex;

        for (int i = 0; i < m_ButtonImages.Count; ++i)
        {
            
            Vector2 vImagePos = m_ButtonImages[i].rectTransform.anchoredPosition;

            if(vImagePos.y + GetComponent<RectTransform>().anchoredPosition.y <= m_iMinHeight)
            {
                m_bFailed = true;
            }
            
            vImagePos.y -= m_fButtonSpeed * Time.unscaledDeltaTime;

            m_ButtonImages[i].rectTransform.anchoredPosition = vImagePos;
        }

        //Update m_iCurrentButtonIndex after the for loop is done
        m_iCurrentButtonIndex = NextButtonIndex;
    }
    
    private void ChangeButtonAtIndex(int ButtonIndex)
    {
        m_ButtonImages[ButtonIndex].overrideSprite = CQTESettings.s_SpriteList[m_iButtonSequenceList[m_ButtonImagesIndex[ButtonIndex]]];
    }
    

    private bool RectOverlap(RectTransform firstRect, RectTransform secondRect)
    {

        if (Mathf.Abs(firstRect.anchoredPosition.y - secondRect.anchoredPosition.y) > (firstRect.rect.height / 2.0f + secondRect.rect.height / 2.0f) + m_fPressForgivenessDistance)
            return false;

        return true;
    }

    private void HitNote()
    {
        if (m_iCurrentButtonIndex == m_iButtonsCount)
            ++m_iCurrentMilestone;
        else if (m_iCurrentButtonIndex == m_iButtonsCount * CQTESettings.s_iSecondMilestoneMultiplier - 1)
            ++m_iCurrentMilestone;
        else if (m_iCurrentButtonIndex == (m_iButtonsCount * m_iButtonCountMultiplier) - CQTESettings.s_iThirdMilestoneMultiplier)
            ++m_iCurrentMilestone;

        float NotePercentage = 0.0f;
        
        switch (m_iCurrentMilestone)
        {
            case 0:
                NotePercentage = (1.0f / m_iButtonsCount) * 0.25f;
                break;
            case 1:
                NotePercentage = (1.0f / (m_iButtonsCount * CQTESettings.s_iFirstMilestoneMultiplier)) * 0.25f;
                break;
            case 2:
                NotePercentage = (1.0f / (m_iButtonsCount * CQTESettings.s_iSecondMilestoneMultiplier)) * 0.25f;
                break;
            case 3:
                NotePercentage = (1.0f / (m_iButtonsCount * CQTESettings.s_iThirdMilestoneMultiplier)) * 0.25f;
                break;
        }

        m_fNotePercentageHit += NotePercentage;
        m_PlayerMetalMode.HitNote(NotePercentage);


    }

    
    private void EndQTE()
    {

        m_PlayerMetalMode.CalculateMilestone(m_fNotePercentageHit);
        m_PlayerMetalMode.m_bOnCooldown = true;

        
        m_PlayerMetalMode.SetChargeOnCooldown();
    }

    public static void ForceReset()
    {
        if (instance.m_bInitialized == true)
        {
            instance.EndQTE();

            instance.ResetEvent();
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
