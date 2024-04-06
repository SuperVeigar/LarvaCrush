using UnityEngine;
using UniRx;
using UnityEngine.UI;

namespace SuperVeigar
{
    public class SoundButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private AudioClip sound;

        private void Start()
        {
            button.OnClickAsObservable().Subscribe(_ =>
            {
                SoundService.Instance.PlaySound(sound);
            }).AddTo(this);
        }
    }
}

