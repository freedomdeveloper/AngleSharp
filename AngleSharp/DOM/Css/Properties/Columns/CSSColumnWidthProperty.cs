﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-width
    /// </summary>
    public sealed class CSSColumnWidthProperty : CSSProperty
    {
        #region Fields

        /// <summary>
        /// Null indicates that other properties (column-count) should be considered.
        /// </summary>
        Length? _width;

        #endregion

        #region ctor

        internal CSSColumnWidthProperty()
            : base(PropertyNames.ColumnWidth)
        {
            _width = null;
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the column width should be considered.
        /// </summary>
        public Boolean IsUsed
        {
            get { return _width.HasValue; }
        }

        /// <summary>
        /// Gets the width of a single columns.
        /// </summary>
        public Length Count
        {
            get { return _width.HasValue ? _width.Value : Length.Zero; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var width = value.ToLength();

            if (width.HasValue)
                _width = width.Value;
            else if (value.Is("auto"))
                _width = null;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
