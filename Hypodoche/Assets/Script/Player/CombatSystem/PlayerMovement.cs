﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hypodoche
{
    #region Required Components
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerCombat))]
    [RequireComponent(typeof(PlayerStatus))]
    #endregion

    public class PlayerMovement : MonoBehaviour
    {
        #region Variables
        [SerializeField] [Range(1f, 10f)] private float _playerSpeed = 1f;
        [SerializeField] [Range(1.5f, 50f)] private float _dashMultiplier = 5f;
        [SerializeField] [Range(1.5f, 50f)] private float _backstepMultiplier = 1.5f;
        [SerializeField] [Range(1.5f, 10f)] private float _sprintMultiplier = 10f;
        [SerializeField] private GameObject _sprite;
        [SerializeField] private LayerMask _mouseMask;

        private Rigidbody _rigidbody;
        private PlayerCombat _playerCombat;

        private PlayerStatus _playerStatus;
        private Vector3 _movement;
        private Vector3 _backMovement = Vector3.right;
        private bool _facesLeft = true;
        private bool _isDashing = false;
        private bool _isBackstepping = false;
        private bool _isSprinting = false;
        private float _dashRate = 2f;
        private float _nextDashTime = 0f;
        #endregion

        #region Getter and Setter

        public float GetNextDashTime()
        {
            return _nextDashTime;
        }

        public bool GetIsDashing()
        {
            return _isDashing;
        }

        public void SetIsDashing(bool isDashing)
        {
            _isDashing = isDashing;
        }

        public bool GetIsBackstepping()
        {
            return _isBackstepping;
        }

        public void SetIsBackstepping(bool isBackstepping)
        {
            _isBackstepping = isBackstepping;
        }

        public void SetIsSprinting(bool isSprinting)
        {
            _isSprinting = isSprinting;
        }

        public void SetMovement(Vector3 movement)
        {
            _movement = movement;
            if (movement.magnitude > 0)
                _backMovement = movement * -1f;
        }
        #endregion

        #region Methods
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerCombat = GetComponent<PlayerCombat>();
            _playerStatus = GetComponent<PlayerStatus>();
        }

        private void FixedUpdate()
        {
            //
            if(_playerStatus.isStunned())
                return;
                
            if (_isDashing)
            {
                UpdateSpriteDirection(_movement);
                UpdatePosition(_movement * _playerSpeed * _dashMultiplier, Time.fixedDeltaTime);
            }
            else if (_isBackstepping)
                UpdatePosition(_backMovement * _playerSpeed * _backstepMultiplier, Time.fixedDeltaTime);
            else if (_isSprinting)
            {
                UpdateSpriteDirection(_movement);
                UpdatePosition(_movement * _playerSpeed * _sprintMultiplier, Time.fixedDeltaTime);
            }
            else
            {
                UpdateSpriteDirection(_movement);
                UpdatePosition(_movement * _playerSpeed, Time.fixedDeltaTime);
            }
        }

        private void UpdatePosition(Vector3 movement, float delta)
        { 
            if (movement.magnitude != 0)
                _rigidbody.velocity = Vector3.zero; 
            _rigidbody.MovePosition(_rigidbody.position + movement * delta);
        }

        public void UpdateDashTime()
        {
            _nextDashTime = Time.time + 1f / _dashRate;
        }

        public void FollowMousePointer()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, _mouseMask))
            {
                Debug.Log("Hit: " + hit.point);
                if (hit.point.x > transform.position.x && _facesLeft ||
                    hit.point.x < transform.position.x && !_facesLeft)
                    Flip();
            }
        }

        private void UpdateSpriteDirection(Vector3 movement)
        {
            if (movement.x > 0 && _facesLeft ||
                movement.x < 0 && !_facesLeft)
                Flip();
        }

        private void Flip()
        {
            if(_facesLeft){
                _sprite.transform.rotation = Quaternion.Euler(-30,180,0);
            }
            else{
                _sprite.transform.rotation = Quaternion.Euler(30,0,0);     
            }
            _facesLeft = !_facesLeft;
        }
        #endregion
    }
}
