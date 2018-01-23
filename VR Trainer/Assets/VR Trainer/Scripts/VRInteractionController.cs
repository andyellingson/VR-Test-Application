using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using TMPro;
using System;

namespace VRTrainer
{
    public class VRInteractionController : MonoBehaviour
    {

        //Movement properties
        [SerializeField] private VRInteractiveItem m_InteractiveItem;
        [SerializeField] private VRInput m_SwipeDetection;
        [SerializeField] public bool IsSelected;
        [SerializeField] private GameObject MainCamera;
        [SerializeField] private Reticle Target;

        [SerializeField] private TextMeshPro DebugText;

        private VRInput.SwipeDirection lastSwipe = VRInput.SwipeDirection.NONE;

        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnClick += HandleOnClick;
            m_InteractiveItem.OnDoubleClick += HandleOnDoubleClick;
            m_SwipeDetection.OnSwipe += HandleSwipe;
        }

        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnClick -= HandleOnClick;
            m_InteractiveItem.OnDoubleClick += HandleOnDoubleClick;
            m_SwipeDetection.OnSwipe -= HandleSwipe;
        }

        // Use this for initialization
        void Start()
        {
            IsSelected = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (IsSelected)
                Move(Target.ReticleTransform.position); //move to mouse position
        }

        //Handle the Over event
        private void HandleOver()
        {
            if (DebugText != null) DebugText.text = "Mouse Over\t" + gameObject.name;
        }

        //Handle the Over event
        private void HandleOnClick()
        {
            if (DebugText != null) DebugText.text = "Click " + gameObject.name + "\t IsSelected = " + IsSelected;
            IsSelected = !IsSelected;
            if(IsSelected && this.gameObject.GetComponent<Terminal>().type == TerminalType.Wire)
                this.gameObject.GetComponent<Terminal>().ConnectionPoint = null;

        }

        //doubleclick to disconnect wire terminal
        private void HandleOnDoubleClick()
        {
            if (DebugText != null) DebugText.text = "Double Click " + gameObject.name;
        }
        public void Move(Vector3 target)
        {
            Vector3 reticlePosition;
            float speed = 1;
            float step = speed * Time.deltaTime;
            reticlePosition = new Vector3(target.x, transform.position.y, target.z);
            //Move
            transform.position = Vector3.MoveTowards(transform.position, reticlePosition, step);

            //teleport
            //transform.position = new Vector3(target.x, transform.position.y, target.z);
        }

        private void HandleSwipe(VRInput.SwipeDirection obj)
        {
            if (IsSelected)
            {
                Quaternion newRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
                if (DebugText != null) DebugText.text = "Swipe " + obj;

                if (lastSwipe !=  null)
                {
                    if (obj == VRInput.SwipeDirection.DOWN && lastSwipe == VRInput.SwipeDirection.DOWN)
                    {
                        newRotation *= Quaternion.Euler(90, 0, 0); // this add a 90 degrees Z rotation
                        if (DebugText != null) DebugText.text = "Swipe " + obj + " " + " Rotation: " + newRotation + " " + gameObject.name;
                    }
                    else if (obj == VRInput.SwipeDirection.LEFT && lastSwipe == VRInput.SwipeDirection.LEFT)
                    {
                        newRotation *= Quaternion.Euler(0, 90, 0); // this add a 90 degrees Y rotation
                        if (DebugText != null) DebugText.text = "Swipe " + obj + " " + " Rotation: " + newRotation + " " + gameObject.name;
                    }
                    else if (obj == VRInput.SwipeDirection.RIGHT && lastSwipe == VRInput.SwipeDirection.RIGHT)
                    {
                        newRotation *= Quaternion.Euler(0, -90, 0); // this add a 90 degrees Y rotation
                        if (DebugText != null) DebugText.text = "Swipe " + obj + " " + " Rotation: " + newRotation + " " + gameObject.name;
                    }
                    else if (obj == VRInput.SwipeDirection.UP && lastSwipe == VRInput.SwipeDirection.UP)
                    {
                        newRotation *= Quaternion.Euler(-90, 0, 0); // this subtracts a 90 degrees Z rotation
                        if (DebugText != null) DebugText.text = "Swipe " + obj + " " + " Rotation: " + newRotation + " " + gameObject.name;
                    }
                    else
                    {
                        lastSwipe = obj;
                    }
                }
                else
                {
                    //first swipe
                    lastSwipe = obj;
                }

                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 20 * Time.deltaTime);
            }
        }
    }
}
