using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

namespace SuperVeigar
{
    [Serializable]
    public class LevelDesignData
    {
        public List<LevelDesignInfo> list;
    }

    public class LevelDesignLoader
    {
        public void Save(int stage, List<LevelDesignInfo> bricks, Action OnComplete)
        {
            string filePath = GetFilePath(stage);

            LevelDesignData data = new LevelDesignData();
            data.list = bricks;

            string jsonData = JsonUtility.ToJson(data);

            File.WriteAllText(filePath, jsonData);

            OnComplete?.Invoke();
        }

        public void Load(int stage, ref List<LevelDesignInfo> list, Action OnComplete)
        {
            TextAsset textAsset = Resources.Load<TextAsset>($"StageData{stage}");

            LevelDesignData data = null;

            if (textAsset != null)
            {
                data = JsonUtility.FromJson<LevelDesignData>(textAsset.text);

                list = data.list;
            }

            OnComplete?.Invoke();
        }
        
        private string GetFilePath(int stage)
        {
            return Application.dataPath + $"/Resources/StageData{stage}.json";
        }
    }
}

