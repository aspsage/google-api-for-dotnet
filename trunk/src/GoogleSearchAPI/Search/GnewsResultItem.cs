﻿/**
 * GNewsResultItem.cs
 *
 * Copyright (C) 2008,  iron9light
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using Newtonsoft.Json;

namespace Google.API.Search
{
    [JsonObject]
    internal class GNewsResultItem : INewsResultItem
    {
        private string m_PlainTitle;
        private string m_PlainPublisher;
        private string m_PlainLocation;

        /// <summary>
        /// Supplies the title value of the result.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; protected set; }

        /// <summary>
        /// Supplies the title, but unlike .title, this property is stripped of html markup (e.g., &lt;b&gt;, &lt;i&gt;, etc.)
        /// </summary>
        [JsonProperty("titleNoFormatting")]
        public string TitleNoFormatting { get; protected set; }

        /// <summary>
        /// Supplies the raw URL of the result.
        /// </summary>
        [JsonProperty("unescapedUrl")]
        public string UnescapedUrl { get; protected set; }

        /// <summary>
        /// Supplies an escaped version of the above URL.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; protected set; }

        /// <summary>
        /// Supplies the name of the publisher of the news story.
        /// </summary>
        [JsonProperty("publisher")]
        public string Publisher { get; protected set; }

        /// <summary>
        /// Contains the location of the news story. This is a list of locations in most specific to least specific order where the components are seperated by ",". Note, there may only be one element in the list... A typical value for this property is "Edinburgh,Scotland,UK" or possibly "USA".
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; protected set; }

        /// <summary>
        /// Supplies the published date (rfc-822 format) of the news story referenced by this search result.
        /// </summary>
        [JsonProperty("publishedDate")]
        public DateTime PublishedDate { get; protected set; }

        public override string ToString()
        {
            INewsResultItem result = this;
            return string.Format("{0}" + Environment.NewLine + "[{1}, {2} - {3:d}]",
                                 result.Title,
                                 result.Publisher,
                                 result.Location,
                                 result.PublishedDate);
        }

        #region INewsResultItem Members

        string INewsResultItem.Url
        {
            get { return UnescapedUrl; }
        }

        string INewsResultItem.Title
        {
            get
            {
                if(TitleNoFormatting == null)
                {
                    return null;
                }

                if (m_PlainTitle == null)
                {
                    m_PlainTitle = HttpUtility.HtmlDecode(TitleNoFormatting);
                }
                return m_PlainTitle;
            }
        }

        string INewsResultItem.Publisher
        {
            get
            {
                if(Publisher == null)
                {
                    return null;
                }

                if(m_PlainPublisher == null)
                {
                    m_PlainPublisher = HttpUtility.HtmlDecode(Publisher);
                }
                return m_PlainPublisher;
            }
        }

        string INewsResultItem.Location
        {
            get
            {
                if(Location == null)
                {
                    return null;
                }

                if(m_PlainLocation == null)
                {
                    m_PlainLocation = HttpUtility.HtmlDecode(Location);
                }
                return m_PlainLocation;
            }
        }

        DateTime INewsResultItem.PublishedDate
        {
            get { return PublishedDate; }
        }

        #endregion
    }
}