﻿//-----------------------------------------------------------------------
// <copyright file="AffixService.cs" company="Wohs">
//     Missing Copyright information from a valid stylecop.json file.
// </copyright>
//-----------------------------------------------------------------------

using Lurker.Models.Items;
using Lurker.Models.TradeAPI;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lurker.Services
{
    public class AffixService
    {
        #region Fields
        
        public static readonly string ImplicitMarker = " (implicit)";
        public static readonly string CraftedMarker = " (crafted)";
        private IEnumerable<AffixEntry> _affixes;
        private static readonly TradeApiClient Client = new TradeApiClient();
        private static AffixService _instance;
        private static readonly string MaximumLifeText = "# to maximum Life";
        private static readonly string StrengthAffixId = "explicit.stat_4080418644";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AffixService"/> class.
        /// </summary>
        /// <param name="affixes">The affixes.</param>
        private AffixService(IEnumerable<AffixEntry> affixes)
        {
            this._affixes = affixes;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the explicits.
        /// </summary>
        private IEnumerable<AffixEntry> Explicits => this._affixes.Where(a => a.Type == AffixType.Explicit);

        /// <summary>
        /// Gets the implicits.
        /// </summary>
        private IEnumerable<AffixEntry> Implicits => this._affixes.Where(a => a.Type == AffixType.Implicit);

        /// <summary>
        /// Gets the pseudos.
        /// </summary>
        private IEnumerable<AffixEntry> Pseudos => this._affixes.Where(a => a.Type == AffixType.Pseudo);

        /// <summary>
        /// Gets the crafted.
        /// </summary>
        private IEnumerable<AffixEntry> Crafted => this._affixes.Where(a => a.Type == AffixType.Crafted);

        #endregion

        #region Methods

        /// <summary>
        /// Gets the total life.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The total life of the item.</returns>
        public static double GetTotalLife(PoeItem item)
        {
            double increasedLifeCount = 0;
            var strAffix = item.Affixes.FirstOrDefault(a => a.Id == StrengthAffixId);

            if(strAffix != null)
            {
                var strBonus = (int)(strAffix.Value / 10);
                increasedLifeCount = strBonus * 5;
            }

            var instance = GetInstance();
            var maximumLifeAffixes = instance._affixes.Where(a => a.text == MaximumLifeText);

            foreach (var maximumLifeAffix in maximumLifeAffixes)
            {
                var affix = item.Affixes.FirstOrDefault(a => a.Id == maximumLifeAffix.Id);
                if (affix != null)
                {
                    increasedLifeCount += affix.Value;
                }
            }

            return increasedLifeCount;
        }

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <returns>The affix service.</returns>
        public static async Task InitializeAsync()
        {
            if (_instance == null)
            {
                var affixes = await Client.GetAffixes();
                _instance = new AffixService(affixes);
            }
        }

        /// <summary>
        /// Finds the explicit identifier.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The explicit id.</returns>
        public static string FindExplicitId(string text)
        {
            var instance = GetInstance();
            var affix = instance.Explicits.FirstOrDefault(e => e.text == text);
            if (affix == null)
            {
                return null;
            }

            return affix.Id;
        }

        /// <summary>
        /// Finds the crafted identifier.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The affix id.</returns>
        public static string FindCraftedId(string text)
        {
            var newText = text.Replace(CraftedMarker, string.Empty);
            var instance = GetInstance();
            var affix = instance.Crafted.FirstOrDefault(e => e.text == newText);
            if (affix == null)
            {
                return null;
            }

            return affix.Id;
        }

        /// <summary>
        /// Finds the implicit identifier.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The implicit id</returns>
        public static string FindImplicitId(string text)
        {
            var newText = text.Replace(ImplicitMarker, string.Empty);
            var instance = GetInstance();
            var affix = instance.Implicits.FirstOrDefault(e => e.text == newText);
            if (affix == null)
            {
                return null;
            }

            return affix.Id;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>The singleton.</returns>
        private static AffixService GetInstance()
        {
            if (_instance == null)
            {
                throw new System.InvalidOperationException("Needs to be Initialize first");
            }

            return _instance;
        }

        #endregion
    }
}
