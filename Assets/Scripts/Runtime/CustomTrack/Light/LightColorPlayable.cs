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
        /// <see cref="Playable" />を作成します
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
