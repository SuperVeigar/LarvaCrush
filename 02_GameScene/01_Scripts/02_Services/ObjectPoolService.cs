using System.Collections.Generic;
using UnityEngine;

namespace SuperVeigar
{
    public class ObjectPoolService : SingletonBehaviour<ObjectPoolService>
    {
        [Header("Tail")]
        [SerializeField] private List<Tail> tails = new List<Tail>();
        [SerializeField] private Tail prefab;
        [SerializeField] private Transform tailParent;

        public void Reset()
        {
            ResetTails();
        }

        private void ResetTails()
        {
            for (int i = 0; i < tails.Count; i++)
            {
                tails[i].Reset();
                tails[i].gameObject.SetActive(false);
            }
        }

        public Tail GetTail()
        {
            for (int i = 0; i < tails.Count; i++)
            {
                if (tails[i].gameObject.activeInHierarchy == false)
                {
                    tails[i].gameObject.SetActive(true);
                    return tails[i];
                }
            }

            Tail tail = Instantiate(prefab, tailParent);
            tails.Add(tail);

            return tail;
        }
    }
}

