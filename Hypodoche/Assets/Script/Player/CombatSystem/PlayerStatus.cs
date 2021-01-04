﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Hypodoche
{
    public class PlayerStatus : MonoBehaviour
    {
        #region Variables
        public float _playerHealth;
        private float _playerStamina;
        [SerializeField] private float _staminaRegenRate = 15f;
        [SerializeField] private AnimatorHandler _animatorHandler;
        private float _staminaRegenDelay = 0.5f;
        public float _maxHealth = 100f;
        private float _maxStamina = 100f;
        private float _staminaRegenStartTime = 0f;
        private float _exhaustionPoint = 30f;
        private bool _isExhausted = false;

        private bool _isStunned = false;

        private float _stunTime;

        private float _startStunClock;

        [SerializeField] private Image _stamina;
        [SerializeField] private Image _health;
        private Collider _collider;
        private List<Effects> _status = new List<Effects>();
        [SerializeField] private UI_AppearStatusIcon _icons;
        #endregion

        #region Methods

        private void Start()
        {
            _playerHealth = _maxHealth;
            _playerStamina = _maxStamina;
            _health.fillAmount = 1f;
            _stamina.fillAmount = 1f;
            _collider = GetComponent<Collider>();
        }

        private void Update()
        {
            if(_isStunned){
               
                if(Time.time >= _startStunClock + _stunTime)
                    _isStunned = false;
            }
            if(_status.Count > 0){
                foreach(Effects e in _status){
                    if(!e._stun.isEmpty){
                        _isStunned = true;
                        _stunTime = e._stun.time;
                        _startStunClock = Time.time;
                    }
                }
                _status.Clear();
            }
            if (_playerStamina < _maxStamina && Time.time > _staminaRegenStartTime)
            {
                _playerStamina += _staminaRegenRate * Time.deltaTime;
                if (_playerStamina > _exhaustionPoint)
                    _isExhausted = false;
                if (_playerStamina > _maxStamina)
                    _playerStamina = _maxStamina;
            }

            UpdateStaminaUIValue();
        }

        #region Stamina

        public bool HasStamina(float cost)
        {
            return (_playerStamina > cost && !_isExhausted);
        }

        public void SpendStamina(float cost)
        {
            _playerStamina -= cost;

            if (_playerStamina < 1f)
                _isExhausted = true;
            
            if (_playerStamina < _exhaustionPoint)
                _staminaRegenStartTime = Time.time + 3 * _staminaRegenDelay;
            else
                _staminaRegenStartTime = Time.time + _staminaRegenDelay;

        }

        private void UpdateStaminaUIValue()
        {
            _stamina.fillAmount = _playerStamina / _maxStamina;
        }

        #endregion

        #region Health

        public void TakeDamage(float damage)
        {
            _playerHealth -= damage;

            UpdateHealthUIValue();

            if (_playerHealth <= 0)
            {
                _animatorHandler.UpdateParameter("isDead", true);
                _health.color = Color.black;
                _stamina.color = Color.black;
                _collider.enabled = false;
            }
            _animatorHandler.ActivateTargetTrigger("isDamaged");
        }

        public void UpdateHealthUIValue()
        {
            _health.fillAmount = _playerHealth / _maxHealth;
        }
        #endregion

        #region Statuses
        public void AddStatus(Effects effect)
        {
            _status.Add(effect);
        }

        public void RemoveStatus(Effects effect)
        {
            _status.Remove(effect);
        }
        #endregion

        #region getter
        
        public List<Effects> getDebuffList(){
            return _status;
        }

        public float  getStunTime(){
            return _stunTime;
        }

        public bool isStunned(){
            return _isStunned;
        }

        public float getStartStunClock(){
            return _startStunClock;
        }

        #endregion
        #endregion
    }
}
