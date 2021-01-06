﻿using System.Collections;
using System.Collections.Generic;
using Hypodoche;
using UnityEngine;


namespace Hypodoche{
    public class LittleHelper : MonoBehaviour,Traps
    {
        #region Variables
        protected float _recover = 20f;
        protected float _deadTime = 16f;
        protected  float _startTime;


        private bool _active = true;

        #endregion
    

        #region methods

        public void Start(){
            _active = true;
            _startTime = Time.time;
        }

        public void Update(){
            if (!_active && Time.time >= _startTime + _deadTime) { 
                _active = true;
                gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            }
        }


        public void OnTriggerEnter(Collider  col)
        {
            if(!_active) return;

            if(col.gameObject.CompareTag("Player"))
            {
                float maxStamina = col.gameObject.GetComponent<PlayerStatus>().getMaxStamina();
                float currentStamina = col.gameObject.GetComponent<PlayerStatus>().getCurrentStamina();
                if (maxStamina == currentStamina) return; 
                
                if(currentStamina <= maxStamina/4) col.gameObject.GetComponent<PlayerStatus>().setStamina(_recover *2f);
                else if(currentStamina <= maxStamina/2) col.gameObject.GetComponent<PlayerStatus>().setStamina(_recover *1.5f);
                else col.gameObject.GetComponent<PlayerStatus>().setStamina(_recover);
                _active = false;
                gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                _startTime = Time.time;
            }
        }


        public string SendDataTrap()
        {
            throw new System.NotImplementedException();
        }
        #endregion

    }
}