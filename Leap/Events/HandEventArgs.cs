using System;

namespace IntuiLab.Leap.Events
{
    #region Delegate

    public delegate void HandEventHandler(object sender, HandEventArgs e);
    
    #endregion

    /// <summary>
    /// Event for the hand present or lost detection
    /// </summary>
    public class HandEventArgs : EventArgs
    {
    }
}
