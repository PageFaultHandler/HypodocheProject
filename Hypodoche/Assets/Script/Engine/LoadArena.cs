﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hypodoche
{
    public class LoadArena : MonoBehaviour
    {
        #region Variables
        [SerializeField] private ArenaTransferSO _arenaTransfer;
        [SerializeField] private Transform _basePoint;
        [SerializeField] private float _horizontalOffset;
        [SerializeField] private float _verticalOffset;
        [SerializeField] private List<GameObject> _bossList;
        [SerializeField] private Transform _spawnBossPosition;
        [SerializeField] private GameObject _debuffArea;
        [SerializeField] private Image _healthFill;
        [SerializeField] private Text _nameText;
        [SerializeField] private CampaignProgression _cp;

        [SerializeField] private Image _firstMinionFill;
        [SerializeField] private Image _secondMinionFill;

        [SerializeField] private GameObject _firstMinionCanvas;
        [SerializeField] private GameObject _secondMinionCanvas;

        [SerializeField] private IceCrow _firstMinion;
        [SerializeField] private WaterCrow _secondMinion;

        [SerializeField] private AudioClip _HaljaMusic;
        [SerializeField] private AudioClip _CaputMalleiMusic;

        [SerializeField] private AudioClip _LiyanMusic;

        [SerializeField] private AudioSource _as;








        #endregion

        #region Methods
        void Awake()
        {
            SpawnBoss();
            int i = 0, j = 0;
            if(_arenaTransfer != null && _arenaTransfer.GetSlotArray() != null)
            {
                foreach (GameObject prefab in _arenaTransfer.GetSlotArray()){       
                    if (prefab != null){
                        Instantiate(prefab, new Vector3(_basePoint.position.x + i * _horizontalOffset, 0, _basePoint.position.z - j * _verticalOffset), Quaternion.identity);
                    }
                    i++;
                    if(i == 5) {
                        i = 0;
                        j++;
                    }
                }
            }
        }

        private void SpawnBoss()
        {
            GameObject boss;
            switch(_cp.GetBossName()){
                case "Halja":
                    boss = _bossList[0];
                    _nameText.text = "Halja";
                    _as.clip = _HaljaMusic;
                    boss.GetComponent<Halja>().GetIceCrowGO().GetComponent<Enemy>().SetFill(_firstMinionFill);
                    boss.GetComponent<Halja>().GetWaterCrowGO().GetComponent<Enemy>().SetFill(_secondMinionFill);
                    boss.GetComponent<Halja>().GetIceCrowGO().GetComponent<IceCrow>().setCanvas(_firstMinionCanvas);
                    boss.GetComponent<Halja>().GetWaterCrowGO().GetComponent<WaterCrow>().setCanvas(_secondMinionCanvas);
                    break;
                case "Li Yan":
                    _as.clip = _LiyanMusic;
                    boss = _bossList[1];
                    _nameText.text = "Li Yan";
                    break;
                case "Caputmallei": default:
                    _as.clip = _CaputMalleiMusic;
                    boss = _bossList[2];
                    _nameText.text = "Caputmallei";
                    break;
            }

            //BOSS SETUP
            boss.GetComponent<Entity>()._ui = _debuffArea;
            boss.GetComponent<Enemy>().SetFill(_healthFill);
            _as.Play();
            Instantiate(boss, _spawnBossPosition.position, Quaternion.identity);
        }
        #endregion
    }
}
    
