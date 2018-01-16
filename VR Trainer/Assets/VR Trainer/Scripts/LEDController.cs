using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using TMPro;
namespace VRTrainer
{
    public class LEDController : MonoBehaviour
    {
        //this but be set up prior to runtime
        [SerializeField] private Part MasterPart;
        [SerializeField] private Part LED;

        //We need this to turn the bulb on and off
        //it needs to be set up prior to runtime
        [SerializeField] private GameObject Bulb;

        //Debug text
        [SerializeField] private TextMeshPro DebugText;
        
        void FixedUpdate()
        {
            if (!MasterPart.CircuitComplete)
                LED.IsOnCircuit = false;

            FlipSwitch(MasterPart.CircuitComplete && LED.IsOnCircuit);
        }

        void FlipSwitch(bool IsOn)
        {
            if (IsOn)
            {
                Color color = Color.green;
                Bulb.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                Bulb.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
            }
            else
            {
                Bulb.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                Bulb.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.01f, 0.01f, 0.01f));
            }
        }
    }
}