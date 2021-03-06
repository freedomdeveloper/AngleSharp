﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-stretch
    /// </summary>
    public sealed class CSSFontStretchProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, FontStretch> _styles = new Dictionary<String, FontStretch>(StringComparer.OrdinalIgnoreCase);
        FontStretch _stretch;

        #endregion

        #region ctor

        static CSSFontStretchProperty()
        {
            _styles.Add("normal", FontStretch.Normal);
            _styles.Add("ultra-condensed", FontStretch.UltraCondensed);
            _styles.Add("extra-condensed", FontStretch.ExtraCondensed);
            _styles.Add("condensed", FontStretch.Condensed);
            _styles.Add("semi-condensed", FontStretch.SemiCondensed);
            _styles.Add("semi-expanded", FontStretch.SemiExpanded);
            _styles.Add("expanded", FontStretch.Expanded);
            _styles.Add("extra-expanded", FontStretch.ExtraExpanded);
            _styles.Add("ultra-expanded", FontStretch.UltraExpanded);
        }

        internal CSSFontStretchProperty()
            : base(PropertyNames.FontStretch)
        {
            _inherited = true;
            _stretch = FontStretch.Normal;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected font stretch setting.
        /// </summary>
        public FontStretch Stretch
        {
            get { return _stretch; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            FontStretch style;

            if (value is CSSIdentifierValue && _styles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                _stretch = style;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
