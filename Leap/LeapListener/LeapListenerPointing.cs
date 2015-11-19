using IntuiLab.Leap.Events;
using IntuiLab.Leap.Pointing;
using Leap;

namespace IntuiLab.Leap
{
    public partial class LeapListener
    {
        #region Fields

        /// <summary>
        /// For pointing
        /// </summary>
        private FingerPointing m_FingerPointing;

        #endregion

        #region Events

        /************ Events raised for pointing ****************************************************/

        /// <summary>
        /// Event raised when a finger appears
        /// </summary>
        public event FingerPresentEventHandler FingerAppears;

        /// <summary>
        /// Event raised when a finger is present (carry data of tip position)
        /// </summary>
        public event FingerPresentEventHandler FingerMoves;

        /// <summary>
        /// Event raised when a finger disappears
        /// </summary>
        public event FingerRemovedEventHandler FingerRemoved;

        /// <summary>
        /// Event raised when a touch down is detected
        /// </summary>
        public event TouchEventHandler TouchDown;

        /// <summary>
        /// Event raised when a touch up is detected
        /// </summary>
        public event TouchEventHandler TouchUp;


        private void RaiseFingerAppearsEvent(FingerPresentEventArgs e)
        {
            if (FingerAppears != null)
                FingerAppears(this, e);
        }

        private void RaiseFingerMovesEvent(FingerPresentEventArgs e)
        {
            if (FingerMoves != null)
                FingerMoves(this, e);
        }

        private void RaiseFingerRemovedEvent(FingerRemovedEventArgs e)
        {
            if (FingerRemoved != null)
                FingerRemoved(this, e);
        }

        private void RaiseTouchDownEvent(TouchEventArgs e)
        {
            if (TouchDown != null)
                TouchDown(this, e);
        }

        private void RaiseTouchUpEvent(TouchEventArgs e)
        {
            if (TouchUp != null)
                TouchUp(this, e);
        }

        /********************************************************************************************/

        #endregion

        #region Enable/Disable Pointing

        /// <summary>
        /// Enable pointing
        /// </summary>
        public void EnablePointing()
        {
            ParametersOther.Instance.FingerPointingEnable = true;
            this.m_FingerPointing.FingerAppears += OnFingerAppears;
            this.m_FingerPointing.FingerMoves += OnFingerMoves;
            this.m_FingerPointing.FingerRemoved += OnFingerRemoved;
            this.m_FingerPointing.TouchDown += OnTouchDown;
            this.m_FingerPointing.TouchUp += OnTouchUp;
        }

        /// <summary>
        /// Disable pointing
        /// </summary>
        public void DisablePointing()
        {
            ParametersOther.Instance.FingerPointingEnable = false;
            this.m_FingerPointing.FingerAppears -= OnFingerAppears;
            this.m_FingerPointing.FingerMoves -= OnFingerMoves;
            this.m_FingerPointing.FingerRemoved -= OnFingerRemoved;
            this.m_FingerPointing.TouchDown -= OnTouchDown;
            this.m_FingerPointing.TouchUp -= OnTouchUp;
        }

        #endregion

        private void FingerDetection(Frame frame, Frame lastFrame, Controller controller)
        {
            #region Finger Detection
            if (ParametersOther.Instance.FingerPointingEnable)
            {
                this.m_FingerPointing.FingerDetection(frame, lastFrame, controller);
            }
            #endregion
        }

        #region Callbacks for pointing

        private void OnFingerAppears(object sender, FingerPresentEventArgs e)
        {
            RaiseFingerAppearsEvent(e);
        }

        private void OnFingerMoves(object sender, FingerPresentEventArgs e)
        {
            RaiseFingerMovesEvent(e);
        }

        private void OnFingerRemoved(object sender, FingerRemovedEventArgs e)
        {
            RaiseFingerRemovedEvent(e);
        }

        private void OnTouchDown(object sender, TouchEventArgs e)
        {
            RaiseTouchDownEvent(e);
        }

        private void OnTouchUp(object sender, TouchEventArgs e)
        {
            RaiseTouchUpEvent(e);
        }

        #endregion
    }
}
