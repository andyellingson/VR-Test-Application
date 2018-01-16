using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace VRTrainer
{
    public class EnergyPacket : MonoBehaviour
    {

        public GameObject CurrentTarget;
        public GameObject StartingObject; //set up before runtime
        //Debug text
        [SerializeField] private TextMeshPro DebugText;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (CurrentTarget != null)
                Move(CurrentTarget.transform.position);
        }

        public void Move(Vector3 target)
        {
            Vector3 reticlePosition;
            float speed = 0.25f;
            float step = speed * Time.deltaTime;
            reticlePosition = new Vector3(target.x, target.y, target.z);
            //Move
            transform.position = Vector3.MoveTowards(transform.position, reticlePosition, step);
        }

        public void Teleport(GameObject subject, Vector3 target)
        {
            subject.transform.position = new Vector3(target.x, target.y, target.z);
        }

        void OnTriggerEnter(Collider collisionInfo)
        {
            Teleport(this.gameObject, collisionInfo.gameObject.transform.position);

            GameObject collisionObject = collisionInfo.gameObject;
            if (collisionObject == CurrentTarget)
                CurrentTarget = NextTarget(CurrentTarget);

            if (DebugText != null) DebugText.text = "Energy Packet new target:\t " + CurrentTarget.name;
        }

        private GameObject NextTarget(GameObject currentTarget)
        {
            //get terminal component
            Terminal terminal = currentTarget.GetComponent<Terminal>();
            if (terminal != null)
            {
                //try to hop to next terminal 
                terminal = HopTerminal(terminal);
            }

            if (terminal != null)
            {
                //return object at other end of new connection
                return terminal.gameObject;
            }
            else
            {
                //no where else to go circuit is broken
                Destroy(gameObject);
            }
            return null;
        }

        //Jumps from current terminal to the 
        //terminal of the object connected to this terminal
        public Terminal HopTerminal(Terminal terminal)
        {
            Terminal newTerminal = null;
            //on part terminal
            GameObject newTerminalObject = terminal.ConnectedGameObject;

            if (newTerminalObject != null)
            {
                //determine what type of object were connected to
                if (newTerminalObject.transform.parent.gameObject.tag.Equals("Wire"))
                {
                    //were connecting to another wire terminal return this terminal's other end
                    return newTerminalObject.GetComponent<Terminal>().Other;
                }

                newTerminal = newTerminalObject.GetComponent<Terminal>();
            }
            //on other part terminal
            newTerminal = newTerminal.Other;
            if(newTerminal != null)
            {
                newTerminalObject = newTerminal.ConnectedGameObject;

                //energy packets have filled the entire circuit 
                //signal factory to quit making packets
                if(newTerminalObject == StartingObject)
                {
                    EnergyPacketFactory factory = null;
                    Terminal tempTerminal = StartingObject.GetComponent<Terminal>();
                    GameObject tempObject = tempTerminal.ConnectedGameObject;
                    factory = tempObject.GetComponent<EnergyPacketFactory>();
                    if(factory != null)
                    {
                        factory.IsOn = false;
                    }
                }
            }

            //teleport to new starting point
            Teleport(this.gameObject, newTerminalObject.transform.position);

            newTerminal = newTerminalObject.GetComponent<Terminal>();
            
            //go to other end of wire
            return newTerminal.Other;
        }
    }
}