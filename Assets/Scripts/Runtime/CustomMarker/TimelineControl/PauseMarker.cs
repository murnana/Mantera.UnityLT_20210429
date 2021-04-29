using System;

using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace ManteraUnityLT
{
    /// <summary>
    /// 一時停止マーカー
    /// </summary>
    [HideInMenu]
    [Serializable]
    [ExcludeFromPreset]
    [ExcludeFromObjectFactory]
    internal sealed class PauseMarker : Marker, INotification
    {
    #region Marker

        /// <summary>
        /// This function is called when the object is loaded.
        /// </summary>
        private void OnEnable()
        {
            id = new PropertyName("Pause");
        }

    #endregion

        /// <inheritdoc />
        public PropertyName id { get; private set; }

    #region INotification

    #endregion
    }
}
