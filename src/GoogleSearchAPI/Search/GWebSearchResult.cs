﻿/**
 * GWebSearchResult.cs
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

using Newtonsoft.Json;

namespace Google.API.Search
{
    [JsonObject]
    internal class GWebSearchResult : IWebSearchResult
    {
        private string m_PlaneTitle;
        private string m_PlaneContent;

        /// <summary>
        /// Indicates the "type" of result.
        /// </summary>
        [JsonProperty("GsearchResultClass")]
        public string GSearchResultClass { get; private set; }

        /// <summary>
        /// Supplies the raw URL of the result.
        /// </summary>
        [JsonProperty("unescapedUrl")]
        public string UnescapedUrl { get; private set; }

        /// <summary>
        /// Supplies an escaped version of the above URL.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; private set; }

        /// <summary>
        /// Supplies a shortened version of the URL associated with the result. Typically displayed in green, stripped of a protocol and path.
        /// </summary>
        [JsonProperty("visibleUrl")]
        public string VisibleUrl { get; private set; }

        /// <summary>
        /// Supplies a url to google's cached version of the page responsible for producting this result. This property may be null indicating that there is no cache, and it might be out of date in cases where the search result has been saved and in the mean time, the cache has gone stale. For best results, this property should not be persisted.
        /// </summary>
        [JsonProperty("cacheUrl")]
        public string CacheUrl { get; private set; }

        /// <summary>
        /// Supplies the title value of the result.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; private set; }

        /// <summary>
        /// Supplies the title, but unlike .title, this property is stripped of html markup (e.g., <b>, <i>, etc.)
        /// </summary>
        [JsonProperty("titleNoFormatting")]
        public string TitleNoFormatting { get; private set; }

        /// <summary>
        /// Supplies a brief snippet of information from the page associated with the search result.
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; private set; }

        public override string ToString()
        {
            IWebSearchResult result = this;
            return string.Format("[{0}] {1}", result.Title, result.Content);
        }

        #region IWebSearchResult Members

        string IWebSearchResult.Url
        {
            get { return Url; }
        }

        string IWebSearchResult.VisibleUrl
        {
            get { return VisibleUrl; }
        }

        string IWebSearchResult.CacheUrl
        {
            get { return CacheUrl; }
        }

        string IWebSearchResult.Title
        {
            get
            {
                if(m_PlaneTitle == null)
                {
                    m_PlaneTitle = HttpUtility.HtmlDecode(TitleNoFormatting);
                }
                return m_PlaneTitle;
            }
        }

        string IWebSearchResult.Content
        {
            get
            {
                if(m_PlaneContent == null)
                {
                    m_PlaneContent = HttpUtility.RemoveHtmlTags(Content);
                }
                return m_PlaneContent;
            }
        }

        #endregion
    }
}