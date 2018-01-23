using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using TMPro;

namespace VRTrainer
{
    public class Terminal : MonoBehaviour
    {
        //why do we need a Resistor?
        //The short answer: to limit the current in the LED to a safe value. ... 
        //That means that a change in voltage will produce a proportional change in current. 
        //Current versus voltage is a straight line for a resistor, but not at all for an LED. 
        //Because of this, you can't say that LEDs have “resistance.”

        [SerializeField] public string Name;

        //this is the terminal at the other end of the wire
        //this must be set prior to runtime
        [SerializeField] public Terminal Other;

        //this must be set prior to runtime
        [SerializeField] public TerminalType type;

        //reference to whatever this Terminal is Connected to
        public GameObject ConnectedGameObject;

        //connected terminal connection point
        //this is used to keep wires connected to a
        //parts terminal
        public GameObject ConnectionPoint;

        //We need to know if this terminal is selected or not
        //in order to release it from the connection points
        public VRInteractionController VRController;

        //Debug text
        [SerializeField] private TextMeshPro DebugText;

        public bool IsConnected()
        {
            return ConnectedGameObject != null;
        }

        private void OnEnable()
        {
            VRController = this.gameObject.GetComponent<VRInteractionController>();
        }

        void Update()
        {

            if (VRController != null)
            {
                //if this terminal is not selected and it is not
                //on a part. then teleport it to the connection point
                if (VRController.IsSelected == false)
                {
                    if (type == TerminalType.Wire && ConnectionPoint != null)
                    {
                        if (transform.position != ConnectionPoint.transform.position)
                        {
                            Teleport(ConnectionPoint.transform.position);
                        }
                    }
                }
                //else
                //{
                //    //selected terminals should not have connection points
                //    if (ConnectionPoint != null)
                //        ConnectionPoint = null;
                //}
            }
        }

        void OnTriggerEnter(Collider collisionInfo)
        {
            //ConnectedGameObject = collisionInfo.gameObject.transform.parent.gameObject; //this gets to parent object
            if (collisionInfo.gameObject.tag.Equals("Terminal"))
            {
                ConnectedGameObject = collisionInfo.gameObject;
                if (DebugText != null)
                {
                    Terminal tempTerminal = ConnectedGameObject.GetComponent<Terminal>();
                    if (tempTerminal != null)
                    {
                        DebugText.text = this.gameObject.name + " collided with " + ConnectedGameObject.name;
                        //detect if connectedObject has a connection point
                        if (tempTerminal.ConnectionPoint != null)
                        {
                            ConnectionPoint = tempTerminal.ConnectionPoint;
                        }
                    }
                }
            }
        }

        void OnTriggerExit(Collider collisionInfo)
        {
            if (collisionInfo.gameObject.tag.Equals("Terminal"))
            {
                Terminal temp = ConnectedGameObject.GetComponent<Terminal>();

                if (DebugText != null && temp != null)
                {
                    DebugText.text = this.gameObject.name + " collided with " + ConnectedGameObject.name;
                }

                ConnectedGameObject = null;
            }
        }

        public void Teleport(Vector3 target)
        {
            //teleport
            transform.position = new Vector3(target.x, transform.position.y, target.z);
        }
    }
}
