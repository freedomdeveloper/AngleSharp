﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective
    /// </summary>
    public sealed class CSSPerspectiveProperty : CSSProperty
    {
        #region Fields

        Length _distance;

        #endregion

        #region ctor

        internal CSSPerspectiveProperty()
            : base(PropertyNames.Perspective)
        {
            _inherited = false;
            _distance = Length.Zero;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the distance from the user to the z=0 plane.
        /// It is used to apply a perspective transform to the element and
        /// its content. If it 0 or a negative value, no perspective transform
        /// is applied.
        /// </summary>
        public Length Distance
        {
            get { return _distance; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var distance = value.ToLength();

            if (distance.HasValue)
                _distance = distance.Value;
            //Is a keyword indicating that no perspective transform has to be applied.
            else if (value.Is("none"))
                _distance = Length.Zero;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
