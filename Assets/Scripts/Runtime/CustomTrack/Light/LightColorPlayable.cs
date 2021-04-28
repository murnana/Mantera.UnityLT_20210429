using UnityEngine;
using UnityEngine.Playables;

namespace ManteraUnityLT
{
    /// <summary>
    /// ライトのカラーを持つ<see cref="Playable" />
    /// </summary>
    internal sealed class LightColorPlayable : PlayableBehaviour
    {
        /// <summary>
        /// ライトのカラー
        /// </summary>
        public Color Color { get; private set; }

        /// <summary>
        /// ライトの強さ
        /// </summary>
        public float Intensity { get; private set; }

        /// <summary>
        /// <see cref="Playable"/>を作成します
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="color"></param>
        /// <param name="intensity"></param>
        /// <returns></returns>
        public static Playable Create(PlayableGraph graph, Color color, float intensity)
        {
            // Playableの作成
            ScriptPlayable<LightColorPlayable> playable = ScriptPlayable<LightColorPlayable>.Create(graph: graph);

            // 中の初期化
            LightColorPlayable behaviour = playable.GetBehaviour();
            behaviour.Color     = color;
            behaviour.Intensity = intensity;

            return playable;
        }
    }
}
