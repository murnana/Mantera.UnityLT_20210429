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
    /// ライトの色をミックスさせる<see cref="Playable" />
    /// </summary>
    internal sealed class LightColorMixerPlayable : PlayableBehaviour
    {
        private bool  IsInitialize     { get; set; }
        private Color DefaultColor     { get; set; }
        private float DefaultIntensity { get; set; }

        /// <summary>
        /// <see cref="Playable" />を作成します
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="inputCount"></param>
        /// <returns></returns>
        public static Playable Create(PlayableGraph graph, int inputCount)
        {
            ScriptPlayable<LightColorMixerPlayable> playable =
                ScriptPlayable<LightColorMixerPlayable>.Create(graph: graph, inputCount: inputCount);
            return playable;
        }

    #region PlayableBehaviour

        /// <inheritdoc />
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (info.effectivePlayState == PlayState.Paused)
            {
                IsInitialize = false;
            }
        }

        /// <inheritdoc />
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if ((info.effectiveWeight <= 0) || (info.effectivePlayState == PlayState.Paused))
            {
                return;
            }

            int inputCount = playable.GetInputCount();
            if (inputCount <= 0)
            {
                return;
            }

            if (!(playerData is Light light))
            {
                Debug.LogWarningFormat(
                    format: "{0} is not typeof {1}",
                    nameof(playerData),
                    nameof(LightColorClip)
                );
                return;
            }

            if (!IsInitialize)
            {
                DefaultColor     = light.color;
                DefaultIntensity = light.intensity;
                IsInitialize     = true;
            }

            Color color = new Color(
                r: 0,
                g: 0,
                b: 0,
                a: 0
            );
            float intensity   = 0.0f;
            float totalWeight = 0.0f;
            for (int i = 0; i < inputCount; i++)
            {
                float    weight        = playable.GetInputWeight(i);
                Playable inputPlayable = playable.GetInput(i);
                if (inputPlayable.GetPlayableType() != typeof(LightColorPlayable))
                {
                    continue;
                }

                LightColorPlayable behaviour = ((ScriptPlayable<LightColorPlayable>) inputPlayable).GetBehaviour();
                color       += behaviour.Color     * weight * info.effectiveWeight;
                intensity   += behaviour.Intensity * weight * info.effectiveWeight;
                totalWeight += weight;
            }

            if (totalWeight < 1.0f)
            {
                float diffWeight = 1.0f - totalWeight;
                color     += DefaultColor     * diffWeight;
                intensity += DefaultIntensity * diffWeight;
            }

            light.color     = color;
            light.intensity = intensity;
        }

    #endregion
    }
}
