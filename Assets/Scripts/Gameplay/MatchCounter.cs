using System;
using Managers;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class MatchCounter : MonoBehaviour
    {
        private int _currentMatchCounter; 
        [SerializeField]private TextMeshProUGUI matchCountTMP;

        private void Awake()
        {
            BoardCreated();
        }

        private void OnEnable()
        {
            EventManager.OnMatchFound += MatchFound;
        }
        
        private void OnDisable()
        {
            EventManager.OnMatchFound -= MatchFound;
        }
        
        private void BoardCreated()
        {
            _currentMatchCounter = 0;
            SetMatchCounterTMP();
        }

        private void MatchFound()
        {
            _currentMatchCounter++;
            SetMatchCounterTMP();
        }


        private void SetMatchCounterTMP()
        {
            matchCountTMP.text = "Match Count : " + _currentMatchCounter.ToString();
        }

    }
}
