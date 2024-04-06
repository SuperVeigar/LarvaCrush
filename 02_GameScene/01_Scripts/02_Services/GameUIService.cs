using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

namespace SuperVeigar
{
    public class GameUIService : MonoBehaviour
    {
        [SerializeField] Button title;

        private void Start()
        {
            BindView();
        }

        private void BindView()
        {
            title.OnClickAsObservable().Subscribe(_ =>
            {
                SceneManager.LoadSceneAsync(Define.Const.TITLE_SCENE);
            }).AddTo(this);
        }
    }
}

