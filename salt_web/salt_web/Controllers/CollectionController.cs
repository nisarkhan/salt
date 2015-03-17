using salt_web.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace salt_web.Controllers
{
    public class CollectionController : Controller
    {
        // GET: Collection
        public ActionResult Index(Guid clientId)
        {
            CollectionViewModel coll = new CollectionViewModel();

            string sql = string.Format(" select refund.pkid,vendors.VendorPKID as VendorPKID, vendors.companylegalname as 'Vendor', " +
                            " convert(nvarchar,coalesce(refund.amountidentified,0),1) as 'Amount Identified', " +
                            " convert(nvarchar,coalesce(refund.amountrecovered,0),1) as 'Amount Recovered', refund.recoverytype as Type, " +
                            " convert(nvarchar,coalesce(refund.amountidentified,0),1) as 'Amount Due', " +
                            " rtrim(vendors.contactfirstname)+' '+vendors.contactlastname as 'Contact', " +
                            " vendors.emailaddress as 'E-mail', vendors.phonenumber as 'Phone #', " +
                            " refund.recoveryType from salt.dbo.refundentry as refund join salt.dbo.vendors as vendors " +
                            " on refund.VendorPKID=vendors.VendorPKID  join salt.dbo.menuusstates as states  " +
                            " on refund.stateidentified=states.pkid join salt.dbo.MenuRecoveryType as mrTypes " +
                            " on mrTypes.pkid = refund.recoverytype where clientpkid=convert(uniqueidentifier,'{0}') " +
                            "  /*and refund.stateidentified=convert(uniqueidentifier,@state)*/ " +
                            " order by mrTypes.sort , " +
                            " vendors.companylegalname ", clientId);

            using (var context = new salt_entities())
            {
                var _data = context.Database.SqlQuery<CollectionViewModel>(sql);
                //return View(_data);    
            }

            return View(coll);
        }
    }
}