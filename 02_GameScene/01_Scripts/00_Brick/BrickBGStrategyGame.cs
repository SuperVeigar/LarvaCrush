namespace SuperVeigar
{
    public class BrickBGStrategyGame : BrickBGStrategy
    {
        
        public override void Init(BrickBG brickBG)
        {
            
        }

        public override void EventOnLeftClick(int index)
        {

        }

        public override void EventOnRightClick(int index)
        {
            
        }

        public override bool GetColliderEnable(BrickType brickType)
        {
            return brickType != BrickType.Black;
        }
    }
}

