using System;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
#if UNITY_EDITOR
using System.ComponentModel;

#endif

namespace ManteraUnityLT
{
    /// <summary>
    /// ライトの色をコントロールするためのトラック
    /// </summary>
    [Serializable]
    [ExcludeFromPreset]
    [ExcludeFromObjectFactory]
    [TrackBindingType(typeof(Light))]
    [TrackClipType(clipClass: typeof(LightColorClip))]
#if UNITY_EDITOR
    [DisplayName("ManteraUnityLT/Light Color")]
#endif
    internal sealed class LightColorTrack : TrackAsset
    {
    #region TrackAsset

        /// <inheritdoc />
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return LightColorMixerPlayable.Create(graph: graph, inputCount: inputCount);
        }

    #endregion
    }
}
