using System.Linq;

using UnityEditor.Timeline;
using UnityEditor.Timeline.Actions;

using UnityEngine.Timeline;

namespace ManteraUnityLT.Editor
{
    /// <summary>
    /// <see cref="PauseMarker"/>を作成するためのカスタムアクション
    /// </summary>
    /// <para>
    /// <see cref="PauseMarker"/>に<see cref="HideInMenuAttribute"/>をつけたことにより、
    /// デフォルトで表示される作成メニューを非表示にしている
    ///
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
