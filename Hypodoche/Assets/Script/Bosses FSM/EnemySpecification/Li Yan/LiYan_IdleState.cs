﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hypodoche
{
    public class LiYan_IdleState : IdleState
    {
        #region Variables
        private LiYan _liYan;

        public LiYan_IdleState(Entity entity, FiniteStateMachine stateMachine, string animationName, D_IdleState idleData, LiYan liYan)
            : base(entity, stateMachine, animationName, idleData)
        {
            _liYan = liYan;
        }
        #endregion

        #region Methods
        public override void Enter()
        {
            base.Enter();
            if (_entity._entityData.health <= 0)
            {
                Debug.Log("cambio stato : idle -> death " + Time.time);
                _stateMachine.ChangeState(_liYan._deathState);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (_isIdleTimeElapsed)
            {
                _stateMachine.ChangeState(_liYan._moveState);
            }
        }

        #endregion
    }
}