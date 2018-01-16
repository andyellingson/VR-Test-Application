using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using TMPro;

namespace VRTrainer
{
    public class VRInteractionController : MonoBehaviour
    {

        //Movement properties
        [SerializeField] private VRInteractiveItem m_InteractiveItem;
        [SerializeField] private bool IsSelected;
        [SerializeField] private GameObject MainCamera;
        [SerializeField] private Reticle Target;

        [SerializeField] private TextMeshPro DebugText;

        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnDown += HandlePickUp;
        }

        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnDown -= HandlePickUp;
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

        //VRInteractiveItem Events
        private void HandlePickUp()
        {
            if (DebugText != null) DebugText.text = "InteractiveItem:\tWire PickUp IsSelected = " + IsSelected;
            IsSelected = !IsSelected;
        }

        //Handle the Over event
        private void HandleOver()
        {
            if (DebugText != null) DebugText.text = "InteractiveItem:\t" + gameObject.name + " Over";
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
    }
}
