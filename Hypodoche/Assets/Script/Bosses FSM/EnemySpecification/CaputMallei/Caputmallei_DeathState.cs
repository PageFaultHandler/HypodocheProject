﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Hypodoche
{
    public class Caputmallei_DeathState : State
    {
        #region Variables
        private Caputmallei _caputmallei;
        private bool _isDead = false;



        public Caputmallei_DeathState(Entity entity, FiniteStateMachine stateMachine, string animationName, Caputmallei caputmallei)
            : base(entity, stateMachine, animationName)
        {
            _caputmallei = caputmallei;
        }
        #endregion

        #region Methods

         public override void ExecuteAfterAnimation()
        {
            base.ExecuteAfterAnimation();
            Debug.Log("Caput è morto, avanziamo");
            if(!_isDead){
                _isDead = true;
                CampaignProgressionManager cpm = GameObject.FindObjectOfType<CampaignProgressionManager>();
                cpm.Advance(false);
            }
            _caputmallei.DestroyBoss();
        }
        public override void Enter()
        {
            base.Enter();
            _animWaiter.StartCoroutine(_animWaiter.waitTillTheAnimationEnds(_entity._animator,this));
        }

        public override void Exit()
        {
            base.Exit();

        }




        public override void Update()
        {
            base.Update();
           
        }



        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        #endregion
    }
}
