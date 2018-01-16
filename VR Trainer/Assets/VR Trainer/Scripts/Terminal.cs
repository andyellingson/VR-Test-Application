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
        [SerializeField] public GameObject ConnectedGameObject;

        //Debug text
        [SerializeField] private TextMeshPro DebugText;

        public bool IsConnected()
        {
            return ConnectedGameObject != null;
        }

        void OnTriggerEnter(Collider collisionInfo)
        {
            //ConnectedGameObject = collisionInfo.gameObject.transform.parent.gameObject; //this gets to parent object
            if (collisionInfo.gameObject.tag.Equals("Terminal"))
            {
                ConnectedGameObject = collisionInfo.gameObject;
                if (DebugText != null) DebugText.text = "InteractiveItem:\t " + Name + " collided with " + ConnectedGameObject.GetComponent<Terminal>().Name;
            }
        }

        void OnTriggerExit(Collider collisionInfo)
        {
            if (collisionInfo.gameObject.tag.Equals("Terminal"))
            {
                Terminal temp = ConnectedGameObject.GetComponent<Terminal>();
                if (DebugText != null && temp != null) DebugText.text = "InteractiveItem:\t " + Name + " stopped colliding with " + temp.Name;
                ConnectedGameObject = null;
            }
        }
    }
}
