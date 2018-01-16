using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using TMPro;
using System.Linq;
using System;

namespace VRTrainer
{
    public class BuzzerController : MonoBehaviour
    {
        //used to play buzzer sound
        [SerializeField] private AudioSource audioSource;               //set at compile time
        //Debug text for VR Input
        [SerializeField] private TextMeshPro DebugText;                 //set at compile time

        [SerializeField] private Animator BuzzerAnimator;                 //set at compile time

        //Master Part to detect when circuit is complete
        [SerializeField] private Part MasterPart;                 //set at compile time
        [SerializeField] private Part Buzzer;                 //set at compile time

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void FixedUpdate()
        {
        //shut off if circuit is broken
        if (!MasterPart.CircuitComplete)
            Buzzer.IsOnCircuit = false;

        FlipSwitch(MasterPart.CircuitComplete && Buzzer.IsOnCircuit);
        }

        void FlipSwitch(bool IsOn)
        {
            if (IsOn)
            {
                if (DebugText != null) DebugText.text = "Buzzer:\t Speaker is On";
                audioSource.Play();
                BuzzerAnimator.SetBool("IsOnCircuit", true);
            }
            else
            {
                if (DebugText != null) DebugText.text = "Buzzer:\t Speaker is Off";
                audioSource.Stop();
                BuzzerAnimator.SetBool("IsOnCircuit", false);
            }
        }
    }
}