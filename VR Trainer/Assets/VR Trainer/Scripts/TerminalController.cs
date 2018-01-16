using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VRTrainer
{
    [ExecuteInEditMode()]
    public class TerminalController : MonoBehaviour
    {
        //Wire connecting terminals
        public Transform[] points;
        public LineRenderer lineRenderer;
        public bool UnitsConnected;

        //Terminals
        [SerializeField] private GameObject TerminalOneObject;  //Must be set up before runtime
        [SerializeField] private Terminal TerminalOne;

        [SerializeField] private GameObject TerminalTwoObject;  //Must be set up before runtime
        [SerializeField] private Terminal TerminalTwo;

        //Debug Text
        [SerializeField] private TextMeshPro TerminalDebugText;

        // Use this for initialization
        void Start()
        {
            UnitsConnected = false;
            TerminalOne = TerminalOneObject.GetComponent<Terminal>();
            TerminalTwo = TerminalTwoObject.GetComponent<Terminal>();
            lineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            //Draw LineRender line
            for (int i = 0; i < points.Length; i++)
            {
                lineRenderer.SetPosition(i, points[i].position);
            }

            //// connect
            //if (!UnitsConnected && TerminalOne.ConnectedGameObject != null && TerminalTwo.ConnectedGameObject != null)
            //{
            //    //both terminals in this wire are connected to a objects
            //    //we need to let those objects know who they are connected to
            //    Part terminalOnePart = TerminalOne.ConnectedGameObject.GetComponent<Part>();
            //    Part terminalTwoPart = TerminalTwo.ConnectedGameObject.GetComponent<Part>();

            //    terminalOnePart.ConnectedParts.Add(terminalTwoPart);
            //    terminalTwoPart.ConnectedParts.Add(terminalOnePart);

            //    UnitsConnected = true;
            //}
            //else
            //{
            //    //disconnect happens in the terminals
            //}
        }
    }
}
