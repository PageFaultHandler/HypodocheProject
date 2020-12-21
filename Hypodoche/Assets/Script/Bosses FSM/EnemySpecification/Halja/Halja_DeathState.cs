﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hypodoche{
    public class Halja_DeathState : State
    {
         #region Variables
        private Halja _halja;



        public Halja_DeathState(Entity entity, FiniteStateMachine stateMachine, string animationName, Halja halja)
            : base(entity, stateMachine, animationName)
        {
            _halja = halja;
        }
        #endregion

        #region Methods
        public override void Enter()
        {
            base.Enter();
            SceneManager.LoadScene(3);// Victory
            _halja.DestroyBoss();
        }

        public override void Exit()
        {
            base.Exit();

        }
        public override void Update()
        {
            base.Update();
           
        }

        #endregion
    }
}