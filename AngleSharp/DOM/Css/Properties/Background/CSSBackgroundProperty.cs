﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background
    /// </summary>
    public sealed class CSSBackgroundProperty : CSSProperty
    {
        #region Fields

        CSSBackgroundImageProperty _image;
        CSSBackgroundPositionProperty _position;
        CSSBackgroundSizeProperty _size;
        CSSBackgroundRepeatProperty _repeat;
        CSSBackgroundAttachmentProperty _attachment;
        CSSBackgroundOriginProperty _origin;
        CSSBackgroundClipProperty _clip;
        CSSBackgroundColorProperty _color;

        #endregion

        #region ctor

        internal CSSBackgroundProperty()
            : base(PropertyNames.Background)
        {
            _image = new CSSBackgroundImageProperty();
            _position = new CSSBackgroundPositionProperty();
            _size = new CSSBackgroundSizeProperty();
            _repeat = new CSSBackgroundRepeatProperty();
            _attachment = new CSSBackgroundAttachmentProperty();
            _origin = new CSSBackgroundOriginProperty();
            _clip = new CSSBackgroundClipProperty();
            _color = new CSSBackgroundColorProperty();
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the background image property.
        /// </summary>
        public CSSBackgroundImageProperty Image
        {
            get { return _image; }
        }

        /// <summary>
        /// Gets the value of the background position property.
        /// </summary>
        public CSSBackgroundPositionProperty Position
        {
            get { return _position; }
        }

        /// <summary>
        /// Gets the value of the background size property.
        /// </summary>
        public CSSBackgroundSizeProperty Size
        {
            get { return _size; }
        }

        /// <summary>
        /// Gets the value of the background repeat property.
        /// </summary>
        public CSSBackgroundRepeatProperty Repeat
        {
            get { return _repeat; }
        }

        /// <summary>
        /// Gets the value of the background attachment property.
        /// </summary>
        public CSSBackgroundAttachmentProperty Attachment
        {
            get { return _attachment; }
        }

        /// <summary>
        /// Gets the value of the background origin property.
        /// </summary>
        public CSSBackgroundOriginProperty Origin
        {
            get { return _origin; }
        }

        /// <summary>
        /// Gets the value of the background clip property.
        /// </summary>
        public CSSBackgroundClipProperty Clip
        {
            get { return _clip; }
        }

        /// <summary>
        /// Gets the value of the background color property.
        /// </summary>
        public CSSBackgroundColorProperty Color
        {
            get { return _color; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value != CSSValue.Inherit)
            {
                var values = value as CSSValueList ?? new CSSValueList(value);
                var image = new CSSValueList();
                var position = new CSSValueList();
                var size = new CSSValueList();
                var repeat = new CSSValueList();
                var attachment = new CSSValueList();
                var origin = new CSSValueList();
                var clip = new CSSValueList();
                var color = new CSSPrimitiveValue<Color>(AngleSharp.DOM.Color.Transparent);
                var list = values.ToList();

                for (int i = 0; i < list.Count; i++)
                {
                    var entry = list[i];
                    var hasImage = false;
                    var hasPosition = false;
                    var hasRepeat = false;
                    var hasAttachment = false;
                    var hasBox = false;
                    var hasColor = i + 1 != list.Count;

                    for (int j = 0; j < entry.Length; j++)
                    {
                        if (!hasPosition && (entry[j].IsOneOf("top", "left", "center", "bottom", "right") || entry[j].AsCalc() != null))
                        {
                            hasPosition = true;
                            position.Add(entry[j]);

                            while (j + 1 < entry.Length && (entry[j + 1].IsOneOf("top", "left", "center", "bottom", "right") || entry[j + 1].AsCalc() != null))
                                position.Add(entry[++j]);

                            if (j + 1 < entry.Length && entry[j + 1] == CSSValue.Delimiter)
                            {
                                j += 2;

                                if (j < entry.Length && (entry[j].IsOneOf("auto", "contain", "cover") || entry[j].AsCalc() != null))
                                {
                                    size.Add(entry[j]);

                                    if (j + 1 < entry.Length && (entry[j + 1].Is("auto") || entry[j + 1].AsCalc() != null))
                                        size.Add(entry[++j]);
                                }
                                else
                                    return false;
                            }
                            else
                                size.Add(new CSSIdentifierValue("auto"));

                            continue;
                        }

                        if (!hasImage && entry[j].AsImage() != null)
                        {
                            hasImage = true;
                            image.Add(entry[j]);
                        }
                        else if (!hasRepeat && entry[j].IsOneOf("repeat-x", "repeat-y", "repeat", "space", "round", "no-repeat"))
                        {
                            hasRepeat = true;
                            repeat.Add(entry[j]);

                            if (j + 1 < entry.Length && entry[j + 1].IsOneOf("repeat", "space", "round", "no-repeat"))
                                repeat.Add(entry[++j]);
                        }
                        else if (!hasAttachment && entry[j].IsOneOf("local", "fixed", "scroll"))
                        {
                            hasAttachment = true;
                            attachment.Add(entry[j]);
                        }
                        else if (!hasBox && entry[j].ToBoxModel().HasValue)
                        {
                            hasBox = true;
                            origin.Add(entry[j]);

                            if (j + 1 < entry.Length && entry[j + 1].ToBoxModel().HasValue)
                                clip.Add(entry[++j]);
                            else
                                clip.Add(new CSSIdentifierValue("border-box"));
                        }
                        else
                        {
                            if (hasColor)
                                return false;

                            hasColor = true;
                            color = entry[j].AsColor();

                            if (color == null)
                                return false;
                        }
                    }

                    if (!hasImage)
                        image.Add(new CSSIdentifierValue("none"));

                    if (!hasPosition)
                    {
                        position.Add(new CSSIdentifierValue("center"));
                        size.Add(new CSSIdentifierValue("auto"));
                    }

                    if (!hasRepeat)
                        repeat.Add(new CSSIdentifierValue("repeat"));

                    if (!hasAttachment)
                        attachment.Add(new CSSIdentifierValue("scroll"));

                    if (!hasBox)
                    {
                        origin.Add(new CSSIdentifierValue("border-box"));
                        clip.Add(new CSSIdentifierValue("border-box"));
                    }

                    if (i + 1 < list.Count)
                    {
                        image.Add(CSSValue.Separator);
                        position.Add(CSSValue.Separator);
                        size.Add(CSSValue.Separator);
                        repeat.Add(CSSValue.Separator);
                        attachment.Add(CSSValue.Separator);
                        origin.Add(CSSValue.Separator);
                        clip.Add(CSSValue.Separator);
                    }
                }

                _image.Value = image;
                _position.Value = position;
                _repeat.Value = repeat;
                _attachment.Value = attachment;
                _origin.Value = origin;
                _size.Value = size;
                _clip.Value = clip;
                _color.Value = color;
            }

            return true;
        }

        #endregion
    }
}
