using UnityEngine;

namespace SuperVeigar
{
    public class Logic
    {
        private const float MILLISECONDS_PER_SECOND = 1000f;
        public static int MillisecondsFromSeconds(float seconds)
        {
            return (int)(seconds * MILLISECONDS_PER_SECOND);
        }
    }
}