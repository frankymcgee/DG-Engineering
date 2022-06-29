using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DG_Engineering.Framework.Global.MYOB
{
    internal class Quotes
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Customer
    {
        [JsonProperty("UID")]
        public string UID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("DisplayID")]
        public string DisplayID { get; set; }

        [JsonProperty("URI")]
        public string URI { get; set; }
    }

    public class FreightTaxCode
    {
        [JsonProperty("UID")]
        public string UID { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("URI")]
        public string URI { get; set; }
    }

    public class Item
    {
        [JsonProperty("UID")]
        public string UID { get; set; }

        [JsonProperty("Number")]
        public string Number { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("CustomerPurchaseOrderNumber")]
        public string CustomerPurchaseOrderNumber { get; set; }

        [JsonProperty("Customer")]
        public Customer Customer { get; set; }

        [JsonProperty("Terms")]
        public Terms Terms { get; set; }

        [JsonProperty("IsTaxInclusive")]
        public bool IsTaxInclusive { get; set; }

        [JsonProperty("Subtotal")]
        public double Subtotal { get; set; }

        [JsonProperty("Freight")]
        public double Freight { get; set; }

        [JsonProperty("FreightTaxCode")]
        public FreightTaxCode FreightTaxCode { get; set; }

        [JsonProperty("TotalTax")]
        public double TotalTax { get; set; }

        [JsonProperty("TotalAmount")]
        public double TotalAmount { get; set; }

        [JsonProperty("Category")]
        public object Category { get; set; }

        [JsonProperty("Salesperson")]
        public Salesperson Salesperson { get; set; }

        [JsonProperty("JournalMemo")]
        public string JournalMemo { get; set; }

        [JsonProperty("ReferralSource")]
        public object ReferralSource { get; set; }

        [JsonProperty("BalanceDueAmount")]
        public double BalanceDueAmount { get; set; }

        [JsonProperty("QuoteType")]
        public string QuoteType { get; set; }

        [JsonProperty("ForeignCurrency")]
        public object ForeignCurrency { get; set; }

        [JsonProperty("LastModified")]
        public DateTime LastModified { get; set; }

        [JsonProperty("URI")]
        public string URI { get; set; }

        [JsonProperty("RowVersion")]
        public string RowVersion { get; set; }
    }

    public class Root
    {
        [JsonProperty("Items")]
        public List<Item> Items { get; set; }

        [JsonProperty("NextPageLink")]
        public object NextPageLink { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }
    }

    public class Salesperson
    {
        [JsonProperty("UID")]
        public string UID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("DisplayID")]
        public string DisplayID { get; set; }

        [JsonProperty("URI")]
        public string URI { get; set; }
    }

    public class Terms
    {
        [JsonProperty("PaymentIsDue")]
        public string PaymentIsDue { get; set; }

        [JsonProperty("DiscountDate")]
        public int DiscountDate { get; set; }

        [JsonProperty("BalanceDueDate")]
        public int BalanceDueDate { get; set; }

        [JsonProperty("DiscountForEarlyPayment")]
        public double DiscountForEarlyPayment { get; set; }

        [JsonProperty("MonthlyChargeForLatePayment")]
        public double MonthlyChargeForLatePayment { get; set; }

        [JsonProperty("DiscountExpiryDate")]
        public DateTime DiscountExpiryDate { get; set; }

        [JsonProperty("Discount")]
        public double Discount { get; set; }

        [JsonProperty("DueDate")]
        public DateTime DueDate { get; set; }

        [JsonProperty("FinanceCharge")]
        public double FinanceCharge { get; set; }
    }


    }
}
