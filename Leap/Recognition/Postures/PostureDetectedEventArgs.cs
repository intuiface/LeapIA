using System;

namespace IntuiLab.Leap.Recognition.Postures
{
    #region Delegate

    internal delegate void PostureDetectedEventHandler(object sender, PostureDetectedEventArgs e);

    #endregion

    /// <summary>
    /// Event for posture detected
    /// </summary>
    internal class PostureDetectedEventArgs : EventArgs
    {
        private PostureType m_type;
        public PostureType Type
        {
            get
            {
                return m_type;
            }
        }

        private int m_percentage;
        public int Percentage
        {
            get
            {
                return m_percentage;
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

        public PostureDetectedEventArgs(PostureType type, long timestamp)
        {
            this.m_type = type;
            this.m_percentage = -1;
            this.m_timestamp = timestamp;
        }

        public PostureDetectedEventArgs(PostureType type, int percentage, long timestamp)
        {
            this.m_type = type;
            this.m_percentage = percentage;
            this.m_timestamp = timestamp;
        }
    }
}
