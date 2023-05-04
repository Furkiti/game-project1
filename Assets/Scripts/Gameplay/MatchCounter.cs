using Managers;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class MatchCounter : MonoBehaviour
    {
        private int _currentMatchCounter; 
        [SerializeField]private TextMeshProUGUI matchCountTMP;
        private void OnEnable()
        {
            EventManager.onBoardCreated += BoardCreated;
            EventManager.onMatchFound += MatchFound;
        }
        
        private void OnDisable()
        {
            EventManager.onBoardCreated -= BoardCreated;
            EventManager.onMatchFound -= MatchFound;
        }
        
        private void BoardCreated(int newBoardSize)
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
