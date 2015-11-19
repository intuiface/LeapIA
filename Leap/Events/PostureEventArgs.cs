using System;

namespace IntuiLab.Leap.Events
{
    #region Delegate

    public delegate void PostureEventHandler(object sender, PostureEventArgs e);

    #endregion

    /// <summary>
    /// Event for posture detected
    /// </summary>
    public class PostureEventArgs : EventArgs
    {
        private int m_percentage;
        public int Percentage
        {
            get
            {
                return m_percentage;
            }
        }

        public PostureEventArgs(int percentage)
        {
            this.m_percentage = percentage;
        }

        public PostureEventArgs()
        {
            this.m_percentage = -1;
        }
    }
}
