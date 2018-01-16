using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace VRTrainer
{

    public class EnergyPacketFactory : MonoBehaviour
    {

        public Part MasterPart;
        public GameObject EnergyPacketPrefab;
        public GameObject EnergyPacketStartingObject;
        public Stack<GameObject> CurrentEnergyPackets;
        public int MaxEnergyPackets;
        private int energyPacketCount;
        public Terminal FirstTarget;
        public float Interval;
        private float timer;
        public bool IsOn;

        // Use this for initialization
        void Start()
        {
            IsOn = true;
            timer = Interval;
            CurrentEnergyPackets = new Stack<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            if(MasterPart.CircuitComplete == false)
            {
                DestroyEnergyPackets();
                //make sure the factory is ready to start making packets again
                IsOn = true;
                EnergyPacketStartingObject = null;
            }

            //get this terminal's Terminal script
            Terminal terminal = this.gameObject.GetComponent<Terminal>();
            if(terminal != null)
            {
                //set starting point for instantiating EnergyPacket objects
                EnergyPacketStartingObject = terminal.ConnectedGameObject;
                FirstTarget = EnergyPacketStartingObject.GetComponent<Terminal>().Other;
            }

            timer -= Time.deltaTime;
            if (timer <= 0 && MasterPart.CircuitComplete &&  IsOn && EnergyPacketStartingObject != null)
            {
                //reset timer
                timer = Interval;
                energyPacketCount++;
                var newEnergyPacket = Instantiate(EnergyPacketPrefab, EnergyPacketStartingObject.transform.position, EnergyPacketStartingObject.transform.rotation);
                CurrentEnergyPackets.Push(newEnergyPacket);
                EnergyPacket temp = newEnergyPacket.GetComponent<EnergyPacket>();
                temp.CurrentTarget = FirstTarget.gameObject;
                temp.StartingObject = EnergyPacketStartingObject;
            }
        }

        public void DestroyEnergyPackets()
        {
            while(CurrentEnergyPackets.Count > 0)
            {
                var temp = CurrentEnergyPackets.Pop();
                Destroy(temp);
            }
        }
    }
}