﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hypodoche
{
    public class DisplayInformation : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TrapSelectionManager _trapSelectionManager;
        [SerializeField] private ShopManager _shopManager;
        private TrapItem _selected;
        [SerializeField] private Text _trapName;
        [SerializeField] private Image _trapImage;
        [SerializeField] private Text _trapDescription;
        [SerializeField] private Image _effectOneImage;
        [SerializeField] private Image _effectTwoImage;
        [SerializeField] private Text _effectOneDescription;
        [SerializeField] private Text _effectTwoDescription;

        private int _effectNum;
        private bool _isShopOpen = false;
        #endregion

        #region Getters and Setters
        public void SetIsShopOpen(bool isOpen)
        {
            _isShopOpen = isOpen;
        }
        #endregion

        #region Methods
        private void Update()
        {
            if(_isShopOpen){
                _selected = _shopManager.GetSelectedItem();
            }
            else{
                _selected = _trapSelectionManager.GetSelectedItem();
            }
            if(_selected != null)
            {
                _trapName.text = _selected.GetItemName();
                _trapImage.sprite = _selected.GetItemSprite();
                _trapDescription.text = _selected.GetDescription();
                _effectNum = _selected.GetEffectNum();
                if(_effectNum == 1)
                    DisplayOneEffect();
                else if(_effectNum == 2)
                    DisplayTwoEffects();
            }
        }

        private void DisplayOneEffect()
        {
            _effectOneDescription.enabled = true;
            _effectOneImage.enabled = true;
            _effectOneDescription.text = _selected.GetEffectOneDescription();
            _effectOneImage.sprite = _selected.GetEffectOneSprite();
            _effectTwoDescription.enabled = false;
            _effectTwoImage.enabled = false;
        }

        private void DisplayTwoEffects()
        {
            DisplayOneEffect();
            _effectTwoDescription.enabled = true;
            _effectTwoImage.enabled = true;
            _effectTwoDescription.text = _selected.GetEffectTwoDescription();
            _effectTwoImage.sprite = _selected.GetEffectTwoSprite();
        }
        #endregion
    }
}