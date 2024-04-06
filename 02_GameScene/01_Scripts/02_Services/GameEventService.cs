using System;

namespace SuperVeigar
{
    public class GameEventService : SingletonBehaviour<GameEventService>
    {
        public static Action OnAddTail;

        public void EventOnAddTail()
        {
            OnAddTail?.Invoke();
        }
    }
}