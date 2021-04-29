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

using UnityEditor.Timeline;
using UnityEditor.Timeline.Actions;

using UnityEngine.Timeline;

namespace ManteraUnityLT.Editor
{
    /// <summary>
    /// <see cref="PauseMarker" />を作成するためのカスタムアクション
    /// </summary>
    /// <para>
    /// <see cref="PauseMarker" />に<see cref="HideInMenuAttribute" />をつけたことにより、
    /// デフォルトで表示される作成メニューを非表示にしている
    /// この状態にするとUnity Editor上で追加できなくなるので
    /// 作成するためのアクションをカスタマイズして作る
    /// </para>
    [MenuEntry("Add Pause Marker")]
    internal sealed class PauseMarkerCreateAction : TimelineAction
    {
        private static PauseMarkerEditor Editor { get; } = new PauseMarkerEditor();

        /// <inheritdoc />
        public override bool Execute(ActionContext context)
        {
            // 指定時間がなければなにもしない
            if (!(context.invocationTime is { } invocationTime))
            {
                return false;
            }

            foreach (TimelineControllerTrack track in context.tracks.OfType<TimelineControllerTrack>())
            {
                PauseMarker marker = track.CreateMarker<PauseMarker>(invocationTime);
                Editor.OnCreate(marker: marker, clonedFrom: null);
            }

            TimelineEditor.Refresh(RefreshReason.ContentsAddedOrRemoved);

            return true;
        }

        /// <inheritdoc />
        public override ActionValidity Validate(ActionContext context)
        {
            // 指定時間がなければなにもしない
            if (context.invocationTime == null)
            {
                return ActionValidity.NotApplicable;
            }

            // 選択中のトラックがTimelineControllerTrackでなければならない
            // ※実際はTimeline上にあるMarkerトラックでも動作するが、
            //  指定コンポーネント以外に伝えたくない、という事情があれば
            //  この方法がつかえます
            if (context.tracks.All(trackAsset => !(trackAsset is TimelineControllerTrack)))
            {
                return ActionValidity.NotApplicable;
            }

            return ActionValidity.Valid;
        }
    }
}
