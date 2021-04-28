using UnityEditor;
using UnityEditor.Timeline;

using UnityEngine;
using UnityEngine.Timeline;

namespace ManteraUnityLT.Editor
{
    /// <summary>
    /// <see cref="LightColorClip" />のエディタ拡張
    /// </summary>
    [CustomTimelineEditor(typeof(LightColorClip))]
    internal sealed class LightColorClipEditor : ClipEditor
    {
        /// <inheritdoc />
        public override void DrawBackground(TimelineClip clip, ClipBackgroundRegion region)
        {
            Rect rect = region.position;
            if (rect.width <= 0)
            {
                return;
            }

            if (!(clip.asset is LightColorClip lightColorClip))
            {
                return;
            }

            Color color = lightColorClip.Color;
            Rect quantizedRect = new Rect(
                x: Mathf.Ceil(rect.x),
                y: Mathf.Ceil(rect.y),
                width: Mathf.Ceil(rect.width),
                height: Mathf.Ceil(rect.height)
            );
            EditorGUI.DrawRect(rect: quantizedRect, color: color);
        }
    }
}
