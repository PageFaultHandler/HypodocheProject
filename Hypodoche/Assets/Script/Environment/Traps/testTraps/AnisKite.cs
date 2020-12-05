﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Hypodoche
{
    public class AnisKite : MonoBehaviour, Traps
    {
        #region variables

        Effects myEffect;
        [SerializeField] public float speed;
        private Vector3 direction;

        #endregion

        #region methods

        public AnisKite()
        {
        }

        public void Start()
        {
            StunData s = new StunData();
            s.isEmpty = true;
            DamageOverTimeData d = new DamageOverTimeData();
            d.isEmpty = true;
            SlowData sl = new SlowData();
            sl.isEmpty = true;
            DamageData dm = new DamageData(); // deals dmg 
            dm.isEmpty = false;
            dm.damage = 50;
            FearData sc = new FearData();
            sc.isEmpty = true;
            SlowOverAreaData sla = new SlowOverAreaData();
            sla.isEmpty = true;
            DamageOverAreaData dma = new DamageOverAreaData();
            dma.isEmpty = true;
            EnhanceData en = new EnhanceData();
            en.isEmpty = true;
            direction = new Vector3(0, 0, 0);
            myEffect = new Effects(sl, s, d, dm, sc, false, sla, dma, en);
            //Simplified AI
            InvokeRepeating("ChangeDirection", 0f, 3f);

        }

        public void ChangeDirection()
        {
            float rx = UnityEngine.Random.Range(-1, 2);
            float rz = UnityEngine.Random.Range(-1, 2);
            direction = new Vector3(rx, 0, rz);
        }

        public void OnTriggerEnter()
        {
            direction = -1 * direction;
            transform.Rotate(0, 180f, 0, Space.Self);
        }

        public void Update()
        {
            transform.position = transform.position + direction * speed;
        }

        public string SendDataTrap()
        {
            //Destroy(gameObject); should not be destroyed 
            return JsonUtility.ToJson(myEffect, true);
        }

        #endregion
    }
}
