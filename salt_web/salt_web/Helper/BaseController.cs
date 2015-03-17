using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace salt_web.Helper
{
    ////public static class HelperExtension
    ////{
    ////    public static string ToString(this DateTime inputDate, bool showOnlyDate)
    ////    {
    ////        var resultDate = inputDate.ToString();

    ////        if (showOnlyDate)
    ////        {
    ////            if (inputDate == DateTime.MinValue)
    ////            {
    ////                resultDate = string.Empty;
    ////            }
    ////            else
    ////            {
    ////                resultDate = inputDate.ToString("MMM/dd/yyyy");
    ////            }
    ////        }
    ////        return resultDate;
    ////    }

    ////}

    public class BaseController : Controller
    {
        //https://stackoverflow.com/questions/1151987/can-i-set-an-unlimited-length-for-maxjsonlength-in-web-config
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

       
    }
}