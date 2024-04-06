using UnityEngine;

namespace SuperVeigar
{
    public class ItemTail : Item
    {
        protected override void TakeItem()
        {
            GameEventService.Instance.EventOnAddTail();
        }
    }
}