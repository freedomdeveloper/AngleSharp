﻿namespace AngleSharp.DOM
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Bundles information stored in HTML forms.
    /// </summary>
    sealed class FormDataSet : IEnumerable<String>
    {
        #region Fields

        String _boundary;
        List<FormDataSetEntry> _entries;

        #endregion

        #region ctor

        public FormDataSet()
        {
            _boundary = String.Concat("<-----?", Guid.NewGuid().ToString(), "?----->");
            _entries = new List<FormDataSetEntry>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the chosen boundary.
        /// </summary>
        public String Boundary
        {
            get { return _boundary; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies the multipart/form-data algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#multipart/form-data-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns></returns>
        public Stream AsMultipart(Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var charset = encoding.WebName;
            var ms = new MemoryStream();

            foreach (var entry in _entries)
            {
                if (entry.Name.Equals("_charset_") && entry.Type.Equals("hidden", StringComparison.OrdinalIgnoreCase))
                    entry.Value = charset;

                //TODO Replace Characters in Name & Value that cannot be expressed by using current encoding with &#...; base-10 unicode point
                //RFC 2388
            }

            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// Applies the urlencoded algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#application/x-www-form-urlencoded-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns></returns>
        public Stream AsUrlEncoded(Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var charset = encoding.WebName;
            var ms = new MemoryStream();
            //TODO
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// Applies the plain encoding algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#text/plain-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns></returns>
        public Stream AsPlaintext(Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var charset = encoding.WebName;
            var ms = new MemoryStream();
            //TODO
            ms.Position = 0;
            return ms;
        }

        public void Append(String name, String value, String type)
        {
            if (String.Compare(type, "textarea", StringComparison.OrdinalIgnoreCase) == 0)
            {
                name = Normalize(name);
                value = Normalize(value);
            }

            CheckBoundary(value);
            _entries.Add(new FormDataSetEntry { Name = name, Value = value, Type = type });
        }

        public void Append(String name, FileEntry value, String type)
        {
            if (String.Compare(type, "file", StringComparison.OrdinalIgnoreCase) == 0)
                name = Normalize(name);

            CheckBoundary(value);
            _entries.Add(new FormDataSetEntry { Name = name, Value = value, Type = type });
        }

        #endregion

        #region Helpers

        void CheckBoundary(Object value)
        {
            //TODO
            //Check if there is any collision with the boundary string - if there is:
            //Re-Generate Boundary String until there are no collisions with any value
        }

        /// <summary>
        /// Replaces every occurrence of a "CR" (U+000D) character not followed by a "LF" (U+000A)
        /// character, and every occurrence of a "LF" (U+000A) character not preceded by a "CR"
        /// (U+000D) character, by a two-character string consisting of a U+000D CARRIAGE RETURN
        /// "CRLF" (U+000A) character pair.
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <returns>The normalized string.</returns>
        String Normalize(String value)
        {
            var lines = value.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            return String.Join("\r\n", lines);
        }

        #endregion

        #region Entry Class

        /// <summary>
        /// Encapsulates the data contained in an entry.
        /// </summary>
        sealed class FormDataSetEntry
        {
            /// <summary>
            /// Gets or sets the entry's name.
            /// </summary>
            public String Name
            {
                get;
                set;
            }

            /// <summary>
            /// Gets or sets the entry's type.
            /// </summary>
            public String Type
            {
                get;
                set;
            }

            /// <summary>
            /// Gets or sets the entry's value.
            /// </summary>
            public Object Value
            {
                get;
                set;
            }
        }

        #endregion

        #region IEnumerable Implementation

        /// <summary>
        /// Gets an enumerator over all entry names.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<String> GetEnumerator()
        {
            return _entries.Select(m => m.Name).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}