﻿//-----------------------------------------------------------------------
// <copyright file="Settings.cs" company="Wohs">
//     Missing Copyright information from a valid stylecop.json file.
// </copyright>
//-----------------------------------------------------------------------

namespace Lurker.Models
{
    using ConfOxide;

    public sealed class Settings: SettingsBase<Settings>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the busy message.
        /// </summary>
        public string BusyMessage { get; set; }

        /// <summary>
        /// Gets or sets the sold message.
        /// </summary>
        public string SoldMessage { get; set; }

        /// <summary>
        /// Gets or sets the thank you message.
        /// </summary>
        public string ThankYouMessage { get; set; }

        /// <summary>
        /// Gets or sets the sill interested message.
        /// </summary>
        public string StillInterestedMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [first launch].
        /// </summary>
        public bool FirstLaunch { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [search enabled].
        /// </summary>
        public bool SearchEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [alert enabled].
        /// </summary>
        public bool AlertEnabled { get; set; }

        /// <summary>
        /// Gets or sets the alert volume.
        /// </summary>
        public float AlertVolume { get; set; }

        #endregion
    }
}
