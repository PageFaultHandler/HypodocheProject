﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hypodoche
{
    [CreateAssetMenu()]
    public class CampaignProgression : ScriptableObject
    {
        #region Variables
        private static Queue<string> _bosses;
        private static string _bossName;
        private static string _bossSubtitle;
        private static string _bossDescription;
        private static string[] _bossTrapSuggestions;
        private static string _bossSuggestions;
        private static string _bossLore;
        private static Sprite _bossSprite;
        #endregion

        #region  Getters and Setters
        public string GetBossName()
        {
            return _bossName;
        }

        public string GetBossSubtitle()
        {
            return _bossSubtitle;
        }

        public string GetBossDescription()
        {
            return _bossDescription;
        }

        public string[] GetBossTrapSuggestions()
        {
            return _bossTrapSuggestions;
        }

        public string GetBossSubgestions()
        {
            return _bossSuggestions;
        }

        public string GetBossLore()
        {
            return _bossLore;
        }

        public Sprite GetBossSprite()
        {
            return _bossSprite;
        }

        public void SetBossName(string s)
        {
            _bossName = s;
        }

        public void SetBossSubtitle(string s)
        {
            _bossSubtitle = s;
        }

        public void SetBossDescription(string s)
        {
            _bossDescription = s;
        }

        public void SetBossTrapSuggestions(string s, int index)
        {
            _bossTrapSuggestions[index] = s;
        }

        public void SetBossSuggestions(string s)
        {
            _bossSuggestions = s;
        }

        public void SetBossLore(string s)
        {
            _bossLore = s;
        }

        public void SetBossSprite(Sprite s)
        {
            _bossSprite = s;
        }
        #endregion

        #region Methods
        public void LoadBossList()
        {
            _bossTrapSuggestions = new string[3];
            string halja = "Halja";
            string liYian = "Li Yan";
            string caput = "Caputmallei";
            List<string> shuffle = new List<string>();
            //shuffle.Add(halja);
            shuffle.Add(liYian);
            //shuffle.Add(caput);
            for (int i = 0; i < shuffle.Count; i++) {
                string temp = shuffle[i];
                int randomIndex = Random.Range(i, shuffle.Count);
                shuffle[i] = shuffle[randomIndex];
                shuffle[randomIndex] = temp;
            }
            _bosses = new Queue<string>();
            foreach(string s in shuffle)
            {
                _bosses.Enqueue(s);
            }
        }

        public string ExtractBoss()
        {
            if(_bosses.Count != 0)
                return _bosses.Dequeue();
            else
                return null;
        }
        #endregion
    }
}
