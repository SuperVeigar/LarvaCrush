using UnityEngine;

namespace SuperVeigar
{
    public abstract class BrickBGStrategy
    {
        protected BrickBG brickBG;

        public abstract void Init(BrickBG brickBG);
        public abstract void EventOnLeftClick(int index);
        public abstract void EventOnRightClick(int index);
        public abstract bool GetColliderEnable(BrickType brickType);
    }
}

