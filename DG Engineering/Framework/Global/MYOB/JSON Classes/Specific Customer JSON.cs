using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace DG_Engineering.Framework.Global.MYOB
{
    internal class Customer
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Address
        {
            [JsonProperty("Location")] public int Location { get; set; }

            [JsonProperty("Street")] public string Street { get; set; }

            [JsonProperty("City")] public string City { get; set; }

            [JsonProperty("State")] public string State { get; set; }

            [JsonProperty("PostCode")] public string PostCode { get; set; }

            [JsonProperty("Country")] public string Country { get; set; }

            [JsonProperty("Phone1")] public string Phone1 { get; set; }

            [JsonProperty("Phone2")] public string Phone2 { get; set; }

            [JsonProperty("Phone3")] public string Phone3 { get; set; }

            [JsonProperty("Fax")] public string Fax { get; set; }

            [JsonProperty("Email")] public string Email { get; set; }

            [JsonProperty("Website")] public string Website { get; set; }

            [JsonProperty("ContactName")] public string ContactName { get; set; }

            [JsonProperty("Salutation")] public string Salutation { get; set; }
        }

        public class Credit
        {
            [JsonProperty("Limit")] public double Limit { get; set; }

            [JsonProperty("Available")] public double Available { get; set; }

            [JsonProperty("PastDue")] public double PastDue { get; set; }

            [JsonProperty("OnHold")] public bool OnHold { get; set; }
        }

        public class CustomField1
        {
            [JsonProperty("Label")] public string Label { get; set; }

            [JsonProperty("Value")] public string Value { get; set; }
        }

        public class CustomField2
        {
            [JsonProperty("Label")] public string Label { get; set; }

            [JsonProperty("Value")] public string Value { get; set; }
        }

        public class CustomField3
        {
            [JsonProperty("Label")] public string Label { get; set; }

            [JsonProperty("Value")] public string Value { get; set; }
        }

        public class FreightTaxCode
        {
            [JsonProperty("UID")] public string Uid { get; set; }

            [JsonProperty("Code")] public string Code { get; set; }

            [JsonProperty("URI")] public string Uri { get; set; }
        }

        public class Item
        {
            [JsonProperty("UID")] public string Uid { get; set; }

            [JsonProperty("LastName")] public string LastName { get; set; }

            [JsonProperty("FirstName")] public string FirstName { get; set; }

            [JsonProperty("IsIndividual")] public bool IsIndividual { get; set; }

            [JsonProperty("DisplayID")] public string DisplayId { get; set; }

            [JsonProperty("IsActive")] public bool IsActive { get; set; }

            [JsonProperty("Addresses")] public List<Address> Addresses { get; set; }

            [JsonProperty("Notes")] public string Notes { get; set; }

            [JsonProperty("Identifiers")] public object Identifiers { get; set; }

            [JsonProperty("CustomList1")] public object CustomList1 { get; set; }

            [JsonProperty("CustomList2")] public object CustomList2 { get; set; }

            [JsonProperty("CustomList3")] public object CustomList3 { get; set; }

            [JsonProperty("CustomField1")] public CustomField1 CustomField1 { get; set; }

            [JsonProperty("CustomField2")] public CustomField2 CustomField2 { get; set; }

            [JsonProperty("CustomField3")] public CustomField3 CustomField3 { get; set; }

            [JsonProperty("CurrentBalance")] public double CurrentBalance { get; set; }

            [JsonProperty("SellingDetails")] public SellingDetails SellingDetails { get; set; }

            [JsonProperty("PaymentDetails")] public object PaymentDetails { get; set; }

            [JsonProperty("LastModified")] public DateTime LastModified { get; set; }

            [JsonProperty("PhotoURI")] public object PhotoUri { get; set; }

            [JsonProperty("URI")] public string Uri { get; set; }

            [JsonProperty("RowVersion")] public string RowVersion { get; set; }
        }

        public class Root
        {
            [JsonProperty("Items")] public List<Item> Items { get; set; }

            [JsonProperty("NextPageLink")] public object NextPageLink { get; set; }

            [JsonProperty("Count")] public int Count { get; set; }
        }

        public class SellingDetails
        {
            [JsonProperty("SaleLayout")] public string SaleLayout { get; set; }

            [JsonProperty("PrintedForm")] public object PrintedForm { get; set; }

            [JsonProperty("InvoiceDelivery")] public string InvoiceDelivery { get; set; }

            [JsonProperty("ItemPriceLevel")] public object ItemPriceLevel { get; set; }

            [JsonProperty("IncomeAccount")] public object IncomeAccount { get; set; }

            [JsonProperty("ReceiptMemo")] public object ReceiptMemo { get; set; }

            [JsonProperty("SalesPerson")] public object SalesPerson { get; set; }

            [JsonProperty("SaleComment")] public object SaleComment { get; set; }

            [JsonProperty("ShippingMethod")] public object ShippingMethod { get; set; }

            [JsonProperty("HourlyBillingRate")] public double HourlyBillingRate { get; set; }

            [JsonProperty("ABN")] public object Abn { get; set; }

            [JsonProperty("ABNBranch")] public object AbnBranch { get; set; }

            [JsonProperty("TaxCode")] public TaxCode TaxCode { get; set; }

            [JsonProperty("FreightTaxCode")] public FreightTaxCode FreightTaxCode { get; set; }

            [JsonProperty("UseCustomerTaxCode")] public bool UseCustomerTaxCode { get; set; }

            [JsonProperty("Terms")] public Terms Terms { get; set; }

            [JsonProperty("Credit")] public Credit Credit { get; set; }

            [JsonProperty("TaxIdNumber")] public object TaxIdNumber { get; set; }

            [JsonProperty("Memo")] public object Memo { get; set; }
        }

        public class TaxCode
        {
            [JsonProperty("UID")] public string Uid { get; set; }

            [JsonProperty("Code")] public string Code { get; set; }

            [JsonProperty("URI")] public string Uri { get; set; }
        }

        public class Terms
        {
            [JsonProperty("PaymentIsDue")] public string PaymentIsDue { get; set; }

            [JsonProperty("DiscountDate")] public int DiscountDate { get; set; }

            [JsonProperty("BalanceDueDate")] public int BalanceDueDate { get; set; }

            [JsonProperty("DiscountForEarlyPayment")]
            public double DiscountForEarlyPayment { get; set; }

            [JsonProperty("MonthlyChargeForLatePayment")]
            public double MonthlyChargeForLatePayment { get; set; }

            [JsonProperty("VolumeDiscount")] public double VolumeDiscount { get; set; }
        }


    }

}
