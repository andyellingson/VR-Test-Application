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
        public Stack<GameObject> CurrentEnergyPackets;
        public int MaxEnergyPackets;
        private int energyPacketCount;
        public Terminal FirstTarget;
        public float Interval;
        private float timer;

        // Use this for initialization
        void Start()
        {
            timer = Interval;
            CurrentEnergyPackets = new Stack<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            if(MasterPart.CircuitComplete == false && CurrentEnergyPackets.Count > 0)
            {
                DestroyEnergyPackets();
            }
            timer -= Time.deltaTime;
            if (timer <= 0 && MasterPart.CircuitComplete && CurrentEnergyPackets.Count < MaxEnergyPackets)
            {
                //reset timer
                timer = Interval;
                energyPacketCount++;
                var newEnergyPacket = Instantiate(EnergyPacketPrefab, transform.position, transform.rotation);
                CurrentEnergyPackets.Push(newEnergyPacket);
                EnergyPacket temp = newEnergyPacket.GetComponent<EnergyPacket>();
                temp.CurrentTarget = FirstTarget.gameObject;
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