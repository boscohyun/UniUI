using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Boscohyun.UniUI.Examples.UICanvasPresenter
{
    public class UICanvasPresenterWithUpdateTexts : UniUI.UICanvasPresenter
    {
        [SerializeField]
        private Text leftText;
        
        [SerializeField]
        private Text rightText;

        private int _leftFrameCount;
        
        private int _rightFrameCount;

        private void Update()
        {
            _leftFrameCount += 1;
            leftText.text = $"Frame Count: {_leftFrameCount}";
        }

        private IEnumerator UpdateRightText()
        {
            while (enabled && animationState == AnimationState.Shown)
            {
                _rightFrameCount += 1;
                rightText.text = $"Frame Count: {_rightFrameCount}";
                yield return null;
            }
        }

        protected override void ShowAnimationEnd()
        {
            base.ShowAnimationEnd();
            MainThreadDispatcher.StartUpdateMicroCoroutine(UpdateRightText());
        }
    }
}
