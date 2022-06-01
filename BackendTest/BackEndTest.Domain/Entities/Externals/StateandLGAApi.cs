using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Domain.Entities.Externals
{
    public static class StateandLGAApi
    {
      
        public static bool IsMapped(string selectedState, string selectedLGA)
        {
            List<StateandLGAs> items = new List<StateandLGAs>();
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "StateandLGA.json");
            bool isMapped = false;
            using (StreamReader r = new StreamReader(fullPath))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<StateandLGAs>>(json);
            }

            if (items.Count > 0)
            {
                foreach (var item in items)
                {
                    if (item.state.Contains(selectedState))
                    {
                        var re = item.lgas.Where(x => x.Contains(selectedLGA)).First();

                        if (re.Length > 0)
                            isMapped = true;

                    }

                };
            }
            return isMapped;
        }
    }

    public class StateandLGAs
    {
        public string state { get; set; }
        public List<string> lgas { get; set; }
    }

}
