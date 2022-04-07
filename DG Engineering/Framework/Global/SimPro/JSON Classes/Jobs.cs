using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.SimPro
{
    internal class Jobs
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

    public class AdditionalContact
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

    public class SiteContact
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("GivenName")]
        public string GivenName { get; set; }

        [JsonProperty("FamilyName")]
        public string FamilyName { get; set; }
    }

    public class Salesperson
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("TypeId")]
        public int TypeId { get; set; }
    }

    public class ProjectManager
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("TypeId")]
        public int TypeId { get; set; }
    }

    public class Status
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }

    public class ResponseTime
    {
    }

    public class Total
    {
        [JsonProperty("ExTax")]
        public double ExTax { get; set; }

        [JsonProperty("Tax")]
        public double Tax { get; set; }

        [JsonProperty("IncTax")]
        public double IncTax { get; set; }

        [JsonProperty("Actual")]
        public double Actual { get; set; }

        [JsonProperty("Committed")]
        public int Committed { get; set; }

        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
    }

    public class ConvertedFromQuote
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Total")]
        public Total Total { get; set; }
    }

    public class ConvertedFrom
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; }
    }

    public class MaterialsCost
    {
        [JsonProperty("Actual")]
        public double Actual { get; set; }

        [JsonProperty("Committed")]
        public int Committed { get; set; }

        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
    }

    public class Labor
    {
        [JsonProperty("Actual")]
        public double Actual { get; set; }

        [JsonProperty("Committed")]
        public int Committed { get; set; }

        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
    }

    public class LaborHours
    {
        [JsonProperty("Actual")]
        public double Actual { get; set; }

        [JsonProperty("Committed")]
        public int Committed { get; set; }

        [JsonProperty("Estimate")]
        public int Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
    }

    public class PlantAndEquipment
    {
        [JsonProperty("Actual")]
        public int Actual { get; set; }

        [JsonProperty("Committed")]
        public int Committed { get; set; }

        [JsonProperty("Estimate")]
        public int Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
    }

    public class PlantAndEquipmentHours
    {
        [JsonProperty("Actual")]
        public int Actual { get; set; }

        [JsonProperty("Estimate")]
        public int Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
    }

    public class Overhead
    {
        [JsonProperty("Actual")]
        public int Actual { get; set; }

        [JsonProperty("Committed")]
        public int Committed { get; set; }

        [JsonProperty("Estimate")]
        public int Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
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
        [JsonProperty("Actual")]
        public double Actual { get; set; }

        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
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
        [JsonProperty("Actual")]
        public int Actual { get; set; }

        [JsonProperty("Estimate")]
        public int Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
    }

    public class GrossProfitLoss
    {
        [JsonProperty("Actual")]
        public double Actual { get; set; }

        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
    }

    public class GrossMargin
    {
        [JsonProperty("Actual")]
        public double Actual { get; set; }

        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
    }

    public class NettProfitLoss
    {
        [JsonProperty("Actual")]
        public double Actual { get; set; }

        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
    }

    public class NettMargin
    {
        [JsonProperty("Actual")]
        public double Actual { get; set; }

        [JsonProperty("Estimate")]
        public double Estimate { get; set; }

        [JsonProperty("Revised")]
        public int Revised { get; set; }

        [JsonProperty("Revized")]
        public int Revized { get; set; }
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
        public int MembershipDiscount { get; set; }

        [JsonProperty("Discount")]
        public int Discount { get; set; }

        [JsonProperty("STCs")]
        public int STCs { get; set; }

        [JsonProperty("VEECs")]
        public int VEECs { get; set; }

        [JsonProperty("GrossProfitLoss")]
        public GrossProfitLoss GrossProfitLoss { get; set; }

        [JsonProperty("GrossMargin")]
        public GrossMargin GrossMargin { get; set; }

        [JsonProperty("NettProfitLoss")]
        public NettProfitLoss NettProfitLoss { get; set; }

        [JsonProperty("NettMargin")]
        public NettMargin NettMargin { get; set; }

        [JsonProperty("InvoicedValue")]
        public double InvoicedValue { get; set; }

        [JsonProperty("InvoicePercentage")]
        public double InvoicePercentage { get; set; }
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
        public CustomField Field { get; set; }

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
        public int STCValue { get; set; }

        [JsonProperty("VEECValue")]
        public int VEECValue { get; set; }
    }

    public class Root
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("Customer")]
        public Customer Customer { get; set; }

        [JsonProperty("CustomerContact")]
        public CustomerContact CustomerContact { get; set; }

        [JsonProperty("AdditionalContacts")]
        public List<AdditionalContact> AdditionalContacts { get; set; }

        [JsonProperty("Site")]
        public Site Site { get; set; }

        [JsonProperty("SiteContact")]
        public SiteContact SiteContact { get; set; }

        [JsonProperty("OrderNo")]
        public string OrderNo { get; set; }

        [JsonProperty("RequestNo")]
        public string RequestNo { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Notes")]
        public string Notes { get; set; }

        [JsonProperty("DateIssued")]
        public string DateIssued { get; set; }

        [JsonProperty("DueDate")]
        public object DueDate { get; set; }

        [JsonProperty("DueTime")]
        public object DueTime { get; set; }

        [JsonProperty("Tags")]
        public List<object> Tags { get; set; }

        [JsonProperty("Salesperson")]
        public Salesperson Salesperson { get; set; }

        [JsonProperty("ProjectManager")]
        public ProjectManager ProjectManager { get; set; }

        [JsonProperty("Technicians")]
        public List<object> Technicians { get; set; }

        [JsonProperty("Technician")]
        public object Technician { get; set; }

        [JsonProperty("Stage")]
        public string Stage { get; set; }

        [JsonProperty("Status")]
        public Status Status { get; set; }

        [JsonProperty("ResponseTime")]
        public ResponseTime ResponseTime { get; set; }

        [JsonProperty("IsVariation")]
        public bool IsVariation { get; set; }

        [JsonProperty("LinkedVariations")]
        public List<object> LinkedVariations { get; set; }

        [JsonProperty("ConvertedFromQuote")]
        public ConvertedFromQuote ConvertedFromQuote { get; set; }

        [JsonProperty("ConvertedFrom")]
        public ConvertedFrom ConvertedFrom { get; set; }

        [JsonProperty("DateModified")]
        public DateTime DateModified { get; set; }

        [JsonProperty("AutoAdjustStatus")]
        public bool AutoAdjustStatus { get; set; }

        [JsonProperty("Total")]
        public Total Total { get; set; }

        [JsonProperty("Totals")]
        public Totals Totals { get; set; }

        [JsonProperty("CustomFields")]
        public List<CustomField> CustomFields { get; set; }

        [JsonProperty("STC")]
        public STC STC { get; set; }

        [JsonProperty("CompletedDate")]
        public object CompletedDate { get; set; }

        [JsonProperty("IoTJobID")]
        public string IoTJobID { get; set; }
    }


    }
}
