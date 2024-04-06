using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using Cysharp.Threading.Tasks;

namespace SuperVeigar
{
    public class TitleService : SingletonBehaviour<TitleService>
    {
        private const int BUTTON_DELAY = 350;
        [SerializeField] private Button start;
        [SerializeField] private Button exit;
        [SerializeField] private Button superviegar;
        [SerializeField] private Button kkemon;

        private void Start()
        {
            SoundService.Instance.PlayBGM(SoundReferenceType.BGM_TITLE);
            BindVieW();
        }

        private void BindVieW()
        {
            start.OnClickAsObservable().Subscribe(_ => 
            {
                SceneManager.LoadSceneAsync(Define.Const.GAME_SCENE);
            }).AddTo(this);

            exit.OnClickAsObservable().Subscribe(async _ => 
            {
                await UniTask.Delay(BUTTON_DELAY);
                Application.Quit();
            }).AddTo(this);

            superviegar.OnClickAsObservable().Subscribe(async _ => 
            {
                await UniTask.Delay(BUTTON_DELAY);
                Application.OpenURL(Define.Const.SUPERVEIGAR_YOUTUBE);
            }).AddTo(this);

            kkemon.OnClickAsObservable().Subscribe(async _ => 
            {
                await UniTask.Delay(BUTTON_DELAY);
                Application.OpenURL(Define.Const.KKEMON_YOUTUBE);
            }).AddTo(this);
        }
    }
}

