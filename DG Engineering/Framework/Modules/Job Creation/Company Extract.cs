using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG_Engineering.Framework.Global;
using DG_Engineering.Framework.Global.Assignar;
using Newtonsoft.Json;
using RestSharp;

namespace DG_Engineering
{
    public partial class MainWindow
    {
        /// <summary>
        /// Excracts Company Names from Assignar
        /// </summary>
        /// <param name="basestring">Company Name Being Referenced to</param>
        private async Task CompanyIdExtract(string basestring)
        {
            var lstStringsToCheck = new List<string>();
            await AssignarAPIConnect("/clients?active=true", Method.Get, null);
            var assignarclients =  Static.AssignarResponseContent;
            var clientlist = JsonConvert.DeserializeObject<Clients.Root>(assignarclients);
            if (clientlist != null)
            {
                lstStringsToCheck.AddRange(clientlist.Data.Select(a => a.Name));
            }
            var resultset = lstStringsToCheck.ToDictionary(s => s, s => Levenshtein.Compute(basestring, s));
            //get the minimum number of modifications needed to arrive at the basestring
            var minimumModifications = resultset.Min(c => c.Value);
            //gives you a list with all strings that need a minimum of modifications to become the
            //same as the baseString
            var closestlist = resultset.Where(c => c.Value == minimumModifications);
            foreach (var a in closestlist)
            {
                var company = a.Key;
                if (clientlist == null) continue;
                foreach (var b in clientlist.Data.Where(b => company == b.Name))
                {
                    _companyId = b.Id;
                    ProjectClient.Text = b.Name;
                }
            }
        }
    }
}