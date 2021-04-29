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
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Playables;

namespace ManteraUnityLT
{
    /// <summary>
    /// <see cref="PlayableDirector" />による再生をコントロールします
    /// </summary>
    [Serializable]
    [ExcludeFromPreset]
    [ExcludeFromObjectFactory]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayableDirector))]
    internal sealed class TimelineController : MonoBehaviour, INotificationReceiver
    {
        [SerializeField]
        [HideInInspector]
        private PlayableDirector m_PlayableDirector;

        /// <summary>
        /// <see cref="PlayableDirector" />が一時停止中か
        /// </summary>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// スキップする一時停止マーカー
        /// </summary>
        private List<PauseMarker> SkipPauseMarkers { get; } = new List<PauseMarker>();


    #region INotificationReceiver

        /// <inheritdoc />
        public void OnNotify(Playable origin, INotification notification, object context)
        {
            if (notification is PauseMarker pauseMarker)
            {
                if (SkipPauseMarkers.Contains(pauseMarker))
                {
                    // 一時停止しない
                    // Debug.Log("Ignore Skip");
                    SkipPauseMarkers.Remove(pauseMarker);
                }
                else
                {
                    // 一時停止
                    // Debug.Log("Pause from Marker");
                    m_PlayableDirector.Pause();
                    m_PlayableDirector.time = pauseMarker.time;
                    SkipPauseMarkers.Add(pauseMarker);
                }
            }
        }

    #endregion

        /// <summary>
        /// スキップする一時停止マーカーを追加します
        /// ※スキップしたら削除されます
        /// </summary>
        /// <param name="pauseMarker"></param>
        public void AddSkipPauseMarker(PauseMarker pauseMarker)
        {
            SkipPauseMarkers.Add(pauseMarker);
        }

        /// <summary>
        /// <see cref="PlayableDirector" />が再生を一時停止したときに呼ばれます
        /// </summary>
        /// <param name="obj"></param>
        private void PlayableDirectorOnPaused(PlayableDirector obj)
        {
            IsPaused = true;
            // Debug.Log("Paused");
        }

        /// <summary>
        /// <see cref="PlayableDirector" />が再生を停止したときに呼ばれます
        /// </summary>
        /// <param name="obj"></param>
        private void PlayableDirectorOnStopped(PlayableDirector obj)
        {
            // Debug.Log("Stopped");
            IsPaused = false;
            SkipPauseMarkers.Clear();
        }

        /// <summary>
        /// <see cref="PlayableDirector" />が再生をしたときに呼ばれます
        /// </summary>
        /// <param name="obj"></param>
        private void PlayableDirectorOnPlayed(PlayableDirector obj)
        {
            // Debug.Log("OnPlayed");
            IsPaused = false;
        }

    #region MonoBehaviour

        private void Start()
        {
            m_PlayableDirector.paused  += PlayableDirectorOnPaused;
            m_PlayableDirector.played  += PlayableDirectorOnPlayed;
            m_PlayableDirector.stopped += PlayableDirectorOnStopped;
        }

    #if UNITY_EDITOR
        private void Reset()
        {
            m_PlayableDirector = GetComponent<PlayableDirector>();
        }
    #endif

    #endregion
    }
}
