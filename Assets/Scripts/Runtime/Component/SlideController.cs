// This is free and unencumbered software released into the public domain.
//
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
//
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
// For more information, please refer to <https://unlicense.org>

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
        private InputActionReference m_SlideNext;

        [SerializeField]
        private InputActionReference m_SlidePrev;

        [SerializeField]
        private PlayableDirector m_PlayableDirector;

        [SerializeField]
        private TimelineController m_TimelineController;

        private PauseMarker[] PauseMarkers { get; set; } = Array.Empty<PauseMarker>();

        /// <summary>
        /// 前のページに戻ります
        /// </summary>
        /// <param name="obj"></param>
        private void OnSlidePrev(InputAction.CallbackContext obj)
        {
            PauseMarker prevMarker =
                PauseMarkers.Reverse().FirstOrDefault(marker => m_PlayableDirector.time > marker.time);
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
