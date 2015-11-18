            // ****************************************************************************
            // <copyright file="LeapListener.cs" company="IntuiLab">
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
using Leap;
using IntuiLab.Leap.Events;
using IntuiLab.Leap.Recognition;
using IntuiLab.Leap.DataStructures;
using IntuiLab.Leap.Pointing;
using IntuiLab.Leap.Recognition.Gestures;
using IntuiLab.Leap.Recognition.Postures;
using System.ComponentModel;

namespace IntuiLab.Leap
{
    /// <summary>
    /// This class is the main class which processes the data of each frame of the Leap controller
    /// </summary>
    public partial class LeapListener : Listener, INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The instance of the Leap controller
        /// </summary>
        private Controller m_Controller;
        
        /// <summary>
        /// For debug
        /// </summary>
        private bool m_VerboseMode;

        /// <summary>
        /// To know if this is already disposed
        /// </summary>
        private bool m_Disposed;

        /// <summary>
        /// To know if the Leap device is plugged
        /// </summary>
        private bool m_IsConnected;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged(String strInfo)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(strInfo));
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of the main class of the Leap Plugin
        /// </summary>
        /// <param name="usingLeapAPIForGesture">if we use the Leap API or the IntuiLab API for gestures</param>
        /// <param name="verboseMode">if we want the trace of the execution for debug</param>
        public LeapListener(bool usingLeapAPIForGesture, bool verboseMode, bool pointingWithOnlyPriorityFinger, bool usingLeapAPIForPointing)
        {
            this.m_Controller = new Controller(this);

            this.m_HandInPreviousFrame = false;
            this.m_VerboseMode = verboseMode;
            this.m_ReplayEnabled = false;

            this.m_Scheduler = new Scheduler(verboseMode);

            //if (pointingWithOnlyPriorityFinger && usingLeapAPIForPointing)
            //    m_FingerPointing = new LeapPriorityFingerPointing();
            //else if (pointingWithOnlyPriorityFinger && !usingLeapAPIForPointing)
            //    m_FingerPointing = new IntuiLabPriorityFingerPointing();
            //else if (!pointingWithOnlyPriorityFinger)
            //    m_FingerPointing = new LeapAllFingersPointing();


            if (usingLeapAPIForGesture)
                m_GestureAPI = new LeapGestureAPI(m_Controller, m_VerboseMode);
            else
                m_GestureAPI = new IntuiLabGestureAPI(m_Scheduler);

            Angle = 0;
            CsvRecordingActivated = false;
            this.m_CSVWriter = null;
            this.m_Disposed = false;
            this.m_IsConnected = true;
        }

        /// <summary>
        /// Default constructor for the Composer
        /// </summary>
        public LeapListener() : this(false, false, true, false)
        {
        }

        #endregion


        #region Dispose

        /// <summary>
        /// Frees the resources
        /// </summary>
        public override void Dispose()
        {
            if (!m_Disposed)
            {
                this.DisableGestureRecognition(GestureType.SwipeLeft);
                this.DisableGestureRecognition(GestureType.SwipeRight);
                this.DisableGestureRecognition(GestureType.Tap);
                this.DisableGestureRecognition(GestureType.Push);

                this.DisablePostureRecognition(PostureType.Rock);
                this.DisablePostureRecognition(PostureType.Scissors);
                this.DisablePostureRecognition(PostureType.Paper);
                this.DisablePostureRecognition(PostureType.Zero);
                this.DisablePostureRecognition(PostureType.One);
                this.DisablePostureRecognition(PostureType.Two);
                this.DisablePostureRecognition(PostureType.Three);
                this.DisablePostureRecognition(PostureType.Four);
                this.DisablePostureRecognition(PostureType.Five);

                //this.DisablePointing();

                m_Controller.RemoveListener(this);
                m_Controller.Dispose();
                m_Controller = null;

                base.Dispose();
                m_Disposed = true;
            }
        }

        #endregion

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern int MessageBox(IntPtr hWnd, String text, String caption, int options);

        public override void OnDisconnect(Controller controller)
        {
            if (m_IsConnected)
            {
                MessageBox(IntPtr.Zero, "Looks like your Leap device is unplugged. Be sure to plug it in before using any of IntuiFace’s Leap interfaces.",
                                                                        "Leap Device Missing", 0x001000);
                m_IsConnected = false;
            }
        }

        /// <summary>
        /// Method called each time a new frame from the device is available
        /// </summary>
        /// <param name="controller">The controller from which the frames come</param>
        public override void OnFrame(Controller controller)
        {
            // the entire body of this function is played only if the replay mode is not enable, to avoid that the frames from the device interfere with the replay
            if (!m_ReplayEnabled)
            {
                // first, we collect several data from the frames (fingers, hands ...)
                Frame frame = controller.Frame();
                Frame lastFrame = controller.Frame(1);
                HandList hands = frame.Hands;
                FingerList fingers = frame.Fingers;

                
                // if the CSV recording is activated, we store hands data and fingers data from this frame in files
                RecordData(frame, hands, fingers);
                
                // we test if the events HandPresent/HandLost have to be raised
                HandDetection(hands, fingers);

                // then, we test if there are fingers in the detection field
                //FingerDetection(frame, lastFrame, controller);

                // and to finish, we proceed to the gesture and posture detections
                GestureAndPostureDetection(frame);
            }
        }
    }
}
