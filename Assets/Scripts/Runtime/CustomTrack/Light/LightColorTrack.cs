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
