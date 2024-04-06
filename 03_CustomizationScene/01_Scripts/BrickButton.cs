using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace SuperVeigar
{
    public class BrickButton : MonoBehaviour
    {
        [SerializeField]private Button button;
        [SerializeField] private BrickType type;

        private void Start()
        {
            BindView();
        }

        private void BindView()
        {
            button.OnClickAsObservable().Subscribe(_ => 
            {
                Select();
            }).AddTo(this);
        }

        private void Select()
        {
            LevelDesignService.Instance.SelectBrickType(type, transform.position);
        }
    }
}