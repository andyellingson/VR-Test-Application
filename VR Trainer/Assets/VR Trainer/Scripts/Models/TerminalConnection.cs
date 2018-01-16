using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTrainer
{
    public class TerminalConnection
    {
        public string TerminalName { get; set; }
        //this terminal keeps track of the object at the other end of the wire if one exists 
        public Terminal ConnectedTerminal { get; set; }
        public TerminalType ConnectedTerminalType { get; set; }
        public TerminalConnection(int id, string name, Part part, Terminal terminal)
        {
            TerminalName = name;
            ConnectedTerminal = terminal;
        }
    }
}
