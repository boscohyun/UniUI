using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Boscohyun.UniUI.Examples
{
    public class UIPresenterController : MonoBehaviour
    {
        [SerializeField]
        private UIPresenter uiWhitePresenter;

        [SerializeField]
        private Button showButton;
        
        [SerializeField]
        private Button hideButton;

        private void Awake()
        {
            showButton.onClick.AsObservable().Subscribe(_ => uiWhitePresenter.Show()).AddTo(gameObject);
            hideButton.onClick.AsObservable().Subscribe(_ => uiWhitePresenter.Hide()).AddTo(gameObject);
        }
    }
}
