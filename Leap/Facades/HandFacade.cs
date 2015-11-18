            // ****************************************************************************
            // <copyright file="HandFacade.cs" company="IntuiLab">
            // INTUILAB CONFIDENTIAL
			//_____________________
			// [2002] - [2015] IntuiLab SA
			// All Rights Reserved.
			// NOTICE: All information contained herein is, and remains
			// the property of IntuiLab SA. The intellectual and technical
			// concepts contained herein are proprietary to IntuiLab SA
			// and may be covered by U.S. and other country Patents, patents
			// in process, and are protected by trade secret or copyright law.
			// Dissemination of this information or reproduction of this
			// material is strictly forbidden unless prior written permission
			// is obtained from IntuiLab SA.
            // </copyright>
            // ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IntuiLab.Leap.Recognition.Gestures;
using IntuiLab.Leap.Recognition.Postures;
using IntuiLab.Leap.Events;
using IntuiLab.Leap.Exceptions;
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

        //public event FingerPresentEventHandler FingerAppears;
        //public event FingerPresentEventHandler FingerMoves;
        //public event FingerRemovedEventHandler FingerRemoved;
        //public event TouchEventHandler TouchDown;
        //public event TouchEventHandler TouchUp;

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


        //private void RaiseFingerAppearsEvent(int xOnScreen, int yOnScreen)
        //{
        //    if (FingerAppears != null)
        //    {
        //        XFingerOnScreen = xOnScreen;
        //        YFingerOnScreen = yOnScreen;
        //        FingerAppears(this, new FingerPresentEventArgs(xOnScreen, yOnScreen));
        //    }
        //}

        //private void RaiseFingerMovesEvent(int xOnScreen, int yOnScreen)
        //{
        //    if (FingerMoves != null)
        //    {
        //        XFingerOnScreen = xOnScreen;
        //        YFingerOnScreen = yOnScreen;
        //        FingerMoves(this, new FingerPresentEventArgs(xOnScreen, yOnScreen));
        //    }
        //}

        //private void RaiseFingerRemovedEvent()
        //{
        //    if (FingerRemoved != null)
        //    {
        //        XFingerOnScreen = 0;
        //        YFingerOnScreen = 0;
        //        FingerRemoved(this, new FingerRemovedEventArgs());
        //    }
        //}

        //private void RaiseTouchDown(TouchEventArgs e)
        //{
        //    if (TouchDown != null)
        //        TouchDown(this, e);
        //}

        //private void RaiseTouchUp(TouchEventArgs e)
        //{
        //    if (TouchUp != null)
        //        TouchUp(this, e);
        //}


        private void OnHandPresent(object sender, HandEventArgs e)
        {
            RaiseHandPresentEvent(e);
        }

        private void OnHandRemoved(object sender, HandEventArgs e)
        {
            RaiseHandRemovedEvent(e);
        }

        //private void OnFingerAppears(object sender, FingerPresentEventArgs e)
        //{
        //    RaiseFingerAppearsEvent(e.XOnScreen, e.YOnScreen);
        //}

        //private void OnFingerMoves(object sender, FingerPresentEventArgs e)
        //{
        //    RaiseFingerMovesEvent(e.XOnScreen, e.YOnScreen);
        //}

        //private void OnFingerRemoved(object sender, FingerRemovedEventArgs e)
        //{
        //    RaiseFingerRemovedEvent();
        //}

        //private void OnTouchDown(object sender, TouchEventArgs e)
        //{
        //    RaiseTouchDown(e);
        //}

        //private void OnTouchUp(object sender, TouchEventArgs e)
        //{
        //    RaiseTouchUp(e);
        //}

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
