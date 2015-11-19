using IntuiLab.Leap.Events;
using IntuiLab.Leap.Recognition.Gestures;
using System;
using System.ComponentModel;

namespace IntuiLab.Leap
{
    /// <summary>
    /// A facade for the gestures Enable/Disable properties and events
    /// </summary>
    public class GestureFacade : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(String strInfo)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(strInfo));
            }
        }

        #region Properties

        /// <summary>
        /// Enable or disable the Swipe Left gesture
        /// </summary>
        private bool m_enableSwipeLeftGesture;
        public bool EnableSwipeLeftGesture
        {
            get
            {
                return m_enableSwipeLeftGesture;
            }
            set
            {
                if (value && !m_enableSwipeLeftGesture)
                {
                    LeapPlugin.Instance.LeapListener.EnableGestureRecognition(GestureType.SwipeLeft);
                    LeapPlugin.Instance.LeapListener.SwipeLeftGestureDetected += OnSwipeLeftDetected;
                }
                else if (!value && m_enableSwipeLeftGesture)
                {
                    LeapPlugin.Instance.LeapListener.DisableGestureRecognition(GestureType.SwipeLeft);
                    LeapPlugin.Instance.LeapListener.SwipeLeftGestureDetected -= OnSwipeLeftDetected;
                }
                m_enableSwipeLeftGesture = value;
                NotifyPropertyChanged("EnableSwipeLeftGesture");
            }
        }

        /// <summary>
        /// Enable or disable the Swipe Right gesture
        /// </summary>
        private bool m_enableSwipeRightGesture;
        public bool EnableSwipeRightGesture
        {
            get
            {
                return m_enableSwipeRightGesture;
            }
            set
            {
                if (value && !m_enableSwipeRightGesture)
                {
                    LeapPlugin.Instance.LeapListener.EnableGestureRecognition(GestureType.SwipeRight);
                    LeapPlugin.Instance.LeapListener.SwipeRightGestureDetected += OnSwipeRightDetected;
                }
                else if (!value && m_enableSwipeRightGesture)
                {
                    LeapPlugin.Instance.LeapListener.DisableGestureRecognition(GestureType.SwipeRight);
                    LeapPlugin.Instance.LeapListener.SwipeRightGestureDetected -= OnSwipeRightDetected;
                }
                m_enableSwipeRightGesture = value;
                NotifyPropertyChanged("EnableSwipeRightGesture");
            }
        }

        /// <summary>
        /// Enable or disable the Tap gesture
        /// </summary>
        private bool m_enableTapGesture;
        public bool EnableTapGesture
        {
            get
            {
                return m_enableTapGesture;
            }
            set
            {
                if (value && !m_enableTapGesture)
                {
                    LeapPlugin.Instance.LeapListener.EnableGestureRecognition(GestureType.Tap);
                    LeapPlugin.Instance.LeapListener.TapGestureDetected += OnTapGestureDetected;
                }
                else if (!value && m_enableTapGesture)
                {
                    LeapPlugin.Instance.LeapListener.DisableGestureRecognition(GestureType.Tap);
                    LeapPlugin.Instance.LeapListener.TapGestureDetected -= OnTapGestureDetected;
                }
                m_enableTapGesture = value;
                NotifyPropertyChanged("EnableTapGesture");
            }
        }

        #endregion

        #region Events

        public event LinearGestureEventHandler TapGestureDetected;
        public event LinearGestureEventHandler SwipeRightGestureDetected;
        public event LinearGestureEventHandler SwipeLeftGestureDetected;

        
        private void RaiseTapGestureEvent()
        {
            if (TapGestureDetected != null)
                TapGestureDetected(this, new LinearGestureEventArgs());
        }

        private void RaiseSwipeRightGestureEvent()
        {
            if (SwipeRightGestureDetected != null)
                SwipeRightGestureDetected(this, new LinearGestureEventArgs());
        }

        private void RaiseSwipeLeftGestureEvent()
        {
            if (SwipeLeftGestureDetected != null)
                SwipeLeftGestureDetected(this, new LinearGestureEventArgs());
        }


        private void OnSwipeLeftDetected(object sender, LinearGestureEventArgs e)
        {
            RaiseSwipeLeftGestureEvent();
        }

        private void OnSwipeRightDetected(object sender, LinearGestureEventArgs e)
        {
            RaiseSwipeRightGestureEvent();
        }

        private void OnTapGestureDetected(object sender, LinearGestureEventArgs e)
        {
            RaiseTapGestureEvent();
        }

        #endregion

        #region Constructor

        public GestureFacade()
        {
           // if the LeapPlugin is not already instantiated, we do it
            if (LeapPlugin.Instance == null)
            {
                LeapPlugin temp = new LeapPlugin();
            }

            this.m_enableSwipeLeftGesture = false;
            this.m_enableSwipeRightGesture = false;
            this.m_enableTapGesture = false;

            EnableSwipeLeftGesture = true;
            EnableSwipeRightGesture = true;
            EnableTapGesture = true;

            Main.RegisterFacade(this);
        }

        #endregion

        /// <summary>
        /// Clear the resources
        /// </summary>
        public void Dispose()
        {
            EnableSwipeLeftGesture = false;
            EnableSwipeRightGesture = false;
            EnableTapGesture = false;
        }
    }
}
