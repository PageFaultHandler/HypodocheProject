﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Hypodoche
{
    public class Entity : MonoBehaviour
    {
        #region Variables
        public Rigidbody _rigidBodyBoss { get; private set; }
        public Animator _animator { get; private set; }
        public GameObject _boss { get; private set; }
        public FiniteStateMachine _stateMachine;
        private Vector3 _direction;
        public D_Entity _entityData;
        private bool _flipped = true;
        [SerializeField] public GameObject _ui;
        [SerializeField] public bool _minion;
        #endregion

        #region Getters and Setters
        public void SetIsFlipped(bool flip)
        {
            _flipped = flip;
        }
        #endregion

        #region

        public void Awake(){
            resetStates();
            resetValues();
        }

        public virtual void Start()
        { 
            _boss = gameObject;
            _rigidBodyBoss = _boss.GetComponent<Rigidbody>();
            _animator = _boss.transform.GetComponentInChildren<Animator>();
            _stateMachine = new FiniteStateMachine();
        }

        public virtual void Update()
        {
            if (Time.timeScale == 0f)
                return;
            //Debug.Log("i am: "+ this);
            _stateMachine._currentState.Update();
        }



        public void resetValues()
        {
            _entityData.slowOverArea = false;
            _entityData.damageOverArea = false;
            _entityData.enhanceMultiplier = 0f;
            _entityData.damageTakenOverTime = 0f;
            _entityData.health = 1000f;
        }

        public virtual void setDirection()
        {
            _direction = new Vector3(UnityEngine.Random.Range(-1.0f,1.0f),0,UnityEngine.Random.Range(-1.0f, 1.0f));    
        }


        public virtual void setDirection(Vector3 dir){
            _direction = dir;
        }

        /*public virtual void FixedUpdate()
        {
            _rigidBodyBoss.velocity = Vector3.zero;
        }*/
        
        public virtual void Move(float speed)
        {
            _rigidBodyBoss.MovePosition(_rigidBodyBoss.position + _direction * speed * Time.deltaTime);
        }

        //mi giro di 180 gradi dal lato opposto. però sull'asse delle ascisse. Non so come gestire le ordinate, sarebbe visivamente brutto
        public virtual void Flip()
        {
            Vector3 flippedRotation = new Vector3(-45,180,0);
            Vector3 rotation = new Vector3(45,0,0);
            Transform child =  _boss.transform.GetChild(0).transform;
            if(!_flipped)
                child.rotation = Quaternion.Euler(flippedRotation);
            else
                child.rotation = Quaternion.Euler(rotation);
            
            _flipped = !_flipped;
        }

        public virtual bool checkWall()
        {

            Debug.DrawRay(_boss.transform.position, _direction * _entityData.wallCheckRange, Color.yellow);
            return Physics.Raycast(_boss.transform.position, _direction, _entityData.wallCheckRange, _entityData.whatIsPerimeter);
        }


        public virtual Transform isPlayerInAggroRange()
        {
            Collider[] player = Physics.OverlapSphere(_boss.transform.position, _entityData.aggroRange, _entityData.whatIsPlayer);
            if (player.Length == 0)
                return null;
            else return player[0].transform;
        }



        // non ho la minima idea di come farlo vedere in gizmos, ma dovrebbe essere giusto.
        public virtual bool hittable(float viewAngle, float radius, float from, float to, LayerMask targetMask) //radius < viewRadius
        {
            Debug.Log("Checking attack range...");
            radius = radius <= _entityData.aggroRange ? radius : _entityData.aggroRange;
            Collider[] targetsInViewRadius = Physics.OverlapSphere(_boss.transform.position, radius, targetMask);
            if (targetsInViewRadius.Length == 0)
                return false;
            Transform target = targetsInViewRadius[0].transform;
            Vector3 dirToTarget = (target.position - _boss.transform.position).normalized;
            if (Vector3.Angle(_boss.transform.forward * from, dirToTarget) < viewAngle)
            {
                float dstToTarget = Vector3.Distance(_boss.transform.position, target.position);
                if (dstToTarget <= to)
                    return true;
            }
            return false;
        }

        public Vector3 getDirection(){
            return _direction;
        }
/*
        public virtual void OnDrawGizmos() { 
            if (!Application.isPlaying)
                return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_boss.GetComponent<Collider>().bounds.center, _entityData.aggroRange);
        }*/


        public void resetStates()
        {
            _entityData.isStun = false;
            _entityData.timeOfStun = 0f;
            _entityData.isSlowed = false;
            _entityData.timeOfSlow = 0f;
            _entityData.gotDamageOverTime = false;
            _entityData.timeOfDamage = 0f;
        }

       
        #endregion
    }
}
