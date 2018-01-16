using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using TMPro;

public class SurfaceController : MonoBehaviour {

    [SerializeField] private VRInteractiveItem m_InteractiveItem;
    public TextMeshPro DebugText;

    private void OnEnable()
    {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_InteractiveItem.OnClick += HandleClick;
        m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
        m_InteractiveItem.OnUp += HandlePutDown;
        m_InteractiveItem.OnDown += HandlePickUp;
    }


    private void OnDisable()
    {
        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
        m_InteractiveItem.OnClick -= HandleClick;
        m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
        m_InteractiveItem.OnUp -= HandlePutDown;
        m_InteractiveItem.OnDown -= HandlePickUp;
    }


    //Handle the Over event
    private void HandleOver()
    {
        DebugText.text = "InteractiveItem:\tLamp Over";
        //IsMoviePlaying = true;
        // m_Renderer.material = m_OverMaterial;
    }


    //Handle the Out event
    private void HandleOut()
    {
        DebugText.text = "InteractiveItem:\tLamp Out";
        // IsMouseOver = false;
        // m_Renderer.material = m_NormalMaterial;
    }


    //Handle the Click event
    private void HandleClick()
    {
        DebugText.text = "InteractiveItem:\tLamp Click";
    }

    private void HandlePickUp()
    {
        DebugText.text = "InteractiveItem:\tLamp Pick Up";
    }


    private void HandlePutDown()
    {
        DebugText.text = "InteractiveItem:\tLamp PutDown";
        // moving = false;
    }

    //Handle the DoubleClick event
    private void HandleDoubleClick()
    {
        DebugText.text = "InteractiveItem:\tLamp Double Click";
        //  m_Renderer.material = m_DoubleClickedMaterial;
    }
}
