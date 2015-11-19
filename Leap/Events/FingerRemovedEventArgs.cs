using System;

namespace IntuiLab.Leap.Events
{
    #region Delegate

    public delegate void FingerRemovedEventHandler(object sender, FingerRemovedEventArgs e);

    #endregion

    /// <summary>
    /// Event when a finger is removed
    /// </summary>
    public class FingerRemovedEventArgs : EventArgs
    {
        private int m_id;
        public int Id
        {
            get
            {
                return m_id;
            }
        }

        public FingerRemovedEventArgs(int id)
        {
            m_id = id;
        }

        public FingerRemovedEventArgs()
        {
            m_id = -1;
        }
    }
}
