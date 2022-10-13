using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PicoHRDLGui
{
    internal class ResultFile
    {
        public Tuple<float[], float[]> rawTuple;
        public rawInfo rawinfo;
        
        public ResultFile() { }
        public ResultFile(Tuple<float[], float[]> raw)
        {
            rawTuple = raw;
        }
        public ResultFile(Tuple<float[], float[]> raw, rawInfo ri)
        {
            rawTuple = raw;
            rawinfo = ri;
        }
    }
    public class rawInfo
    {
        public DateTime startDateTime;
        public DateTime endDateTime;
        public int laserWavelength;
        public string chamberName;
        public string lotName;
        public string cassetteName;
        public string runName;
        public string recipeName;
        public string comment;

        public rawInfo(DateTime start, int laserWL, string chamber, string lot, string cassette, string run, string recipe)
        {
            this.startDateTime = start;
            this.endDateTime = start;
            this.laserWavelength = laserWL;
            this.chamberName = chamber;
            this.lotName = lot;
            this.cassetteName = cassette;
            this.runName = run;
            this.recipeName = recipe;
            this.comment = "";
        }
        public rawInfo()
        {
            startDateTime = DateTime.UtcNow;
            endDateTime = DateTime.UtcNow;
            laserWavelength = 0;
            chamberName = "Unknown";
            lotName = "Unknown";
            cassetteName = "Unknown";
            runName = "Manual run at " + DateTime.Now;
            recipeName = "Manual";
            comment = "";
        }
    }
}
