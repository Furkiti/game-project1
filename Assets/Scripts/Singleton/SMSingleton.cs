﻿using Abstract;
using UnityEngine;

namespace Singleton
{
    public class SMSingleton<T> : StateMachine
        where T : Component
    {
        private static T _instance;
        public static T Instance {
            get {
                if (_instance == null) {
                    var objs = FindObjectsOfType (typeof(T)) as T[];
                    if (objs.Length > 0)
                        _instance = objs[0];
                    if (objs.Length > 1) {
                        Debug.LogError ("There is more than one " + typeof(T).Name + " in the scene.");
                    }
                    if (_instance == null) {
                        GameObject obj = new GameObject ();
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        _instance = obj.AddComponent<T> ();
                    }
                }
                return _instance;
            }
        }
    }


    public class SMSingletonPersistent<T> : StateMachine
        where T : Component
    {
        public static T Instance { get; private set; }
	
        public virtual void Awake ()
        {
            if (Instance == null) {
                Instance = this as T;
                DontDestroyOnLoad (this);
            } else {
                Destroy (gameObject);
            }
        }
    }
}
