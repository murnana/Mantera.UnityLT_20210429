using UnityEditor.Timeline;

using UnityEngine.Timeline;

namespace ManteraUnityLT.Editor
{
    /// <summary>
    /// <see cref="PauseMarker"/>のエディタ拡張
    /// </summary>
    [CustomTimelineEditor(typeof(PauseMarker))]
    internal sealed class PauseMarkerEditor : MarkerEditor
    {
        /// <inheritdoc />
        public override void OnCreate(IMarker marker, IMarker clonedFrom)
        {
            if (marker is PauseMarker pauseMarker)
            {
                pauseMarker.name = "Pause";
            }
        }
    }
}
