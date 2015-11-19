using System;

namespace IntuiLab.Leap.Events
{
    #region Delegate

    public delegate void FingerPresentEventHandler(object sender, FingerPresentEventArgs e);

    #endregion

    /// <summary>
    /// Event when a finger is present
    /// </summary>
    public class FingerPresentEventArgs : EventArgs
    {
        private int m_id;
        public int Id
        {
            get
            {
                return m_id;
            }
        }

        private float m_x;
        public float X
        {
            get
            {
                return m_x;
            }
        }

        private float m_y;
        public float Y
        {
            get
            {
                return m_y;
            }
        }

        private float m_z;
        public float Z
        {
            get
            {
                return m_z;
            }
        }

        /// <summary>
        /// X coordinate of the intersection point between the finger and the screen, in pixels
        /// </summary>
        private int m_xOnScreen;
        public int XOnScreen
        {
            get
            {
                return m_xOnScreen;
            }
        }

        /// <summary>
        /// Y coordinate of the intersection point between the finger and the screen, in pixels
        /// </summary>
        private int m_yOnScreen;
        public int YOnScreen
        {
            get
            {
                return m_yOnScreen;
            }
        }

        public FingerPresentEventArgs(int id, float x, float y, float z, int xOnScreen, int yOnScreen)
        {
            m_id = id;
            m_x = x;
            m_y = y;
            m_z = z;
            m_xOnScreen = xOnScreen;
            m_yOnScreen = yOnScreen;
        }

        public FingerPresentEventArgs(int xOnScreen, int yOnScreen)
        {
            m_id = -1;
            m_x = -1;
            m_y = -1;
            m_z = -1;
            m_xOnScreen = xOnScreen;
            m_yOnScreen = yOnScreen;
        }
    }
}
