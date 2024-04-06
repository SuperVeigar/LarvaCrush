using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UniRx;

namespace SuperVeigar
{
    [Serializable]
    public class LevelDesignInfo
    {
        public int index;
        public BrickType type;
    }

    public enum LevelDesignType
    {
        Game,
        Customization,
    }

    public class LevelDesignService : SingletonBehaviour<LevelDesignService>
    {
        [Header("UI")]
        public BrickType selectedBrickType = BrickType.Red;
        [SerializeField] private GameObject selectMark;
        [SerializeField] private InputField stageInputfield;
        [SerializeField] private Button save;
        [SerializeField] private Button load;
        [SerializeField] private Button reset;

        // level design info
        private int stage;
        private List<LevelDesignInfo> levelDesignInfoList;
        private const int row = 27;
        private const int col = 9;     // 홀수로 설정할 것
        private const float distanceColumn = 1.4f;
        private const float distanceRow = 0.8f;

        [Header("BrickBG")]
        [SerializeField] private GameObject brickBGPrefab;
        [SerializeField] private Transform brickBGParent;
        [SerializeField] private List<BrickBG> brickBGs;
        public List<Sprite> brickSprites;

        private LevelDesignLoader loader;
        
        [SerializeField] LevelDesignType levelDesignType;

        private void Awake()
        {
            levelDesignInfoList = new List<LevelDesignInfo>();
            loader = new LevelDesignLoader();

            BindView();
            CraeteBrickBGs();
        }

        private void BindView()
        {
            stageInputfield?.OnValueChangedAsObservable().Subscribe(value =>
            {
                if (string.IsNullOrEmpty(value) == false)
                {
                    stage = Convert.ToInt32(value);
                }
            }).AddTo(this);

            save?.OnClickAsObservable().Subscribe(_ =>
            {
                SaveFile();
            }).AddTo(this);

            load?.OnClickAsObservable().Subscribe(_ =>
            {
                LoadFile();
            }).AddTo(this);

            reset?.OnClickAsObservable().Subscribe(_ =>
            {
                ResetBricks();
            }).AddTo(this);
        }

#region Bricks

        private void ResetBricks()
        {
            levelDesignInfoList.Clear();

            for (int i = 0; i < brickBGs.Count; i++)
            {
                brickBGs[i].Reset();
            }
        }

        private void CraeteBrickBGs()
        {
            int index = 0;

            // main row
            float startPositionX = (float)((col - 1) / 2) * distanceColumn * -1f;
            float startPositionY = brickBGParent.position.y;
            for (int i = 0; i < row; i++)
            {
                for (int k = 0; k < col; k++)
                {
                    float positionX = startPositionX + distanceColumn * k;
                    float positionY = startPositionY - distanceRow * i;
                    Vector3 position = new Vector3(positionX, positionY, 0f);

                    InstantiateBrick(position, index++);
                }
            }

            // sub row
            startPositionX = (float)(col / 2) * distanceColumn * -1f - (float)distanceColumn * 0.5f;
            startPositionY = brickBGParent.position.y + (float)distanceRow * 0.5f;
            for (int i = 0; i < row; i++)
            {
                for (int k = 0; k < col + 1; k++)
                {
                    float positionX = startPositionX + distanceColumn * k;
                    float positionY = startPositionY - distanceRow * i;
                    Vector3 position = new Vector3(positionX, positionY, 0f);

                    InstantiateBrick(position, index++);
                }
            }
        }

        private void InstantiateBrick(Vector3 position, int index)
        {
            if (brickBGs == null)
            {
                brickBGs = new List<BrickBG>();
            }

            BrickBG brick = Instantiate(brickBGPrefab, position, Quaternion.identity, brickBGParent).GetComponent<BrickBG>();

            brick.Init(index, GetBrickStrategy());

            brickBGs.Add(brick);
        }

        private BrickBGStrategy GetBrickStrategy()
        {
            return levelDesignType == LevelDesignType.Game ? new BrickBGStrategyGame() : new BrickBGStrategyLevelDesign();
        }

        public void AddBrickInfo(int index)
        {
            for (int i = 0; i < levelDesignInfoList.Count; i++)
            {
                if (levelDesignInfoList[i].index == index)
                {
                    LevelDesignInfo info = levelDesignInfoList[i];
                    info.type = selectedBrickType;
                    return;
                }
            }

            LevelDesignInfo newInfo = new LevelDesignInfo();
            newInfo.index = index;
            newInfo.type = selectedBrickType;
            levelDesignInfoList.Add(newInfo);
        }

        public void RemoveBrickInfo(int index)
        {
            for (int i = 0; i < levelDesignInfoList.Count; i++)
            {
                if (levelDesignInfoList[i].index == index)
                {
                    levelDesignInfoList.Remove(levelDesignInfoList[i]);
                    return;
                }
            }
        }

#endregion

#region Save

        private void SaveFile()
        {
            if (IsNullOrEmptyInputfield() == true)
            {
                PopupService.Instance.CreateAlertPopupWithCenterButton("스테이지를 입력하세요.");
            }
            else
            {
                loader.Save(stage, levelDesignInfoList,
                    OnComplete: () => 
                    {
                        PopupService.Instance.CreateAlertPopupWithCenterButton("저장 완료 !");
                    });
            }
        }

#endregion

#region Load

        private void LoadFile()
        {
            if (IsNullOrEmptyInputfield() == true)
            {
                PopupService.Instance.CreateAlertPopupWithCenterButton("스테이지를 입력하세요.");
            }
            else
            {
                List<LevelDesignInfo> tempLevelDesignInfoList = new List<LevelDesignInfo>();

                loader.Load(stage, ref tempLevelDesignInfoList,
                OnComplete: () => 
                {
                    SetLoadedBricks(ref tempLevelDesignInfoList);
                    PopupService.Instance.CreateAlertPopupWithCenterButton("불러오기 완료 !");
                });
            }
        }

        private void SetLoadedBricks(ref List<LevelDesignInfo> tempLevelDesignInfoList)
        {
            if (tempLevelDesignInfoList != null && tempLevelDesignInfoList.Count > 0)
            {
                ResetBricks();

                levelDesignInfoList = tempLevelDesignInfoList;

                for (int i = 0; i < levelDesignInfoList.Count; i++)
                {
                    for (int k = 0; k < brickBGs.Count; k++)
                    {
                        if (brickBGs[k].info.index == levelDesignInfoList[i].index)
                        {
                            brickBGs[k].SetType(levelDesignInfoList[i].type);
                            break;
                        }
                    }
                }
            }
        }

        public void LoadStage(int stage)
        {
            List<LevelDesignInfo> tempLevelDesignInfoList = new List<LevelDesignInfo>();

            loader.Load(stage, ref tempLevelDesignInfoList,
            OnComplete: () => 
            {
                SetLoadedBricks(ref tempLevelDesignInfoList);
            });
        }

#endregion

        public void SelectBrickType(BrickType type, Vector2 position)
        {
            selectedBrickType = type;
            selectMark.transform.position = position;
            selectMark.gameObject.SetActive(true);
        }

        private bool IsNullOrEmptyInputfield()
        {
            return string.IsNullOrEmpty(stageInputfield.text);
        }
    }
}

