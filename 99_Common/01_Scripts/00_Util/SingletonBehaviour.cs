using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace SuperVeigar
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                }
                return instance;
            }
        }
    }
}

