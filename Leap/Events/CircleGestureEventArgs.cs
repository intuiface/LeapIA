using System;

namespace IntuiLab.Leap.Events
{
    #region Delegate

    public delegate void CircleGestureEventHandler(object sender, CircleGestureEventArgs e);

    #endregion

    /// <summary>
    /// Event for the circle gesture detection
    /// </summary>
    public class CircleGestureEventArgs : EventArgs
    {
        private int m_id;
        public int Id
        {
            get
            {
                return m_id;
            }
        }

        private float m_progress;
        public float Progress
        {
            get
            {
                return m_progress;
            }
        }

        private string m_clockwiseness;
        public string Clockwiseness
        {
            get
            {
                return m_clockwiseness;
            }
        }

        /// <summary>
        /// The angle done since the beginning of the movement
        /// </summary>
        private float m_angle;
        public float Angle
        {
            get
            {
                return m_angle;
            }
        }

        public CircleGestureEventArgs(int id, float progress, string clockwisness, float angle)
        {
            m_id = id;
            m_progress = progress;
            m_clockwiseness = clockwisness;
            m_angle = angle;
        }
    }
}
