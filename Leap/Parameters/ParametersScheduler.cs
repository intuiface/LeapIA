            // ****************************************************************************
            // <copyright file="ParametersScheduler.cs" company="IntuiLab">
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
using IntuiLab.Leap.Exceptions;

namespace IntuiLab.Leap
{
    /// <summary>
    /// This class contains the configurable parameters for the Scheduler
    /// </summary>
    public class ParametersScheduler
    {
        #region Singleton Pattern

        private static volatile ParametersScheduler m_Instance = null;
        private static object syncRoot = new Object();

        /// <summary>
        /// Singleton
        /// </summary>
        public static ParametersScheduler Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (m_Instance == null)
                        {
                            m_Instance = new ParametersScheduler();
                        }
                    }
                }
                return m_Instance;
            }
        }

        public ParametersScheduler()
        {
            if (m_Instance == null)
            {
                m_Instance = this;
                InitializeParameters();
            }
            else
            {
                throw new LeapException("ParametersScheduler is already instancied");
            }
        }

        #endregion

        #region Constants

        const long DEFAULT_TIME_BETWEEN_TWO_FRAMES = 30000; // 30 ms
        const int DEFAULT_TIMES_UP_TIMER_INTERVAL = 700; // ms
        const int DEFAULT_DELAY_TIMER_INTERVAL = 30; // ms
        const int DEFALUT_MAX_DELAY_TIMER_COUNTER = 5;
        const int DEFAULT_POSTURE_LOST_TIMER_INTERVAL = 100; // ms

        #endregion

        #region Scheduler Parameters

        public long MinTimeAfterPostureDetections { get; set; }
        public int NbEventsForRockPostureToBeDetected { get; set; }
        public int NbEventsForScissorsPostureToBeDetected { get; set; }
        public int NbEventsForPaperPostureToBeDetected { get; set; }
        public int NbEventsForZeroPostureToBeDetected { get; set; }
        public int NbEventsForOnePostureToBeDetected { get; set; }
        public int NbEventsForTwoPostureToBeDetected { get; set; }
        public int NbEventsForThreePostureToBeDetected { get; set; }
        public int NbEventsForFourPostureToBeDetected { get; set; }
        public int NbEventsForFivePostureToBeDetected { get; set; }

        public readonly long TimeBetweenTwoFrames = DEFAULT_TIME_BETWEEN_TWO_FRAMES;
        public readonly int TimesUpTimerInterval = DEFAULT_TIMES_UP_TIMER_INTERVAL;
        public readonly int DelayTimerInterval = DEFAULT_DELAY_TIMER_INTERVAL;
        public readonly int MaxDelayTimerCounter = DEFALUT_MAX_DELAY_TIMER_COUNTER;
        public readonly int PostureLostTimerInterval = DEFAULT_POSTURE_LOST_TIMER_INTERVAL;
        
        #endregion

        #region Initialization

        private void InitializeParameters()
        {
            // Scheduler parameters
            MinTimeAfterPostureDetections = 2000000; // 2s
            NbEventsForRockPostureToBeDetected = 100;
            NbEventsForScissorsPostureToBeDetected = 100;
            NbEventsForPaperPostureToBeDetected = 100;
            NbEventsForZeroPostureToBeDetected = 100;
            NbEventsForOnePostureToBeDetected = 100;
            NbEventsForTwoPostureToBeDetected = 100;
            NbEventsForThreePostureToBeDetected = 100;
            NbEventsForFourPostureToBeDetected = 100;
            NbEventsForFivePostureToBeDetected = 100;
        }

        #endregion
    }
}
