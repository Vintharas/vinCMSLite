
namespace vinCMS.Infraestructure
{
    public static class Constants
    {
       
        public const int CATEGORY_UNCATEGORIZED_ID = 1;
        

        public const string VIEW_TAGNAME = "tagName";
        public const string VIEW_CATEGORYNAME = "categoryName";
        public const string VIEW_TITLE = "title";
        public const string VIEW_CATEGORYLIST = "listOfCategories";
        public const string VIEW_TAGLIST = "listOfTags";
        public const string VIEW_MESSAGE = "message";
        public const string VIEW_MESSAGE_ERROR = "errorMessage";

        public const string MEDIA_PATH = "~/public/media/";

        /// <summary>
        /// enum that represents the mime types supported for media used by the CMS
        /// its values coincide with the id's of the MimeType table in the database
        /// </summary>
        public enum MimeTypes
        {
            Png = 1, Jpeg, Pdf
        }
    }
}