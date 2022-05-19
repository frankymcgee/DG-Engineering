using System.Collections.Generic;
using System.Linq;
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
        /// <param name="basestring">Company Name from SimPro Being Referenced to</param>
        public void CompanyIdExtract(string basestring)
        {
            ProgressBar.PerformStep();
            var lstStringsToCheck = new List<string>();
            var assignarclients =
                AssignarConnect(Static.AssignarDashboardUrl + "clients/", Static.JwtToken, Method.GET,null);
            var clientlist = JsonConvert.DeserializeObject<Clients.Root>(assignarclients);
            if (clientlist != null) lstStringsToCheck.AddRange(clientlist.Data.Select(a => a.Name));
            var resultset = lstStringsToCheck.ToDictionary(stringtoTest => stringtoTest,
                stringtoTest => Levenshtein.Compute(basestring, stringtoTest));
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
                    CompanyId = b.Id;
                }
            }
            ProgressBar.PerformStep();
        }
    }
}