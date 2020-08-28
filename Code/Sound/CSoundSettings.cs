using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoundSettings : MonoBehaviour
{

    [Range(0.0f, 1.0f)]
    [SerializeField] private float m_fVolume = 0.25f;
    //TODO ADD DIFFERENT VOLUME SETTINGS
    //MUSIC
    //VO
    //SFX

    [SerializeField] private bool m_bDevMute = true;

    FMOD.Studio.Bus m_MasterBus;
    // Start is called before the first frame update
    void Start()
    {
        m_MasterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            m_bDevMute = !m_bDevMute;
        }
        if(m_bDevMute == true)
        {
            m_MasterBus.setVolume(0.0f);
        }
        else m_MasterBus.setVolume(m_fVolume);
    }

}
