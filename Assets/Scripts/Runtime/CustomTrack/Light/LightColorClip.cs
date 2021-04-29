using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ManteraUnityLT
{
    /// <summary>
    /// ライトのカラーをコントロールするためのトラックのクリップ
    /// </summary>
    [Serializable]
    [ExcludeFromPreset]
    [ExcludeFromObjectFactory]
    internal sealed class LightColorClip : PlayableAsset, ITimelineClipAsset
    {
        [SerializeField]
        private Color m_Color = Color.white;

        [SerializeField]
        private float m_Intensity = 1.0f;

        /// <summary>
        /// ライトのカラー
        /// </summary>
        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        /// <summary>
        /// ライトの強弱
        /// </summary>
        public float Intensity
        {
            get { return m_Intensity; }
            set { m_Intensity = value; }
        }

    #region ITimelineClipAsset

        /// <inheritdoc />
        public ClipCaps clipCaps
        {
            get { return ClipCaps.All; }
        }

    #endregion

    #region PlayableAsset

        /// <inheritdoc />
        public override IEnumerable<PlayableBinding> outputs
        {
            get
            {
                // 実はこのoutputを作る必要はない
                yield return ScriptPlayableBinding.Create(
                    name: name,
                    key: this,
                    type: typeof(Light)
                );
            }
        }

        /// <inheritdoc />
        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return LightColorPlayable.Create(
                graph: graph,
                color: m_Color,
                intensity: m_Intensity
            );
        }

    #endregion
    }
}
