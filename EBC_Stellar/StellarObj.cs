using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_API.Models
{
    public class ObjectAPI
    {
        public Links _links { get; set; }
        public Embedded _embedded { get; set; }
    }
    public class Links
    {
        public Url self { get; set; }
        public Url next { get; set; }
        public Url prev { get; set; }
        public Url transaction { get; set; }
        public Url effects { get; set; }
        public Url succeeds { get; set; }
        public Url precedes { get; set; }
    }
    public class Url
    {
        public string href { get; set; }
    }
    public class Embedded
    {
        public List<Records> records { get; set; }
    }
    public class Records
    {
        public Links _links { get; set; }
        public string id { get; set; }
        public string paging_token { get; set; }
        public string transaction_successful { get; set; }
        public string source_account { get; set; }
        public string type { get; set; }
        public string type_i { get; set; }
        public string created_at { get; set; }
        public string transaction_hash { get; set; }
        public string starting_balance { get; set; }
        public string funder { get; set; }
        public string account { get; set; }
        public string asset_type { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string amount { get; set; }
    }

}