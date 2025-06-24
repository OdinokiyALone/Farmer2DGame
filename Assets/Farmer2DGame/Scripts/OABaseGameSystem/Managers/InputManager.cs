using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OABaseGameSystem
{
    public class InputManager
    {
        public enum ActionsMapName
        {
            UI,
            Player,
        }
        public Pusher input;


        public void ActionEnable(ActionsMapName action)
        {
            string mapName = action.ToString();

            foreach (InputActionMap map in input.asset.actionMaps)
            {
                if (map.name == mapName) map.Enable();
                else map.Disable();
            }
        }

        public InputManager()
        {
            input = new Pusher();
            ActionEnable(ActionsMapName.UI);
        }
    }
}