using System;

namespace IntuiLab.Leap.Events
{
    #region Delegate

    public delegate void TouchEventHandler(object sender, TouchEventArgs e);

    #endregion

    /// <summary>
    /// Event for touch detected during pointing
    /// </summary>
    public class TouchEventArgs : EventArgs
    {
        /// <summary>
        /// X coordinate of the touch
        /// </summary>
        private int m_x;
        public int X
        {
            get
            {
                return m_x;
            }
        }

        /// <summary>
        /// Y coordinate of the touch
        /// </summary>
        private int m_y;
        public int Y
        {
            get
            {
                return m_y;
            }
        }

        public TouchEventArgs(int x, int y)
        {
            m_x = x;
            m_y = y;
        }
    }
}
