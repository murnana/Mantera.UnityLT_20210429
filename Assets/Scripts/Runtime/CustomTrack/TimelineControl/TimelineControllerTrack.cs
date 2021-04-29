using System;
using System.ComponentModel;

using UnityEngine;
using UnityEngine.Timeline;

namespace ManteraUnityLT
{
    /// <summary>
    /// <see cref="TimelineController"/>のトラック
    /// </summary>
    [Serializable]
    [ExcludeFromPreset]
    [ExcludeFromObjectFactory]
    [TrackBindingType(typeof(TimelineController))]
#if UNITY_EDITOR
    [DisplayName("ManteraUnityLT/Timeline Controller")]
#endif
    public sealed class TimelineControllerTrack : TrackAsset
    {
    }
}
