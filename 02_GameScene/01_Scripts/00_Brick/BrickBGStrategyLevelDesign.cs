using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperVeigar
{
    public class BrickBGStrategyLevelDesign : BrickBGStrategy
    {
        public override void Init(BrickBG brickBG)
        {
            this.brickBG = brickBG;
        }

        public override void EventOnLeftClick(int index)
        {
            LevelDesignService.Instance.AddBrickInfo(index);
        }

        public override void EventOnRightClick(int index)
        {
            LevelDesignService.Instance.RemoveBrickInfo(index);
        }

        public override bool GetColliderEnable(BrickType brickType)
        {
            return true;
        }
    }
}