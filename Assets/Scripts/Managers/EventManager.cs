using System;
using Gameplay;
using Gameplay.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace Managers
{
    public static class EventManager
    {
        [Header("Dependencies")] 
        [Header("Game Events")] 
        public static Action OnGameLoaded;
        public static Action OnGameStarted;
        public static Action OnGameCompleted;
        public static Action OnGameFailed;
        public static Action OnGameReset;
   
        [HorizontalLine()]
        [Header("Input Events")] 
        public static Action<IClickable> OnTileClicked;
        public static Action OnMatchFound;
        public static Action<int> OnBoardCreated;
        public static Action<int> OnRegenerateButtonClicked;
        

    }
}
    

