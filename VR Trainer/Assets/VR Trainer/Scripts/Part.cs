using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTrainer
{
    public class Part : MonoBehaviour
    {
        //unique identifier
        [SerializeField] private int Id;
        [SerializeField] private string Name;

        //MasterUnits look for complete connections
        [SerializeField] private bool IsMaster;

        //this bool is used to track if this part is on the current ciruit
        [SerializeField] public bool IsOnCircuit;

        //Set when circuit is complete
        [SerializeField] public bool CircuitComplete;

        //Terminals
        [SerializeField] public GameObject PositiveTerminal;
        [SerializeField] public GameObject NegativeTerminal;

        void OnStart()
        {
            CircuitComplete = false;
            IsOnCircuit = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (IsMaster)
            {
                if (!CheckCircuit())
                {
                    //make sure circuit is marked as incomplete
                    CircuitComplete = false;
                }
            }
        }

        public bool CheckCircuit()
        {
            //lets start on our positive terminal
            Terminal firstTerminal = PositiveTerminal.GetComponent<Terminal>();
            Terminal current = firstTerminal;
            while(current != null)
            {
                current = HopTerminal(current);


                if(current != null)
                {
                    //if current is a part mark it as being on the circuit
                    //this doesn't work if structure is not correct
                    Part temp = current.transform.parent.GetComponent<Part>();
                    if (temp != null)
                        temp.IsOnCircuit = true;

                    if (current.Other != null)
                        current = current.Other;
                    else
                        return false;   //no where else to go
                }

                if(current == firstTerminal)
                {
                    CircuitComplete = true;
                    return true;
                }
            }
            return false;
        }

        //Jumps from current terminal to the 
        //terminal of the object connected to this terminal
        public Terminal HopTerminal(Terminal terminal)
        {
            Terminal newTerminal = null;
            GameObject newTerminalObject = terminal.ConnectedGameObject;

            if (newTerminalObject != null)
                newTerminal = newTerminalObject.GetComponent<Terminal>();

            return newTerminal;
        }
    }
}