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

        /// <inheritdoc />
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
            base.GatherProperties(director: director, driver: driver);

            if (director == null)
            {
                return;
            }

            // Timeline上で、どのプロパティを操るのかをエディタに教える
            // (そうすることで、プレビュー終了後に値を戻しておいてくれる)
            Light bindObject = director.GetGenericBinding(this) as Light;
            if (bindObject == null)
            {
                return;
            }

            driver.AddFromName(component: bindObject, name: "m_Color");
            driver.AddFromName(component: bindObject, name: "m_Intensity");
        }

    #endregion
    }
}
