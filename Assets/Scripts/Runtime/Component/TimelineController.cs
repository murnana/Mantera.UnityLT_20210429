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
        private PlayableDirector m_PlayableDirector = default;

        /// <summary>
        /// <see cref="PlayableDirector"/>が一時停止中か
        /// </summary>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// スキップする一時停止マーカー
        /// </summary>
        private List<PauseMarker> SkipPauseMarkers { get; } = new List<PauseMarker>();

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
        /// <see cref="PlayableDirector"/>が再生を一時停止したときに呼ばれます
        /// </summary>
        /// <param name="obj"></param>
        private void PlayableDirectorOnPaused(PlayableDirector obj)
        {
            IsPaused = true;
            // Debug.Log("Paused");
        }

        /// <summary>
        /// <see cref="PlayableDirector"/>が再生を停止したときに呼ばれます
        /// </summary>
        /// <param name="obj"></param>
        private void PlayableDirectorOnStopped(PlayableDirector obj)
        {
            // Debug.Log("Stopped");
            IsPaused = false;
            SkipPauseMarkers.Clear();
        }

        /// <summary>
        /// <see cref="PlayableDirector"/>が再生をしたときに呼ばれます
        /// </summary>
        /// <param name="obj"></param>
        private void PlayableDirectorOnPlayed(PlayableDirector obj)
        {
            // Debug.Log("OnPlayed");
            IsPaused = false;
        }


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

    #region MonoBehaviour

        private void Start()
        {
            m_PlayableDirector.paused += PlayableDirectorOnPaused;
            m_PlayableDirector.played += PlayableDirectorOnPlayed;
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
