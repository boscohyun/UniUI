using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Boscohyun.UniUI.Examples.NestedUIPresenter
{
    public class NestedUIPresenterWithTextInjection : UniUI.UIPresenter
    {
        [SerializeField]
        private Text injectedText;
        
        [Header("Animation")]
        
        [SerializeField, Range(0f, 2f)]
        private float showDuration = 1f;

        public void Show(string text)
        {
            injectedText.text = text;
            Show();
        }
        
        public IObservable<NestedUIPresenterWithTextInjection> ShowAsObservable(
            string text,
            bool skipAnimation = default)
        {
            injectedText.text = text;
            return base.ShowAsObservable(skipAnimation).Select(_ => this);
        }
        
        protected override void ShowAnimationBegin(bool skip = default)
        {
            base.ShowAnimationBegin(skip);
            transform.localScale = skip ? Vector3.one : Vector3.zero;
        }

        protected override async UniTask ShowAnimationAsync()
        {
            float deltaScale = 1f / showDuration;
            while (transform.localScale.x < 1f)
            {
                transform.localScale += new Vector3(deltaScale, deltaScale, deltaScale) * Time.deltaTime;
                await UniTask.NextFrame();
            }

            transform.localScale = Vector3.one;
        }
    }
}
