﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-radius
    /// </summary>
    public sealed class CSSBorderRadiusProperty : CSSProperty
    {
        #region Fields

        CSSBorderBottomLeftRadiusProperty _bottomLeft;
        CSSBorderBottomRightRadiusProperty _bottomRight;
        CSSBorderTopLeftRadiusProperty _topLeft;
        CSSBorderTopRightRadiusProperty _topRight;

        #endregion

        #region ctor

        internal CSSBorderRadiusProperty()
            : base(PropertyNames.BorderRadius)
        {
            _inherited = false;
            _topRight = new CSSBorderTopRightRadiusProperty();
            _bottomRight = new CSSBorderBottomRightRadiusProperty();
            _bottomLeft = new CSSBorderBottomLeftRadiusProperty();
            _topLeft = new CSSBorderTopLeftRadiusProperty();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the bottom-left radius.
        /// </summary>
        public CSSBorderBottomLeftRadiusProperty BottomLeft
        {
            get { return _bottomLeft; }
        }

        /// <summary>
        /// Gets the value of the bottom-right radius.
        /// </summary>
        public CSSBorderBottomRightRadiusProperty BottomRight
        {
            get { return _bottomRight; }
        }

        /// <summary>
        /// Gets the value of the top-left radius.
        /// </summary>
        public CSSBorderTopLeftRadiusProperty TopLeft
        {
            get { return _topLeft; }
        }

        /// <summary>
        /// Gets the value of the top-right radius.
        /// </summary>
        public CSSBorderTopRightRadiusProperty TopRight
        {
            get { return _topRight; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;
            else if (value is CSSValueList)
                return Check((CSSValueList)value);

            return Check(new CSSValue[] { value, value, value, value });
        }

        Boolean Check(CSSValueList arguments)
        {
            var count = arguments.Length;
            var splitIndex = arguments.Length;

            for (var i = 0; i < splitIndex; i++)
                if (arguments[i] == CSSValue.Delimiter)
                    splitIndex = i;

            if (count - 1 > splitIndex + 4 || splitIndex > 4 || splitIndex == count - 1 || splitIndex == 0)
                return false;

            var values = new CSSValue[4];

            for (int i = 0; i < splitIndex; i++)
                for (int j = i; j < 4; j += i + 1)
                    values[j] = arguments[i];

            if (splitIndex != count)
            {
                var opt = new CSSValue[4];
                splitIndex++;
                count -= splitIndex;

                for (int i = 0; i < count; i++)
                    for (int j = i; j < 4; j += i + 1)
                        opt[j] = arguments[i + splitIndex];

                for (int i = 0; i < 4; i++)
                {
                    var list = new CSSValueList(values[i]);
                    list.Add(opt[i]);
                    values[i] = list;
                }
            }

            return Check(values);
        }

        Boolean Check(CSSValue[] values)
        {
            var target = new CSSProperty[] { new CSSBorderBottomLeftRadiusProperty(), new CSSBorderBottomRightRadiusProperty(), new CSSBorderTopLeftRadiusProperty(), new CSSBorderTopRightRadiusProperty() };

            for (int i = 0; i < 4; i++)
            {
                target[i].Value = values[i];

                if (target[i].Value != values[i])
                    return false;
            }

            _bottomLeft = (CSSBorderBottomLeftRadiusProperty)target[0];
            _bottomRight = (CSSBorderBottomRightRadiusProperty)target[1];
            _topLeft = (CSSBorderTopLeftRadiusProperty)target[2];
            _topRight = (CSSBorderTopRightRadiusProperty)target[3];
            return true;
        }

        #endregion
    }
}
