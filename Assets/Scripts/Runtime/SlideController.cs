using System;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

namespace ManteraUnityLT
{
    /// <summary>
    /// スライドのコントロールを行います
    /// </summary>
    internal sealed class SlideController : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference m_SlideNext = default;

        [SerializeField]
        private InputActionReference m_SlidePrev = default;

        [SerializeField]
        private Slide[] m_Slides = Array.Empty<Slide>();

        /// <summary>
        /// 現在表示しているページのインデックス
        /// </summary>
        private int CurrentPageIndex { get; set; } = 0;

        /// <summary>
        /// 前のページに戻ります
        /// </summary>
        /// <param name="obj"></param>
        private void OnSlidePrev(InputAction.CallbackContext obj)
        {
            if (CurrentPageIndex <= 0)
            {
                return;
            }

            m_Slides[CurrentPageIndex].SetActive(false);
            m_Slides[--CurrentPageIndex].SetActive(true);
        }

        /// <summary>
        /// 次のページに進みます
        /// </summary>
        /// <param name="obj"></param>
        private void OnSlideNext(InputAction.CallbackContext obj)
        {
            if (CurrentPageIndex >= (m_Slides.Length - 1))
            {
                return;
            }

            m_Slides[CurrentPageIndex].SetActive(false);
            m_Slides[++CurrentPageIndex].SetActive(true);
        }

    #region MonoBehaviour

        private void Start()
        {
            m_SlideNext.action.performed += OnSlideNext;
            m_SlidePrev.action.performed += OnSlidePrev;
        }

        private void OnEnable()
        {
            m_SlideNext.action.Enable();
            m_SlidePrev.action.Disable();
        }

        private void OnDisable()
        {
            m_SlideNext.action.Disable();
            m_SlidePrev.action.Disable();
        }

    #endregion
    }
}
