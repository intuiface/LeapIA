using IntuiLab.Leap.Events;
using System;
using System.ComponentModel;

namespace IntuiLab.Leap
{
    /// <summary>
    /// A facade for the general properties and events
    /// </summary>
    public class HandFacade : INotifyPropertyChanged, IDisposable
    {
        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(String strInfo)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(strInfo));
            }
        }


        #region Enable Properties

        ///// <summary>
        ///// Enable the pointing
        ///// </summary>
        //private bool m_enableFingerPointing;
        //public bool EnableFingerPointing
        //{
        //    get
        //    {
        //        return m_enableFingerPointing;
        //    }
        //    set
        //    {
        //        if (value && !m_enableFingerPointing)
        //            LeapListener.EnablePointing();
        //        else if (!value && m_enableFingerPointing)
        //            LeapListener.DisablePointing();
        //        m_enableFingerPointing = value;
        //        NotifyPropertyChanged("EnableFingerPointing");
        //    }
        //}

        #endregion

        #region Fingers Properties

        ///// <summary>
        ///// The x coordinate on screen of the finger for pointing
        ///// </summary>
        //private int m_xFingerOnScreen;
        //public int XFingerOnScreen
        //{
        //    get
        //    {
        //        return m_xFingerOnScreen;
        //    }
        //    set
        //    {
        //        m_xFingerOnScreen = value;
        //        NotifyPropertyChanged("XFingerOnScreen");
        //    }
        //}

        ///// <summary>
        ///// The y coordinate on screen of the finger for pointing
        ///// </summary>
        //private int m_yFingerOnScreen;
        //public int YFingerOnScreen
        //{
        //    get
        //    {
        //        return m_yFingerOnScreen;
        //    }
        //    set
        //    {
        //        m_yFingerOnScreen = value;
        //        NotifyPropertyChanged("YFingerOnScreen");
        //    }
        //}

        #endregion

        #endregion

        #region Events

        public event HandEventHandler HandPresent;
        public event HandEventHandler HandRemoved;

        private void RaiseHandPresentEvent(HandEventArgs e)
        {
            if (HandPresent != null)
                HandPresent(this, e);
        }

        private void RaiseHandRemovedEvent(HandEventArgs e)
        {
            if (HandRemoved != null)
                HandRemoved(this, e);
        }

        private void OnHandPresent(object sender, HandEventArgs e)
        {
            RaiseHandPresentEvent(e);
        }

        private void OnHandRemoved(object sender, HandEventArgs e)
        {
            RaiseHandRemovedEvent(e);
        }

        #endregion

        #region Constructor

        public HandFacade()
        {
            // if the LeapPlugin is not already instantiated, we do it
            if (LeapPlugin.Instance == null)
            {
                LeapPlugin temp = new LeapPlugin();
            }

            Subscribe();
            Main.RegisterFacade(this);
        }

        #endregion

        /// <summary>
        /// Subscribe to the hands events
        /// </summary>
        public void Subscribe()
        {
            // we subscribe to the general events
            LeapPlugin.Instance.LeapListener.HandPresent += OnHandPresent;
            LeapPlugin.Instance.LeapListener.HandRemoved += OnHandRemoved;
        }

        /// <summary>
        /// Unsubscribe to the hands events
        /// </summary>
        public void Unsubscribe()
        {
            LeapPlugin.Instance.LeapListener.HandPresent -= OnHandPresent;
            LeapPlugin.Instance.LeapListener.HandRemoved -= OnHandRemoved;
        }

        /// <summary>
        /// Clear the resources
        /// </summary>
        public void Dispose()
        {
            Unsubscribe();
        }
    }
}
