﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/transition-delay
    /// </summary>
    public sealed class CSSTransitionDelayProperty : CSSProperty
    {
        #region Fields

        List<Time> _times;

        #endregion

        #region ctor

        internal CSSTransitionDelayProperty()
            : base(PropertyNames.TransitionDelay)
        {
            _inherited = false;
            _times = new List<Time>();
            _times.Add(Time.Zero);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the delays for the transitions.
        /// </summary>
        public IEnumerable<Time> Delays
        {
            get { return _times; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var values = value.AsList<CSSPrimitiveValue<Time>>();

            if (values != null)
            {
                _times.Clear();

                foreach (var v in values)
                    _times.Add(v.Value);
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
