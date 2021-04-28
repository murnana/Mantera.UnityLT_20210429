using System;

using UnityEngine;
using UnityEngine.Playables;

namespace ManteraUnityLT
{
    /// <summary>
    /// スライドのデータ
    /// </summary>
    [Serializable]
    internal class Slide
    {
        [SerializeField]
        private GameObject m_Slide = default;

        [SerializeField]
        private PlayableDirector m_PlayableDirector = default;

        /// <summary>
        /// スライドの表示・非表示
        /// </summary>
        /// <param name="value"></param>
        public void SetActive(bool value)
        {
            m_Slide.SetActive(value);
        }

        /// <summary>
        /// ページの再生
        /// </summary>
        public void Play()
        {
            m_PlayableDirector.Play();
        }
    }
}
