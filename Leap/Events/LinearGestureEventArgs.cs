using Leap;
using System;

namespace IntuiLab.Leap.Events
{
    #region Delegate

    public delegate void LinearGestureEventHandler(object sender, LinearGestureEventArgs e);

    #endregion

    /// <summary>
    /// Event for a linear gesture detection (swipe, tap, push)
    /// </summary>
    public class LinearGestureEventArgs : EventArgs
    {
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

        public LinearGestureEventArgs(float speed, Vector direction, long timestamp)
        {
            m_speed = speed;
            m_direction = direction;
            m_timestamp = timestamp;
        }

        public LinearGestureEventArgs()
        {
            m_speed = 0;
            m_direction = Vector.Zero;
            m_timestamp = 0;
        }
    }
}
