﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-width
    /// </summary>
    public sealed class CSSBorderImageWidthProperty : CSSProperty
    {
        #region Fields

        #endregion

        #region ctor

        internal CSSBorderImageWidthProperty()
            : base(PropertyNames.BorderImageWidth)
        {
            _inherited = false;
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}