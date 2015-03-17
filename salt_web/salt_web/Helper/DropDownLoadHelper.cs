using salt_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace salt_web.Helper
{
    //collection
    public static class RecoveryTypesDropDowns
    {
        public static IEnumerable<SelectListItem> RecoveryTypes { get; set; }
    }

    public class RecoveryTypesModel
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }

    //Billing Information
    public static class BillingInformationDropDowns
    {
        public static IEnumerable<SelectListItem> VendorNames { get; set; }
        public static IEnumerable<SelectListItem> State { get; set; }
    }

    public static class MenuCollectionNoteDropDowns
    {
        public static IEnumerable<SelectListItem> MenuCollectionNote { get; set; }
    }

    public static class UserDropDowns
    {
        public static IEnumerable<SelectListItem> Group { get; set; }
    }

    public static class EmployeeDropDowns
    {
        public static IEnumerable<SelectListItem> Manager { get; set; }
        public static IEnumerable<SelectListItem> Trainer { get; set; }
    }

    public static class ClientDropDowns
    {
        public static IEnumerable<SelectListItem> TypeOfContract { get; set; }
        public static IEnumerable<SelectListItem> PrimarySalesStaff { get; set; }
        public static IEnumerable<SelectListItem> State { get; set; }
    }

    public static class JobTrackingDropDowns
    {
        public static IEnumerable<SelectListItem> AuditorsKey { get; set; }
        public static IEnumerable<SelectListItem> ContractStatus { get; set; }
        public static IEnumerable<SelectListItem> PrimarySalesStaff { get; set; }
        public static IEnumerable<SelectListItem> AccountRep { get; set; }
        public static IEnumerable<SelectListItem> Confirmed { get; set; }
        public static IEnumerable<SelectListItem> Assigned { get; set; }
        public static IEnumerable<SelectListItem> Travel { get; set; }
        public static IEnumerable<SelectListItem> Rental { get; set; }
    }


    public class DropDownLoadHelper : Controller
    {
        public void DropDownLoadRecoveryType()
        {
            using (var context = new salt_entities())
            {
                var list = context.MenuRecoveryTypes.OrderBy(b => b.Sort).ToList().Select(e => new { e.PKID, e.RecoveryType });
                IEnumerable<SelectListItem> lists = list.Select(b => new SelectListItem { Value = b.PKID.ToString().Trim(), Text = b.RecoveryType.Trim() }).ToList();
                //IEnumerable<RecoveryTypesModel> lists = list.Select(b => new RecoveryTypesModel { Id = b.PKID.ToString().Trim(),  Type = b.RecoveryType.Trim() }).ToList();

                RecoveryTypesDropDowns.RecoveryTypes = lists;
            }
        }  

        public void DropDownLoadMenuCollectionNote()
        {
            using (var context = new salt_entities())
            {
                var list = context.MenuCollectionNotes.OrderBy(b => b.CollectionNoteName).ToList().Select(e => new { e.CollectionNotePKID, e.CollectionNoteName });
                IEnumerable<SelectListItem> lists = list.Select(b => new SelectListItem { Value = b.CollectionNotePKID.ToString(), Text = b.CollectionNoteName }).ToList();
                MenuCollectionNoteDropDowns.MenuCollectionNote = lists;
            }
        }

        public void DropDownLoadUser()
        {
            using (var context = new salt_entities())
            {
                var roleList = context.aspnet_Roles.OrderBy(b => b.RoleName).ToList().Select(e => new { e.RoleId, e.RoleName });
                IEnumerable<SelectListItem> roles = roleList.Select(b => new SelectListItem { Value = b.RoleId.ToString(), Text = b.RoleName }).ToList();
                UserDropDowns.Group = roles;                
            }
        }

        public void DropDownLoadEmployee()
        {             
            using (var context = new salt_entities())
            {
                var managerList = context.aspnet_Users.Where(a => a.TypeManager == true).OrderBy(b => b.MenuAdminPosit).ThenBy(c => c.LastName).ThenBy(d => d.FirstName).ToList().Select(e => new { e.UserId, LastFirst = e.LastName + " " + e.FirstName });
                IEnumerable<SelectListItem> manager = managerList.Select(b => new SelectListItem { Value = b.UserId.ToString(), Text = b.LastFirst }).ToList();

                var trainerList = context.aspnet_Users.Where(a => a.TypeTrainer == true).OrderBy(b => b.MenuAdminPosit).ThenBy(c => c.LastName).ThenBy(d => d.FirstName).ToList().Select(e => new { e.UserId, LastFirst = e.LastName + " " + e.FirstName });
                IEnumerable<SelectListItem> trainer = trainerList.Select(b => new SelectListItem { Value = b.UserId.ToString(), Text = b.LastFirst }).ToList();

                EmployeeDropDowns.Manager = manager;
                EmployeeDropDowns.Trainer = trainer;
            } 
        }

        public void DropDownLoadClient()
        {
            using (var context = new salt_entities())
            {
                IEnumerable<SelectListItem> contractType = context.MenuContractTypes
                    .Select(b => new SelectListItem { Value = b.Code, Text = b.TypeOfContract }).ToList();
                var userNameFiltered = context.aspnet_Users.Where(x => x.FirstName != null && x.LastName != null).ToList();
                IEnumerable<SelectListItem> userName = userNameFiltered.Select(b => new SelectListItem { Value = b.UserId.ToString(), Text = b.LastName + " " + b.FirstName }).ToList();
                IEnumerable<SelectListItem> stateName = context.MenuUSStates.Select(b => new SelectListItem { Value = b.Abbrev.ToString(), Text = b.FullName }).ToList();

                ClientDropDowns.TypeOfContract = contractType;
                ClientDropDowns.PrimarySalesStaff = userName;
                ClientDropDowns.State = stateName;
            }
        }

        public void DropDownLoadJobTracking()
        {
            using (var context = new salt_entities())
            {
                IEnumerable<SelectListItem> contractStatus = context.MenuContractStatus.Select(b => new SelectListItem { Value = b.ContractStatusID.ToString(), Text = b.ContractStatusDesc }).ToList();
                //TypeAuditor
                var userNameFiltered = context.aspnet_Users.Where(x => x.FirstName != null && x.LastName != null).ToList().Where(a => a.TypeAuditor == true).OrderBy(x => x.MenuAdminPosit).ThenBy(y => y.LastName).ThenBy(z => z.FirstName);
                IEnumerable<SelectListItem> auditorsKey = userNameFiltered.Select(b => new SelectListItem { Value = b.UserId.ToString(), Text = b.LastName + " " + b.FirstName }).ToList();

                //typeBDR
                var primarySalesStaffList = context.aspnet_Users.Where(x => x.FirstName != null && x.LastName != null).ToList().Where(a => a.TypeBDR == true).OrderBy(x => x.MenuAdminPosit).ThenBy(y => y.LastName).ThenBy(z => z.FirstName);
                IEnumerable<SelectListItem> primarySalesStaff = primarySalesStaffList.Select(b => new SelectListItem { Value = b.UserId.ToString(), Text = b.LastName + " " + b.FirstName }).ToList();

                //accRep
                var accountRepList = context.aspnet_Users.Where(x => x.FirstName != null && x.LastName != null).ToList().Where(a => a.TypeAcctRep == true).OrderBy(x => x.MenuAdminPosit).ThenBy(y => y.LastName).ThenBy(z => z.FirstName);
                IEnumerable<SelectListItem> accountRep = accountRepList.Select(b => new SelectListItem { Value = b.UserId.ToString(), Text = b.LastName + " " + b.FirstName }).ToList();

                //Confirmed
                var confirmedList = context.MenuContractConfirms.Where(x => x.ContractConfirmID != null).ToList();
                IEnumerable<SelectListItem> confirmed = confirmedList.Select(b => new SelectListItem { Value = b.ContractConfirmID.ToString(), Text = b.ContractConfirmDesc }).ToList();

                //assigned:
                var assignedList = context.MenuAssigneds.Where(x => x.AssignedID != null).ToList();
                IEnumerable<SelectListItem> assigned = assignedList.Select(b => new SelectListItem { Value = b.AssignedID.ToString(), Text = b.AssignedDesc }).ToList();

                //menutravel
                var travelList = context.MenuTravels.OrderBy(x => x.TravelID).ToList();
                IEnumerable<SelectListItem> travel = travelList.Select(b => new SelectListItem { Value = b.TravelID.ToString(), Text = b.TravelDesc }).ToList();

                //menuRental
                var rentalList = context.MenuRentals.OrderBy(x => x.RentalID).ToList();
                IEnumerable<SelectListItem> rental = rentalList.Select(b => new SelectListItem { Value = b.RentalID.ToString(), Text = b.RentalDesc }).ToList();

                JobTrackingDropDowns.AuditorsKey = auditorsKey;
                JobTrackingDropDowns.ContractStatus = contractStatus;
                JobTrackingDropDowns.PrimarySalesStaff = primarySalesStaff;
                JobTrackingDropDowns.AccountRep = accountRep;
                JobTrackingDropDowns.Confirmed = confirmed;
                JobTrackingDropDowns.Assigned = assigned;
                JobTrackingDropDowns.Travel = travel;
                JobTrackingDropDowns.Rental = rental;
            }
        }

        public void DropDownLoadBillingInformation()
        {
            using (var context = new salt_entities())
            {
                IEnumerable<SelectListItem> vendorList = context.Vendors
                    .Select(b => new SelectListItem { Value = b.VendorPKID.ToString(), Text = b.CompanyLegalName })
                    .Where(x => x.Text.Trim() != string.Empty).OrderBy(x => x.Text).ToList();

                IEnumerable<SelectListItem> stateName = context.MenuUSStates.Select(b => new SelectListItem { Value = b.PKID.ToString(), Text = b.FullName }).OrderBy(x => x.Text).ToList();
                BillingInformationDropDowns.VendorNames = vendorList;
                BillingInformationDropDowns.State = stateName;
            }
        }
    }
}