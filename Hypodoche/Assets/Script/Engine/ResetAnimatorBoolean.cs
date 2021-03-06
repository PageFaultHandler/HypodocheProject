﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hypodoche
{
    public class ResetAnimatorBoolean : StateMachineBehaviour
    {
        public string _targetBool;
        public bool _status;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(_targetBool, _status);
        }
    }
}
