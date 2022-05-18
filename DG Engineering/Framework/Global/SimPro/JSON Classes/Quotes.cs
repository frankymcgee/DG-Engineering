using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.SimPro
{
    internal class Quotes
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Customer
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("GivenName")]
        public string GivenName { get; set; }

        [JsonProperty("FamilyName")]
        public string FamilyName { get; set; }
    }

    public class CustomerContact
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("GivenName")]
        public string GivenName { get; set; }

        [JsonProperty("FamilyName")]
        public string FamilyName { get; set; }
    }

    public class Site
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }

    public class Forecast
    {
        [JsonProperty("Year")]
        public object Year { get; set; }

        [JsonProperty("Month")]
        public object Month { get; set; }

        [JsonProperty("Percent")]
        public double Percent { get; set; }
    }

    public class Total
    {
        [JsonProperty("ExTax")]
        public double ExTax { get; set; }

        [JsonProperty("Tax")]
        public double Tax { get; set; }

        [JsonProperty("IncTax")]
        public double IncTax { get; set; }

        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class MaterialsCost
    {
        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class Labor
    {
        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class LaborHours
    {
        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class PlantAndEquipment
    {
        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class PlantAndEquipmentHours
    {
        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class Overhead
    {
        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class ResourcesCost
    {
        [JsonProperty("Total")]
        public Total Total { get; set; }

        [JsonProperty("Labor")]
        public Labor Labor { get; set; }

        [JsonProperty("LaborHours")]
        public LaborHours LaborHours { get; set; }

        [JsonProperty("PlantAndEquipment")]
        public PlantAndEquipment PlantAndEquipment { get; set; }

        [JsonProperty("PlantAndEquipmentHours")]
        public PlantAndEquipmentHours PlantAndEquipmentHours { get; set; }

        [JsonProperty("Overhead")]
        public Overhead Overhead { get; set; }
    }

    public class MaterialsMarkup
    {
        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class ResourcesMarkup
    {
        [JsonProperty("Total")]
        public Total Total { get; set; }

        [JsonProperty("Labor")]
        public Labor Labor { get; set; }

        [JsonProperty("PlantAndEquipment")]
        public PlantAndEquipment PlantAndEquipment { get; set; }
    }

    public class Adjusted
    {
        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class GrossProfitLoss
    {
        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class GrossMargin
    {
        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class NettProfitLoss
    {
        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class NettMargin
    {
        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public double Revised { get; set; }

        [JsonProperty("Revized")]
        public double Revized { get; set; }
    }

    public class Totals
    {
        [JsonProperty("MaterialsCost")]
        public MaterialsCost MaterialsCost { get; set; }

        [JsonProperty("ResourcesCost")]
        public ResourcesCost ResourcesCost { get; set; }

        [JsonProperty("MaterialsMarkup")]
        public MaterialsMarkup MaterialsMarkup { get; set; }

        [JsonProperty("ResourcesMarkup")]
        public ResourcesMarkup ResourcesMarkup { get; set; }

        [JsonProperty("Adjusted")]
        public Adjusted Adjusted { get; set; }

        [JsonProperty("MembershipDiscount")]
        public double MembershipDiscount { get; set; }

        [JsonProperty("Discount")]
        public double Discount { get; set; }

        [JsonProperty("STCs")]
        public double STCs { get; set; }

        [JsonProperty("VEECs")]
        public double VEECs { get; set; }

        [JsonProperty("GrossProfitLoss")]
        public GrossProfitLoss GrossProfitLoss { get; set; }

        [JsonProperty("GrossMargin")]
        public GrossMargin GrossMargin { get; set; }

        [JsonProperty("NettProfitLoss")]
        public NettProfitLoss NettProfitLoss { get; set; }

        [JsonProperty("NettMargin")]
        public NettMargin NettMargin { get; set; }
    }

    public class Status
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }

    public class CustomField2
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("IsMandatory")]
        public bool IsMandatory { get; set; }

        [JsonProperty("ListItems")]
        public List<string> ListItems { get; set; }
    }

    public class CustomField
    {
        [JsonProperty("CustomField")]
        public CustomField Custom_Field { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }
    }

    public class STC
    {
        [JsonProperty("STCsEligible")]
        public bool STCsEligible { get; set; }

        [JsonProperty("VEECsEligible")]
        public bool VEECsEligible { get; set; }

        [JsonProperty("STCValue")]
        public double STCValue { get; set; }

        [JsonProperty("VEECValue")]
        public double VEECValue { get; set; }
    }

    public class Root
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Customer")]
        public Customer Customer { get; set; }

        [JsonProperty("CustomerContact")]
        public CustomerContact CustomerContact { get; set; }

        [JsonProperty("AdditionalContacts")]
        public List<object> AdditionalContacts { get; set; }

        [JsonProperty("Site")]
        public Site Site { get; set; }

        [JsonProperty("SiteContact")]
        public object SiteContact { get; set; }

        [JsonProperty("ConvertedFromLead")]
        public object ConvertedFromLead { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Notes")]
        public string Notes { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Salesperson")]
        public object Salesperson { get; set; }

        [JsonProperty("ProjectManager")]
        public object ProjectManager { get; set; }

        [JsonProperty("Technicians")]
        public List<object> Technicians { get; set; }

        [JsonProperty("Technician")]
        public object Technician { get; set; }

        [JsonProperty("DateIssued")]
        public string DateIssued { get; set; }

        [JsonProperty("DueDate")]
        public object DueDate { get; set; }

        [JsonProperty("ValidityDays")]
        public double ValidityDays { get; set; }

        [JsonProperty("OrderNo")]
        public string OrderNo { get; set; }

        [JsonProperty("RequestNo")]
        public string RequestNo { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("IsClosed")]
        public bool IsClosed { get; set; }

        [JsonProperty("Stage")]
        public string Stage { get; set; }

        [JsonProperty("CustomerStage")]
        public string CustomerStage { get; set; }

        [JsonProperty("JobNo")]
        public double JobNo { get; set; }

        [JsonProperty("IsVariation")]
        public bool IsVariation { get; set; }

        [JsonProperty("LinkedJobID")]
        public string LinkedJobID { get; set; }

        [JsonProperty("Forecast")]
        public Forecast Forecast { get; set; }

        [JsonProperty("Total")]
        public Total Total { get; set; }

        [JsonProperty("Totals")]
        public Totals Totals { get; set; }

        [JsonProperty("Status")]
        public Status Status { get; set; }

        [JsonProperty("Tags")]
        public List<object> Tags { get; set; }

        [JsonProperty("DateModified")]
        public DateTime DateModified { get; set; }

        [JsonProperty("AutoAdjustStatus")]
        public bool AutoAdjustStatus { get; set; }

        [JsonProperty("CustomFields")]
        public List<CustomField> CustomFields { get; set; }

        [JsonProperty("STC")]
        public STC STC { get; set; }

        [JsonProperty("ArchiveReason")]
        public object ArchiveReason { get; set; }
    }
    }
}
