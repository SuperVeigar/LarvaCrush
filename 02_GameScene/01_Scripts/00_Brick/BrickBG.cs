using System;
using UnityEngine;
using UniRx;

namespace SuperVeigar
{
    public enum BrickType
    {
        Red,
        Orange,
        Yellow,
        LightGreen,
        Green,
        SkyBlue,
        Blue,
        Indigo,
        Violet,
        Black,
        End
    }
    
    public class BrickBG : MonoBehaviour
    {
        private BrickBGStrategy strategy;
        public LevelDesignInfo info;
        private SpriteRenderer spriteRenderer;
        [SerializeField] PolygonCollider2D collier;
        
        public void Init(int index, BrickBGStrategy strategy)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            info = new LevelDesignInfo();

            info.index = index;
            this.strategy = strategy;

            Reset();
        }

        public void Reset()
        {
            SetType(BrickType.Black);
        }

        public void SetType(BrickType type)
        {
            info.type = type;
            spriteRenderer.sprite = LevelDesignService.Instance.brickSprites[(int)type];
            collier.enabled = strategy.GetColliderEnable(type); 
        }

        public void Touch()
        {
            BrickType nextType = info.type + 1;

            if (nextType == BrickType.Black)
            {
                Break();
            }
            
            if (nextType != BrickType.End)
            {
                SetType(nextType);
            }
        }

        private void Break()
        {
            SoundService.Instance.PlaySound(SoundReferenceType.BRICK_BREAK);
            if (UnityEngine.Random.Range(0, 4) == 0)
            {
                SpawnItem();
            }
        }

        private void SpawnItem()
        {
            Item item = ItemService.Instance.GetAvailableItem(ItemType.Tail);

            item?.Init(transform.position);
        }

#region Level design

        public void EventOnLeftClick()
        {
            strategy?.EventOnLeftClick(info.index);
            SetType(LevelDesignService.Instance.selectedBrickType);
        }

        public void EventOnRightClick()
        {
            strategy?.EventOnRightClick(info.index);
            SetType(BrickType.Black);
        }

#endregion

    }
}

