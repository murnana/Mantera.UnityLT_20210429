using System;
using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;

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
        private PlayableDirector m_PlayableDirector = default;

        [SerializeField]
        private TimelineController m_TimelineController = default;

        private PauseMarker[] PauseMarkers { get; set; } = Array.Empty<PauseMarker>();

        /// <summary>
        /// 前のページに戻ります
        /// </summary>
        /// <param name="obj"></param>
        private void OnSlidePrev(InputAction.CallbackContext obj)
        {
            PauseMarker prevMarker = PauseMarkers.Reverse().FirstOrDefault(marker => m_PlayableDirector.time > marker.time);
            if (prevMarker != null)
            {
                m_PlayableDirector.time = prevMarker.time;
                // Debug.Log("On Prev Time");
            }
        }

        /// <summary>
        /// 次のページに進みます
        /// </summary>
        /// <param name="obj"></param>
        private void OnSlideNext(InputAction.CallbackContext obj)
        {
            // 一時停止中なら時間を進める
            if (m_TimelineController.IsPaused)
            {
                m_PlayableDirector.Resume();
                // Debug.Log("On Next Resume");
            }
            else
            {
                // 一時停止まで進める
                PauseMarker nextMarker = PauseMarkers.FirstOrDefault(marker => m_PlayableDirector.time <= marker.time);
                if (nextMarker != null)
                {
                    m_PlayableDirector.time = nextMarker.time;
                    // Debug.Log("On Next Marker");
                }
            }
        }

    #region MonoBehaviour

        private void Start()
        {
            // 一時停止マーカーの数を確認する
            if (m_PlayableDirector.playableAsset is TimelineAsset timelineAsset)
            {
                TimelineControllerTrack timelineControllerTrack = timelineAsset.GetOutputTracks()
                                                                               .OfType<TimelineControllerTrack>()
                                                                               .FirstOrDefault();
                if (timelineControllerTrack is { })
                {
                    PauseMarkers = timelineControllerTrack.GetMarkers().OfType<PauseMarker>().ToArray();
                }
            }

            m_SlideNext.action.started += OnSlideNext;
            m_SlidePrev.action.started += OnSlidePrev;
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
