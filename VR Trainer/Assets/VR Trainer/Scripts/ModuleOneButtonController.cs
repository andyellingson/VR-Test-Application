using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VRStandardAssets.Utils;

public class ModuleOneButtonController : MonoBehaviour
{

    [SerializeField] private VRInteractiveItem m_InteractiveItem;

    //ui button
    public GameObject ModuleOneButton;

    //debug screen info
    public TextMeshPro DebugText;
    public string StartText;
    public string StopText;
    public TextMeshPro ButtonText;

    private bool ModuleStarted;

    //ModuleOne components and connectors
    public GameObject Bulb;
    public GameObject Battery;
    public GameObject WireOne;
    public GameObject WireTwo;
    public GameObject Instructions;

    private void Awake()
    {
        ModuleStarted = true;
    }

    private void FixedUpdate()
    {

    }

    private void OnEnable()
    {
        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
        m_InteractiveItem.OnClick += HandleClick;
        m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
        m_InteractiveItem.OnUp += HandleButtonUp;
        m_InteractiveItem.OnDown += HandleButtonDown;
    }


    private void OnDisable()
    {
        m_InteractiveItem.OnOver -= HandleOver;
        m_InteractiveItem.OnOut -= HandleOut;
        m_InteractiveItem.OnClick -= HandleClick;
        m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
        m_InteractiveItem.OnUp -= HandleButtonUp;
        m_InteractiveItem.OnDown -= HandleButtonDown;
    }


    //Handle the Over event
    private void HandleOver()
    {
        DebugText.text = "InteractiveItem:\tButton Over";
    }


    //Handle the Out event
    private void HandleOut()
    {
        DebugText.text = "InteractiveItem:\tButton Out";
    }


    //Handle the Click event
    private void HandleClick()
    {
        DebugText.text = "InteractiveItem:\tButton Click";
        ModuleStarted = !ModuleStarted;
        if (!ModuleStarted)
        {
            //show module parts
            Bulb.SetActive(true);
            Battery.SetActive(true);
            WireOne.SetActive(true);
            WireTwo.SetActive(true);
            Instructions.SetActive(true);
            //switch button Text
            ButtonText.text = StopText;
        }
        else
        {
            //show module parts
            Bulb.SetActive(false);
            Battery.SetActive(false);
            WireOne.SetActive(false);
            WireTwo.SetActive(false);
            Instructions.SetActive(false);
            //switch button Text
            ButtonText.text = StartText;

        }
    }

    //Handle the DoubleClick event
    private void HandleDoubleClick()
    {
        DebugText.text = "InteractiveItem:\tButton Double Click";
    }

    private void HandleButtonUp()
    {

    }

    private void HandleButtonDown()
    {
        DebugText.text = "InteractiveItem:\tButtonDown";
        ModuleStarted = !ModuleStarted;
        if (!ModuleStarted)
        {
            //show module parts
            Bulb.SetActive(true);
            Battery.SetActive(true);
            WireOne.SetActive(true);
            WireTwo.SetActive(true);
            Instructions.SetActive(true);
            //switch button Text
            ButtonText.text = StopText;
        }
        else
        {
            //show module parts
            Bulb.SetActive(false);
            Battery.SetActive(false);
            WireOne.SetActive(false);
            WireTwo.SetActive(false);
            Instructions.SetActive(false);
            //switch button Text
            ButtonText.text = StartText;
        }
    }

}
