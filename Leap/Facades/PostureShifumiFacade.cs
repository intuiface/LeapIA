using IntuiLab.Leap.Events;
using IntuiLab.Leap.Recognition.Postures;
using System;
using System.ComponentModel;

namespace IntuiLab.Leap
{
    /// <summary>
    /// A facade for the shifumi posture Enable/Disable properties and events
    /// </summary>
    public class PostureShifumiFacade : INotifyPropertyChanged, IDisposable
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

        #region Enable/Disable Properties

        /// <summary>
        /// Enable or disable the Rock posture
        /// </summary>
        private bool m_enableRockPosture;
        public bool EnableRockPosture
        {
            get
            {
                return m_enableRockPosture;
            }
            set
            {
                if (value && !m_enableRockPosture)
                {
                    LeapPlugin.Instance.LeapListener.EnablePostureRecognition(PostureType.Rock);
                    LeapPlugin.Instance.LeapListener.RockPostureSucceed += OnRockPostureSucceed;
                    LeapPlugin.Instance.LeapListener.RockPostureDetectionInProgress += OnRockPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.RockPostureDetectionLost += OnRockPostureDetectionLost;
                }
                else if (!value && m_enableRockPosture)
                {
                    LeapPlugin.Instance.LeapListener.DisablePostureRecognition(PostureType.Rock);
                    LeapPlugin.Instance.LeapListener.RockPostureSucceed -= OnRockPostureSucceed;
                    LeapPlugin.Instance.LeapListener.RockPostureDetectionInProgress -= OnRockPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.RockPostureDetectionLost -= OnRockPostureDetectionLost;
                }
                m_enableRockPosture = value;
                NotifyPropertyChanged("EnableRockPosture");
            }
        }

        /// <summary>
        /// Enable or disable the Scissors posture
        /// </summary>
        private bool m_enableScissorsPosture;
        public bool EnableScissorsPosture
        {
            get
            {
                return m_enableScissorsPosture;
            }
            set
            {
                if (value && !m_enableScissorsPosture)
                {
                    LeapPlugin.Instance.LeapListener.EnablePostureRecognition(PostureType.Scissors);
                    LeapPlugin.Instance.LeapListener.ScissorsPostureSucceed += OnScissorsPostureSucceed;
                    LeapPlugin.Instance.LeapListener.ScissorsPostureDetectionInProgress += OnScissorsPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.ScissorsPostureDetectionLost += OnScissorsPostureDetectionLost;
                }
                else if (!value && m_enableScissorsPosture)
                {
                    LeapPlugin.Instance.LeapListener.DisablePostureRecognition(PostureType.Scissors);
                    LeapPlugin.Instance.LeapListener.ScissorsPostureSucceed -= OnScissorsPostureSucceed;
                    LeapPlugin.Instance.LeapListener.ScissorsPostureDetectionInProgress -= OnScissorsPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.ScissorsPostureDetectionLost -= OnScissorsPostureDetectionLost;
                }
                m_enableScissorsPosture = value;
                NotifyPropertyChanged("EnableScissorsPosture");
            }
        }

        /// <summary>
        /// Enable or disable the Paper posture
        /// </summary>
        private bool m_enablePaperPosture;
        public bool EnablePaperPosture
        {
            get
            {
                return m_enablePaperPosture;
            }
            set
            {
                if (value && !m_enablePaperPosture)
                {
                    LeapPlugin.Instance.LeapListener.EnablePostureRecognition(PostureType.Paper);
                    LeapPlugin.Instance.LeapListener.PaperPostureSucceed += OnPaperPostureSucceed;
                    LeapPlugin.Instance.LeapListener.PaperPostureDetectionInProgress += OnPaperPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.PaperPostureDetectionLost += OnPaperPostureDetectionLost;
                }
                else if (!value && m_enablePaperPosture)
                {
                    LeapPlugin.Instance.LeapListener.DisablePostureRecognition(PostureType.Paper);
                    LeapPlugin.Instance.LeapListener.PaperPostureSucceed -= OnPaperPostureSucceed;
                    LeapPlugin.Instance.LeapListener.PaperPostureDetectionInProgress -= OnPaperPostureDetectionInProgress;
                    LeapPlugin.Instance.LeapListener.PaperPostureDetectionLost -= OnPaperPostureDetectionLost;
                }
                m_enablePaperPosture = value;
                NotifyPropertyChanged("EnablePaperPosture");
            }
        }

        #endregion

        #region Detection Time

        /// <summary>
        /// The detection time of the rock posture in milliseconds
        /// </summary>
        public int DetectionTimeForRockInMilliseconds
        {
            get
            {
                return ParametersScheduler.Instance.NbEventsForRockPostureToBeDetected * 10;
            }
            set
            {
                ParametersScheduler.Instance.NbEventsForRockPostureToBeDetected = value / 10;
                NotifyPropertyChanged("DetectionTimeForRockInMilliseconds");
            }
        }

        /// <summary>
        /// The detection time of the scissors posture in milliseconds
        /// </summary>
        public int DetectionTimeForScissorsInMilliseconds
        {
            get
            {
                return ParametersScheduler.Instance.NbEventsForScissorsPostureToBeDetected * 10;
            }
            set
            {
                ParametersScheduler.Instance.NbEventsForScissorsPostureToBeDetected = value / 10;
                NotifyPropertyChanged("DetectionTimeForScissorsInMilliseconds");
            }
        }

