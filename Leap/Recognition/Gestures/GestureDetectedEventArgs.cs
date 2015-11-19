using Leap;
using System;

namespace IntuiLab.Leap.Recognition.Gestures
{
    #region Delegate

    internal delegate void GestureDetectedEventHandler(object sender, GestureDetectedEventArgs e);

    #endregion

    /// <summary>
    /// Event for gesture detected
    /// </summary>
    internal class GestureDetectedEventArgs : EventArgs
    {
        private GestureType m_type;
        public GestureType Type
        {
            get
            {
                return m_type;
            }
        }

        private float m_speed;
        public float Speed
        {
            get
            {
                return m_speed;
            }
        }

        private Vector m_direction;
        public Vector Direction
        {
            get
            {
                return m_direction;
            }
        }

        private long m_timestamp;
        public long Timestamp
        {
            get
            {
                return m_timestamp;
            }
        }

        public GestureDetectedEventArgs(GestureType type, float speed, Vector direction, long timestamp)
        {
            m_type = type;
            m_speed = speed;
            m_direction = direction;
            m_timestamp = timestamp;
        }
    }
}
