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
