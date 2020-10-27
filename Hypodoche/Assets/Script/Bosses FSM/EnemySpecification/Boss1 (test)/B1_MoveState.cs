﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hypodoche
{
    public class B1_MoveState : MoveState
    {
        #region Variables
        private Boss1 _boss1;
        public B1_MoveState(Entity entity, FiniteStateMachine stateMachine, string animationName, D_MoveState moveData, Boss1 boss)
            : base(entity, stateMachine, animationName, moveData)
        {
            _boss1 = boss;
        }
        #endregion

        #region Methods
        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (isDetectingWall)
            {
                _boss1._idleState.setFlipAfterIdle(true);
                _stateMachine.ChangeState(_boss1._idleState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
        #endregion
    }
}