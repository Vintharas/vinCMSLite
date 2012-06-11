using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using System.Web.Routing;
using System.Text.RegularExpressions;

namespace vinCMS.Helpers.Routing
{
    public static class RoutingHelpers
    {

        /// <summary>
        /// Method that gets the necessary details to build a blog post url in the
        /// shape of a RouteValueDictionary
        /// </summary>
        /// <param name="blogPost"></param>
        /// <returns></returns>
        public static RouteValueDictionary GetBlogDetails(this BlogPost blogPost)
        {
            return new RouteValueDictionary
                       {
                           {"controller", "blog"},
                           {"action", "details"},
                           {"year", blogPost.PublishingDate.Year},
                           {"month", blogPost.PublishingDate.Month},
                           {"day", blogPost.PublishingDate.Day},
                           {"path", blogPost.Path}
                       };
        }

        /// <summary>
        /// Method that gets the necessary detail values to make an url for a blog post
        /// listing filtered by tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static RouteValueDictionary GetTagDetails(this Tag tag)
        {
            return new RouteValueDictionary()
                       {
                           {"controller", "blog"},
                           {"action", "tag"},
                           {"tagname", MakeSimpleUrlSegment(tag.Name)}
                       };
        }

        /// <summary>
        /// Method that gets the necessary detail values to make an url for a blog post
        /// listing filtered by category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static RouteValueDictionary GetCategoryDetails(this Category category)
        {
            return new RouteValueDictionary()
                       {
                           {"controller", "blog"},
                           {"action", "category"},
                           {"categoryname", MakeSimpleUrlSegment(category.Name)}
                       };
        }

        /// <summary>
        /// Method that gets the necessary detail values to make an url for a page
        /// website "page" :)
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static RouteValueDictionary GetPageDetails(this Page page)
        {
            return new RouteValueDictionary
                       {
                           {"controller", "page"},
                           {"action", "details"},
                           {"path", page.Path}
                       };
        }

        /// <summary>
        /// Method that makes a simple url segment from a former string url. Replaces the spaces with "-",
        /// eliminates strange characters and forces all characters to be lowercase
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string MakeSimpleUrlSegment(string url)
        {
            url = (url ?? string.Empty).Trim();
            url = url.Replace(" ", "-");
            return Regex.Replace(url, "^0-9a-z//-", string.Empty, RegexOptions.IgnoreCase).ToLower();
        }

        /// <summary>
        /// Method that recovers a url segment replacing back the "-" for blank spaces
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string RecoverOriginalUrlSegment(string url)
        {
            return url.Replace("-", " ");
        }

        /// <summary>
        /// Method that makes a simple file segment from a former filename url. Replaces the spaces with "-", 
        /// eliminates stranger characters and forces all characters to be lowercase
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string MakeSimpleFileSegment(string fileName)
        {
            fileName = (fileName ?? string.Empty).Trim();
            fileName = fileName.Replace(" ", "-");
            return Regex.Replace(fileName, "^0-9a-z//-//.", string.Empty, RegexOptions.IgnoreCase).ToLower();
        }
    }
}