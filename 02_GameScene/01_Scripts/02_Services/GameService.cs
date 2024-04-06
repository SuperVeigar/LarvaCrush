using System.Collections.Generic;
using UnityEngine;

namespace SuperVeigar
{
    public enum GameState
    {
        Ready,
        Play,
    }

    public class GameService : SingletonBehaviour<GameService>
    {
        private const int START_STAGE = 1;
        private bool isPause;
        private int currentStage = START_STAGE;
        public GameState gameState;
        [SerializeField] private List<IngameObject> playableObject;

        private void Start()
        {
            SoundService.Instance.PlayBGM(SoundReferenceType.BGM_GAME);
            LevelDesignService.Instance.LoadStage(currentStage);
        }

        private void Update()
        {
            if (isPause == false)
            {
                switch (gameState)
                {
                    case GameState.Ready:
                        OnReadyState();
                        break;
                    case GameState.Play:
                        OnPlayState();
                        break;
                }
            }
        }

        private void FixedUpdate()
        {
            if (isPause == false)
            {
                switch (gameState)
                {
                    case GameState.Play:
                        OnPlayFixedState();
                        break;
                }
            }
        }

        public void Reset()
        {
            isPause = false;
            currentStage = START_STAGE;
            gameState = GameState.Ready;

            foreach (IngameObject obj in playableObject)
            {
                obj.Reset();
            }

            ObjectPoolService.Instance.Reset();
        }

        private void OnReadyState()
        {
            foreach (IngameObject obj in playableObject)
            {
                obj.OnReadyState();
            }
        }

        private void OnPlayState()
        {
            foreach (IngameObject obj in playableObject)
            {
                obj.OnPlayState();
            }
        }

        private void OnPlayFixedState()
        {
            foreach (IngameObject obj in playableObject)
            {
                obj.OnPlayFixedState();
            }
        }

        public void SetGameState(GameState state)
        {
            gameState = state;
        }
    }
}