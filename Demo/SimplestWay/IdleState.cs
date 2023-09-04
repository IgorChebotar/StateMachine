﻿using SimpleMan.StateMachine;
using UnityEngine;

namespace SimpleMan.StateMachineSimpleDemo
{
    public class IdleState : IState
    {
        public void Start()
        {
            GameObject actor = SceneManager.instance.actor;
            actor.transform.position = Vector3.zero;
        }

        public void Stop()
        {
            
        }
    }
}

