            // ****************************************************************************
            // <copyright file="ParametersPointing.cs" company="IntuiLab">
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
    /// This class contains the configurable parameters for pointing
    /// </summary>
    public class ParametersPointing
    {
        #region Singleton Pattern

        private static volatile ParametersPointing m_Instance = null;
        private static object syncRoot = new Object();

        /// <summary>
        /// Singleton
        /// </summary>
        public static ParametersPointing Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (m_Instance == null)
                        {
                            m_Instance = new ParametersPointing();
                        }
                    }
                }
                return m_Instance;
            }
        }

        public ParametersPointing()
        {
            if (m_Instance == null)
            {
                m_Instance = this;
                InitializeParameters();
            }
            else
            {
                throw new LeapException("ParametersPointing is already instancied");
            }
        }

        #endregion

        #region Pointing Parameters

        public float MinXOfVirtualScreen { get; set; }
        public float MaxXOfVirtualScreen { get; set; }
        public float MinYOfVirtualScreen { get; set; }
        public float MaxYOfVirtualScreen { get; set; }
        public float ZLimitForTouch { get; set; }

        #endregion

        #region Initialization

        private void InitializeParameters()
        {
            // Pointing parameters
            MinXOfVirtualScreen = -100; // mm
            MaxXOfVirtualScreen = 100; // mm
            MinYOfVirtualScreen = 100; // mm
            MaxYOfVirtualScreen = 200; // mm
            ZLimitForTouch = -80;
        }

        #endregion
    }
}
