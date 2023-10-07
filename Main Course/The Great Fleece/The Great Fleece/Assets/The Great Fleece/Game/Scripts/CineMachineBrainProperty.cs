using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CineMachineBrainProperty : MonoBehaviour
{
    private Camera _camera;
    private CinemachineBrain cinemachineBrain;

    private void Start()
    {
        _camera = Camera.main;
        cinemachineBrain = _camera.GetComponent<CinemachineBrain>();
        if (cinemachineBrain != null)
        {
            CinemachineBlendDefinition customBlend = new CinemachineBlendDefinition();
            customBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
            customBlend.m_Time = 2f;
            cinemachineBrain.m_DefaultBlend = customBlend;
        }
        else
        {
            Debug.LogError("CinemachineBrain component not found on the camera.");
        }
    }

    private void OnDisable()
    {
        SetDefaultProperties();
    }

    public void SetDefaultProperties()
    {
        CinemachineBlendDefinition customBlend = new CinemachineBlendDefinition();
        customBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        cinemachineBrain.m_DefaultBlend = customBlend;
    }
}


