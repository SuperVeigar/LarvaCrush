using UnityEngine;
using System.Collections.Generic;

namespace SuperVeigar
{
    [CreateAssetMenu(fileName = "SoundReference", menuName = "Scriptable Object/SoundReference")]
    public class SoundReference : ScriptableObject
    {
        public List<AudioClip> audioClips;
    }
}

