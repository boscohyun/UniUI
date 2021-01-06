using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Boscohyun.UniUI.Examples
{
    public class UIPresenterController : MonoBehaviour
    {
        [SerializeField]
        private UIPresenter _uiWhitePresenter;

        [SerializeField]
        private Button _showButton;
        
        [SerializeField]
        private Button _hideButton;

        private void Awake()
        {
            _showButton.onClick.AsObservable().Subscribe(_ => _uiWhitePresenter.Show()).AddTo(gameObject);
            _hideButton.onClick.AsObservable().Subscribe(_ => _uiWhitePresenter.Hide()).AddTo(gameObject);
        }
    }
}