        /// <summary>
        /// The detection time of the paper posture in milliseconds
        /// </summary>
        public int DetectionTimeForPaperInMilliseconds
        {
            get
            {
                return ParametersScheduler.Instance.NbEventsForPaperPostureToBeDetected * 10;
            }
            set
            {
                ParametersScheduler.Instance.NbEventsForPaperPostureToBeDetected = value / 10;
                NotifyPropertyChanged("DetectionTimeForPaperInMilliseconds");
            }
        }

        #endregion

        #endregion

        #region Events

        public event PostureEventHandler RockPostureSucceed;
        public event PostureEventHandler RockPostureDetectionInProgress;
        public event PostureEventHandler RockPostureDetectionLost;

        public event PostureEventHandler PaperPostureSucceed;
        public event PostureEventHandler PaperPostureDetectionInProgress;
        public event PostureEventHandler PaperPostureDetectionLost;

        public event PostureEventHandler ScissorsPostureSucceed;
        public event PostureEventHandler ScissorsPostureDetectionInProgress;
        public event PostureEventHandler ScissorsPostureDetectionLost;

        private void RaiseRockPostureSucceed(PostureEventArgs e)
        {
            if (RockPostureSucceed != null)
            {
                RockPostureSucceed(this, e);
            }
        }
        private void RaiseRockPostureDetectionInProgress(PostureEventArgs e)
        {
            if (RockPostureDetectionInProgress != null)
            {
                RockPostureDetectionInProgress(this, e);
            }
        }
        private void RaiseRockPostureDetectionLost(PostureEventArgs e)
        {
            if (RockPostureDetectionLost != null)
                RockPostureDetectionLost(this, e);
        }


        private void RaisePaperPostureSucceed(PostureEventArgs e)
        {
            if (PaperPostureSucceed != null)
            {
                PaperPostureSucceed(this, e);
            }
        }
        private void RaisePaperPostureDetectionInProgress(PostureEventArgs e)
        {
            if (PaperPostureDetectionInProgress != null)
            {
                PaperPostureDetectionInProgress(this, e);
            }
        }
        private void RaisePaperPostureDetectionLost(PostureEventArgs e)
        {
            if (PaperPostureDetectionLost != null)
                PaperPostureDetectionLost(this, e);
        }


        private void RaiseScissorsPostureSucceed(PostureEventArgs e)
        {
            if (ScissorsPostureSucceed != null)
            {
                ScissorsPostureSucceed(this, e);
            }
        }
        private void RaiseScissorsPostureDetectionInProgress(PostureEventArgs e)
        {
            if (ScissorsPostureDetectionInProgress != null)
            {
                ScissorsPostureDetectionInProgress(this, e);
            }
        }
        private void RaiseScissorsPostureDetectionLost(PostureEventArgs e)
        {
            if (ScissorsPostureDetectionLost != null)
                ScissorsPostureDetectionLost(this, e);
        }

        private void OnRockPostureSucceed(object sender, PostureEventArgs e)
        {
            RaiseRockPostureSucceed(e);
        }
        private void OnRockPostureDetectionInProgress(object sender, PostureEventArgs e)
        {
            RaiseRockPostureDetectionInProgress(e);
        }
        private void OnRockPostureDetectionLost(object sender, PostureEventArgs e)
        {
            RaiseRockPostureDetectionLost(e);
        }


        private void OnPaperPostureSucceed(object sender, PostureEventArgs e)
        {
            RaisePaperPostureSucceed(e);
        }
        private void OnPaperPostureDetectionInProgress(object sender, PostureEventArgs e)
        {
            RaisePaperPostureDetectionInProgress(e);
        }
        private void OnPaperPostureDetectionLost(object sender, PostureEventArgs e)
        {
            RaisePaperPostureDetectionLost(e);
        }


        private void OnScissorsPostureSucceed(object sender, PostureEventArgs e)
        {
            RaiseScissorsPostureSucceed(e);
        }
        private void OnScissorsPostureDetectionInProgress(object sender, PostureEventArgs e)
        {
            RaiseScissorsPostureDetectionInProgress(e);
        }
        private void OnScissorsPostureDetectionLost(object sender, PostureEventArgs e)
        {
            RaiseScissorsPostureDetectionLost(e);
        }

        #endregion

        #region Constructor

        public PostureShifumiFacade()
        {
            // if the LeapPlugin is not already instantiated, we do it
            if (LeapPlugin.Instance == null)
            {
                LeapPlugin temp = new LeapPlugin();
            }

            this.m_enableRockPosture = false;
            this.m_enableScissorsPosture = false;
            this.m_enablePaperPosture = false;

            EnableRockPosture = true;
            EnableScissorsPosture = true;
            EnablePaperPosture = true;

            Main.RegisterFacade(this);
        }

        #endregion

        /// <summary>
        /// Clear the resources
        /// </summary>
        public void Dispose()
        {
            EnableRockPosture = false;
            EnableScissorsPosture = false;
            EnablePaperPosture = false;
        }
    }
}
