using System.Linq;

using UnityEditor;
using UnityEditor.Timeline;

using UnityEngine;
using UnityEngine.Timeline;

namespace ManteraUnityLT.Editor.CustomTrack.TimelineControl
{
    /// <summary>
    /// <see cref="TimelineControllerTrack" />のエディタ拡張
    /// </summary>
    [CustomTimelineEditor(typeof(TimelineControllerTrack))]
    internal sealed class TimelineControllerTrackEditor : TrackEditor
    {
        /// <inheritdoc />
        public override TrackDrawOptions GetTrackOptions(TrackAsset track, Object binding)
        {
            string errorText = GetErrorText(
                track: track,
                boundObject: binding,
                detectErrors: TrackBindingErrors.All
            );

            // エラー(正確にはWarning表示のアイコン)テキストのカスタム
            if (!string.IsNullOrEmpty(errorText))
            {
                // トラックが複数あってはならない
                TimelineAsset timelineAsset = track.timelineAsset;
                if (timelineAsset.GetOutputTracks().OfType<TimelineControllerTrack>().Count() > 1)
                {
                    errorText = string.Format(
                        format: L10n.Tr("Cannot have multiple {0}"),
                        arg0: nameof(TimelineControllerTrack)
                    );
                }
            }

            return new TrackDrawOptions
            {
                errorText     = errorText,
                minimumHeight = DefaultTrackHeight,
                trackColor    = GetTrackColor(track),
                icon          = null,
            };
        }
    }
}
