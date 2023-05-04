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
        public static Action onGameLoaded;
        public static Action onGameStarted;
        public static Action onGameCompleted;
        public static Action onGameFailed;
        public static Action onGameReset;
   
        [HorizontalLine()]
        [Header("Input Events")] 
        public static Action<IClickable> onTileClicked;
        public static Action onMatchFound;
        public static Action<int> onBoardCreated;
        public static Action<int> onRegenerateButtonClicked;
        

    }
}
    

