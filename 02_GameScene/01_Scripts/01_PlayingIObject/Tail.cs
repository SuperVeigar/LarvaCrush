using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System;

namespace SuperVeigar
{
    public class TailData
    {
        public Vector3 position;
        public Action action;

        public TailData(Vector3 position, Action action = null)
        {
            this.position = position;
            this.action = action;
        }
    }

    public class Tail : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private Tail tail;
        private bool isStart = false;
        private Queue<TailData> tailData;
        private TailData addedData;

        public void Init(Vector3 position, int orderInLayer)
        {
            transform.position = position;
            spriteRenderer.sortingOrder = orderInLayer;

            tailData = new Queue<TailData>();

            StartMove().Forget();
        }

        public void AddTail()
        {
            if (tail == null)
            {
                tail = ObjectPoolService.Instance.GetTail();
                tail.Init(transform.position, spriteRenderer.sortingOrder - 1);
            }
            else
            {
                tail.AddTail();
            }
        }

        public void Reset()
        {
            isStart = false;
            tail = null;
            tailData.Clear();
        }

        public void UpdateTail(Vector3 position)
        {
            SetNextData(position);
            ExcuteCurrentData();
        }

        private void SetNextData(Vector3 position)
        {
            addedData = new TailData(position);
            tailData.Enqueue(addedData);
            tail?.UpdateTail(transform.position);
        }

        private void ExcuteCurrentData()
        {
            if (isStart == true)
            {
                TailData data = tailData.Dequeue();
                transform.position = data.position;

                if (data.action != null)
                {
                    data.action.Invoke();
                    tail?.AddAction(data.action);
                }
            }
        }

        private async UniTaskVoid StartMove()
        {
            await UniTask.Delay(Logic.MillisecondsFromSeconds(Define.Const.TAIL_START_DELAY));

            isStart = true;
        }

        public void AddAction(Action action)
        {
            if (addedData != null)
            {
                addedData.action += action;
            }
        }
    }
}