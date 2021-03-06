﻿namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-attachment
    /// </summary>
    public sealed class CSSBackgroundAttachmentProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BackgroundAttachment> _modes = new Dictionary<String, BackgroundAttachment>(StringComparer.OrdinalIgnoreCase);
        List<BackgroundAttachment> _attachments;

        #endregion

        #region ctor

        static CSSBackgroundAttachmentProperty()
        {
            _modes.Add("fixed", BackgroundAttachment.Fixed);
            _modes.Add("local", BackgroundAttachment.Local);
            _modes.Add("scroll", BackgroundAttachment.Scroll);
        }

        internal CSSBackgroundAttachmentProperty()
            : base(PropertyNames.BackgroundAttachment)
        {
            _attachments = new List<BackgroundAttachment>();
            _attachments.Add(BackgroundAttachment.Scroll);
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration with the desired attachment settings.
        /// </summary>
        public IEnumerable<BackgroundAttachment> Attachments
        {
            get { return _attachments; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var list = value as CSSValueList ?? new CSSValueList(value);
            var attachments = new List<BackgroundAttachment>();

            for (int i = 0; i < list.Length; i++)
            {
                BackgroundAttachment attachment;

                if (list[i] is CSSIdentifierValue && _modes.TryGetValue(((CSSIdentifierValue)list[i]).Value, out attachment))
                    attachments.Add(attachment);
                else
                    return false;

                if (++i < list.Length && list[i] != CSSValue.Separator)
                    return false;
            }

            _attachments = attachments;
            return true;
        }

        #endregion
    }
}
