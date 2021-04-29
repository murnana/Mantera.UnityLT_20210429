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
