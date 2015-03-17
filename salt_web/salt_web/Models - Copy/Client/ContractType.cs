using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace salt_web.Models.ClientType
{
    public class ContractType
    {
        
        public IEnumerable<SelectListItem> ContractTypeModel { get; set; }

        public string Text { get; set; }
        public string Code { get; set; }
    }

    //public class ContractTypeModel
    //{

    //    public ContractTypeModel()
    //    {
    //        ContractTypeList = new List<SelectListItem>();
    //    }

    //    [DisplayName("Contract Type")]
    //    public List<SelectListItem> ContractTypeList
    //    {
    //        get;
    //        set;
    //    }


    //}
}