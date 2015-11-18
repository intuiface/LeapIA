            // ****************************************************************************
            // <copyright file="LeapListenerRecording.cs" company="IntuiLab">
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
using IntuiLab.Leap.DataRecording;
using Leap;

namespace IntuiLab.Leap
{
    public partial class LeapListener
    {
        #region Fields

        /// <summary>
        /// To write the data in a CSV file
        /// </summary>
        private CSVWriter m_CSVWriter;

        #endregion

        #region Properties

        /// <summary>
        /// Permits to activate the data recording in CSV files
        /// </summary>
        private bool m_CsvRecordingActivated;
        public bool CsvRecordingActivated
        {
            get
            {
                return m_CsvRecordingActivated;
            }
            set
            {
                lock (this)
                {
                    m_CsvRecordingActivated = value;

                    if (value && m_CSVWriter == null)
                    {
                        m_CSVWriter = new CSVWriter(RecordFileName);
                    }
                    else if (!value && m_CSVWriter != null)
                    {
                        m_CSVWriter.Close();
                        m_CSVWriter = null;
                    }
                }
            }
        }

        /// <summary>
        /// The CSV file name for data recording
        /// </summary>
        private string m_RecordFileName;
        public string RecordFileName
        {
            get
            {
                return m_RecordFileName;
            }
            set
            {
                m_RecordFileName = value;
            }
        }

        #endregion

        private void RecordData(Frame frame, HandList hands, FingerList fingers)
        {
            if (CsvRecordingActivated)
            {
                lock (this)
                {
                    foreach (Hand hand in hands)
                    {
                        m_CSVWriter.RecordHand(frame.Id, hand.Id, hand.Fingers.Count, hand.PalmPosition, hand.PalmNormal, hand.PalmVelocity, hand.SphereCenter, hand.SphereRadius, frame.Timestamp);
                    }
                    foreach (Finger finger in fingers)
                    {
                        m_CSVWriter.RecordFinger(frame.Id, finger.Id, finger.Hand.Id, fingers.Frontmost.Id == finger.Id, finger.Direction, finger.TipPosition, finger.TipVelocity, finger.Length, finger.Width, frame.Timestamp);
                    }
                }
            }
        }
    }
}
